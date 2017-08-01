using InvertoryHelper.Common;
using InvertoryHelper.Model;
using InvertoryHelper.View.NomenclatureKinds;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace InvertoryHelper.ViewModel.NomenclatureKinds
{
    public class NomenclatureKindItemViewModel : BaseViewModel
    {
        private NomenclaturesKind nomenclatureKind;

        public INavigation Navigation;

        public string Name
        {
            get => nomenclatureKind.Name;
            set
            {
                nomenclatureKind.Name = value;
                OnPropertyChanged("Name");
            }
        }

        public Command SaveButton
        {
            get => new Command(async () =>
            {
                MessagingCenter.Send<NomenclaturesKind>(nomenclatureKind, "SaveNomenclaturesKind");
                await Navigation?.PopAsync();
            });
        }
        public Command CancelButton { get => new Command(async () => { await Navigation?.PopAsync(); }); }

        public NomenclatureKindItemViewModel(NomenclatureKindModel NomenclaturesKind = null)
        {
            nomenclatureKind = new NomenclaturesKind();

            if (NomenclaturesKind == null)
                Title = "Add nomenclatures kind";
            else
            {
                Title = "Edit nomenclatures kind";
                nomenclatureKind.Uid = NomenclaturesKind.Uid;
                Name = NomenclaturesKind.Name;
            }
        }
    }
}
