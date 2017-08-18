using System;
using InvertoryHelper.Common;
using InvertoryHelper.Model;
using InvertoryHelper.Resourses;
using Xamarin.Forms;

namespace InvertoryHelper.ViewModel.NomenclatureKinds
{
    public class NomenclatureKindItemViewModel : BaseViewModel
    {
        private readonly NomenclaturesKind nomenclatureKind;
        public INavigation Navigation;

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
            if (nomenclatureKind != null)
            {
                var uid = await DataRepository.Instance.SaveNomenclatureKindAsync(nomenclatureKind);

                if (uid == Guid.Empty)
                {
                    MessagingCenter.Send("Error! Nomenclature kind is not saved!", "DisplayAlert");
                    return;
                }
            }

            MessagingCenter.Send(nomenclatureKind, "SaveNomenclaturesKind");
            await Navigation?.PopAsync();
        });

        public Command CancelButton => new Command(async () => { await Navigation?.PopAsync(); });
    }
}