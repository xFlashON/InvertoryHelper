using System.Collections.ObjectModel;
using InvertoryHelper.Common;
using InvertoryHelper.Model;
using Xamarin.Forms;
using InvertoryHelper.View.Nomenclatures;
using System;
using System.Collections.Generic;
using System.Linq;

namespace InvertoryHelper.ViewModel.Nomenclatures
{
    public class NomenclaturesViewModel : BaseViewModel
    {
        public NomenclatureModel SelectedNomenclature { get; set; }

        public ObservableCollection<NomenclatureModel> NomenclaturesList { get; set; }

        public List<NomenclaturesKind> NomenclatureKindsList { get; set; }

        private NomenclaturesKind selectedNomenclstureKind;

        public NomenclaturesKind SelectedNomenclstureKind
        {
            get => selectedNomenclstureKind;
            set
            {
                selectedNomenclstureKind = value;
                OnPropertyChanged("SelectedNomenclstureKind");
                SearchNomenclatures();
            }
        }

        public INavigation Navigation;

        public string SearchText { get; set; }

        public Command SearchCommand { get { return new Command(() => SearchNomenclatures()); } }

        public Command AddCommand { get { return new Command(() => AddNomenclature()); } }
        public Command EditCommand { get { return new Command(() => EditNomenclature()); } }

        public Command ClearNomenclatureKindCommand { get => new Command(() => SelectedNomenclstureKind = null); }

        public NomenclaturesViewModel()
        {
            Title = "Loading";

            NomenclaturesList = new ObservableCollection<NomenclatureModel>();

            LoadNomenclaturesList();

            SelectedNomenclature = null;
            OnPropertyChanged("SelectedNomenclature");

            MessagingCenter.Subscribe<Nomenclature>(this, "SaveNomenclature", SaveNomenclature);

            LoadNomenclatureKindsList();

        }

        private async void LoadNomenclaturesList()
        {
            if (!IsBusy)
            {

                IsBusy = true;

                var nomenclaturesList = await DataRepository.Instance.GetNomenclaturesAsync();

                if (SelectedNomenclstureKind != null)
                    nomenclaturesList = nomenclaturesList.Where((N) => N.NomenclaturesKind!=null && N.NomenclaturesKind.Uid == SelectedNomenclstureKind.Uid).ToList();

                NomenclaturesList = new ObservableCollection<NomenclatureModel>(nomenclaturesList.Select((N) => new NomenclatureModel(N)).ToList());

                OnPropertyChanged("NomenclaturesList");

                Title = "Nomenclatures";

                IsBusy = false;
            }

        }

        private async void SearchNomenclatures()
        {

            if (!IsBusy)
            {
                if (SearchText == null || SearchText == string.Empty)
                    LoadNomenclaturesList();
                else
                {
                    IsBusy = true;

                    Title = "Searching";

                    var nomenclaturesList = await DataRepository.Instance.GetNomenclaturesAsync(new System.Func<Nomenclature, bool>((N) =>
                    {
                        if (N.Name.ToUpper().Contains(SearchText.ToUpper()) || (N.Artikul != null && N.Artikul.ToUpper().Contains(SearchText.ToUpper())))
                            return true;
                        else
                            return false;
                    }));

                    if (SelectedNomenclstureKind != null)
                        nomenclaturesList = nomenclaturesList.Where((N) => N.NomenclaturesKind != null && N.NomenclaturesKind.Uid == SelectedNomenclstureKind.Uid).ToList();

                    NomenclaturesList = new ObservableCollection<NomenclatureModel>(nomenclaturesList.Select((N) => new NomenclatureModel(N)).ToList());

                    OnPropertyChanged("NomenclaturesList");

                    Title = "Nomenclatures";

                    IsBusy = false;
                }

            }

        }

        private async void AddNomenclature()
        {

            await Navigation?.PushAsync(new NomenclatureItemPage(Navigation));

        }

        private async void EditNomenclature()
        {
            if (SelectedNomenclature != null)
                await Navigation?.PushAsync(new NomenclatureItemPage(Navigation, SelectedNomenclature));

        }

        private async void SaveNomenclature(Nomenclature nomenclature)
        {
            if (nomenclature != null)
            {
                Guid uid = await DataRepository.Instance.SaveNomenclatureAsync(nomenclature);

                if (uid == Guid.Empty)
                {
                    MessagingCenter.Send<String>("Error! Nomenclature is not saved!", "DisplayAlert");
                    return;
                }

                SearchNomenclatures();

            }
        }

        private async void LoadNomenclatureKindsList()
        {
            var NomenclatureKinds = await DataRepository.Instance.GetNomenclatureKindsAsync();
            NomenclatureKindsList = new List<NomenclaturesKind>(NomenclatureKinds);

            OnPropertyChanged("NomenclatureKindsLis");

        }

    }
}