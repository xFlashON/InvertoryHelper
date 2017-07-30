using InvertoryHelper.Common;
using InvertoryHelper.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace InvertoryHelper.ViewModel.Nomenclatures
{
    public class NomenclatureItemViewModel : BaseViewModel
    {
        private Nomenclature nomenclature;

        public INavigation Navigation;

        public ObservableCollection<Unit> BaseUnitsList { get; set;}

        public string Name
        {
            get { return nomenclature.Name; }
            set
            {
                nomenclature.Name = value;
                OnPropertyChanged("Name");
            }
        }


        public String Artikul
        {
            get { return nomenclature.Artikul; }
            set
            {
                nomenclature.Artikul = value;
                OnPropertyChanged("Artikul");
            }
        }

        public Unit BaseUnit
        {
            get { return nomenclature.BaseUnit; }
            set
            {
                nomenclature.BaseUnit = value;
                OnPropertyChanged("BaseUnit");
            }
        }

        public NomenclaturesKind BaseNomenklatureKind
        {
            get { return nomenclature.NomenclaturesKind; }
            set
            {
                nomenclature.NomenclaturesKind = value;
                OnPropertyChanged("BaseNomenklatureKind");
            }
        }

        public Command SaveButton { get => new Command(async () => 
        {
            MessagingCenter.Send<Nomenclature>(nomenclature,"SaveNomenclature");
            await Navigation.PopAsync();
        }); }
        public Command CancelButton { get => new Command(async () => { await Navigation.PopAsync(); }); }

        public NomenclatureItemViewModel(NomenclatureModel Nomenclature = null)
        {

            nomenclature = new Nomenclature();

            if (Nomenclature == null)
                Title = "Add nomenclature";
            else
            {
                Title = "Edit nomenclature";
                nomenclature.Uid = Nomenclature.Uid;
                Name = Nomenclature.Name;
                Artikul = Nomenclature.Artikul;
                BaseUnit = Nomenclature.BaseUnit;
                BaseNomenklatureKind = Nomenclature.NomenclaturesKind;
            }

            LoadUnitsList();

        }

        private async void LoadUnitsList()
        {
            var UnitsList = await DataRepository.Instance.GetUnitsAsync();
            BaseUnitsList = new ObservableCollection<Unit>(UnitsList);

            OnPropertyChanged("BaseUnitsList");

        }

    }

}
