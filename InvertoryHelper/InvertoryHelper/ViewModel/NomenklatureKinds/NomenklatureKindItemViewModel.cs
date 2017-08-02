using InvertoryHelper.Common;
using InvertoryHelper.Model;
using InvertoryHelper.Resourses;
using Xamarin.Forms;

namespace InvertoryHelper.ViewModel.NomenclatureKinds
{
    public class NomenclatureKindItemViewModel : BaseViewModel
    {
        public INavigation Navigation;
        private readonly NomenclaturesKind nomenclatureKind;

        public NomenclatureKindItemViewModel(NomenclatureKindModel NomenclaturesKind = null)
        {
            nomenclatureKind = new NomenclaturesKind();

            if (NomenclaturesKind == null)
            {
                Title = Resource.AddNomenclatureKind;
            }
            else
            {
                Title = Resource.EditNomenclatureKind;
                nomenclatureKind.Uid = NomenclaturesKind.Uid;
                Name = NomenclaturesKind.Name;
            }
        }

        public string Name
        {
            get => nomenclatureKind.Name;
            set
            {
                nomenclatureKind.Name = value;
                OnPropertyChanged("Name");
            }
        }

        public Command SaveButton => new Command(async () =>
        {
            MessagingCenter.Send(nomenclatureKind, "SaveNomenclaturesKind");
            await Navigation?.PopAsync();
        });

        public Command CancelButton => new Command(async () => { await Navigation?.PopAsync(); });
    }
}