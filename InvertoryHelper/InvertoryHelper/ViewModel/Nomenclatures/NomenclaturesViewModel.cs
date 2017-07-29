using System.Collections.ObjectModel;
using InvertoryHelper.Common;
using InvertoryHelper.Model;
using Xamarin.Forms;
using InvertoryHelper.View.Nomenclatures;

namespace InvertoryHelper.ViewModel.Nomenclatures
{
    public class NomenclaturesViewModel : BaseViewModel
    {
        public Nomenclature SelectedNomenclature { get; set; }

        public ObservableCollection<Nomenclature> NomenclaturesList { get; set; }

        public INavigation Navigation;

        public string SearchText { get; set; }

        public Command SearchCommand { get { return new Command(() => SearchNomenclatures()); } }

        public Command AddCommand { get { return new Command(() => AddNomenclature()); } }
        public Command EditCommand { get { return new Command(() => EditNomenclature()); } }

        public NomenclaturesViewModel()
        {
            Title = "Loading";

            LoadNomenclaturesList();

            SelectedNomenclature = null;
            OnPropertyChanged("SelectedNomenclature");

        }

        private async void LoadNomenclaturesList()
        {
            if (!IsBusy)
            {

                IsBusy = true;

                var nomenclaturesList = await DataRepository.Instance.GetNomenclaturesAsync();

                NomenclaturesList = new ObservableCollection<Nomenclature>(nomenclaturesList);

                OnPropertyChanged("NomenclaturesList");

                Title = "Nomenclatures";

                IsBusy = false;
            }

        }

        private async void SearchNomenclatures()
        {

            if (!IsBusy)
            {
                if (SearchText == string.Empty)
                    LoadNomenclaturesList();
                else
                {
                    IsBusy = true;

                    Title = "Searching";

                    var nomenclaturesList = await DataRepository.Instance.GetNomenclaturesAsync(new System.Func<Nomenclature, bool>((N)=> 
                    {
                        if (N.Name.Contains(SearchText) ||N.Artikul.Contains(SearchText))
                            return true;
                        else
                            return false;
                    }));

                    NomenclaturesList = new ObservableCollection<Nomenclature>(nomenclaturesList);

                    OnPropertyChanged("NomenclaturesList");

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
    }
}