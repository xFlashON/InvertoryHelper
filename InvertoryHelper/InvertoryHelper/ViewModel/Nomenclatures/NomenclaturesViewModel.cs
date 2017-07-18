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
            Title = "Nomenclatures";

            using (var db = DependencyService.Get<IDataRepository>())
            {
                if (!IsBusy)
                    NomenclaturesList = new ObservableCollection<Nomenclature>(db.GetNomenclatures());

                OnPropertyChanged("NomenclaturesList");
            }
        }
    }
}