using System.Collections.ObjectModel;
using InvertoryHelper.Common;
using InvertoryHelper.Model;
using Xamarin.Forms;
using InvertoryHelper.View.Nomenclatures;
using System;

namespace InvertoryHelper.ViewModel.Nomenclatures
{
    public class NomenclaturesViewModel : BaseViewModel
    {
        public NomenclatureModel SelectedNomenclature { get; set; }

        public ObservableCollection<NomenclatureModel> NomenclaturesList { get; set; }

        public INavigation Navigation;

        public string SearchText { get; set; }

        public Command SearchCommand { get { return new Command(() => SearchNomenclatures()); } }

        public Command AddCommand { get { return new Command(() => AddNomenclature()); } }
        public Command EditCommand { get { return new Command(() => EditNomenclature()); } }

        public NomenclaturesViewModel()
        {
            Title = "Loading";

            NomenclaturesList = new ObservableCollection<NomenclatureModel>();

            LoadNomenclaturesList();

            SelectedNomenclature = null;
            OnPropertyChanged("SelectedNomenclature");

            MessagingCenter.Subscribe<Nomenclature>(this, "SaveNomenclature", SaveNomenclature);

        }

        private async void LoadNomenclaturesList()
        {
            if (!IsBusy)
            {

                IsBusy = true;

                NomenclaturesList.Clear();

                var nomenclaturesList = await DataRepository.Instance.GetNomenclaturesAsync();

                foreach( var N in nomenclaturesList)
                {
                    NomenclaturesList.Add(new NomenclatureModel(N));
                }

                Title = "Nomenclatures";

                IsBusy = false;
            }

        }

        private async void SearchNomenclatures()
        {

            if (!IsBusy)
            {
                if (SearchText == null ||  SearchText == string.Empty)
                    LoadNomenclaturesList();
                else
                {
                    IsBusy = true;

                    Title = "Searching";

                    var nomenclaturesList = await DataRepository.Instance.GetNomenclaturesAsync(new System.Func<Nomenclature, bool>((N)=> 
                    {
                        if (N.Name.Contains(SearchText)||(N.Artikul != null && N.Artikul.Contains(SearchText)))
                            return true;
                        else
                            return false;
                    }));

                    NomenclaturesList.Clear();

                    foreach (var N in nomenclaturesList)
                    {
                        NomenclaturesList.Add(new NomenclatureModel(N));
                    }

                    Title = "Nomenclatures";

                    IsBusy = false;
                }
            }

        }

        private async void AddNomenclature()
        {

            if (Navigation != null)
                await Navigation.PushAsync(new NomenclatureItemPage(Navigation));

        }

        private async void EditNomenclature()
        {
            if (SelectedNomenclature!=null)
                if (Navigation != null)
                    await Navigation.PushAsync(new NomenclatureItemPage(Navigation,SelectedNomenclature));

        }

        private async void SaveNomenclature (Nomenclature nomenclature)
        {
            if (nomenclature != null)
            {
                Guid uid = await DataRepository.Instance.SaveNomenclatureAsync(nomenclature);

                if (uid == Guid.Empty)
                {
                    MessagingCenter.Send<String>("Error! Nomenklature is not saved!","DisplayAlert");
                    return;
                }

                SearchNomenclatures();

            }
        }

    }
}