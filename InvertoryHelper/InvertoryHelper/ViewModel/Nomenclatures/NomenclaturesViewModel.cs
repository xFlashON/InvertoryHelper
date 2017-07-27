using System.Collections.ObjectModel;
using InvertoryHelper.Common;
using InvertoryHelper.Model;
using Xamarin.Forms;

namespace InvertoryHelper.ViewModel.Nomenclatures
{
    public class NomenclaturesViewModel : BaseViewModel
    {
        public Nomenclature CurrentNomenclature { get; set; }

        public ObservableCollection<Nomenclature> NomenclaturesList { get; set; }

        public INavigation Navigation;

        public NomenclaturesViewModel()
        {
            Title = "Loading";

            LoadNomenclaturesList();
        }

        private async void LoadNomenclaturesList()
        {
            var nomenclaturesList = await DataRepository.Instance.GetNomenclaturesAsync();

            NomenclaturesList = new ObservableCollection<Nomenclature>(nomenclaturesList);

            OnPropertyChanged("NomenclaturesList");

            Title = "Nomenclatures";

            OnPropertyChanged("Title");

        }
    }
}