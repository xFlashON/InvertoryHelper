using System;
using System.Collections.ObjectModel;
using InvertoryHelper.Common;
using InvertoryHelper.Model;
using InvertoryHelper.Resourses;
using Xamarin.Forms;

namespace InvertoryHelper.ViewModel.Nomenclatures
{
    public class NomenclatureItemViewModel : BaseViewModel
    {
        private readonly Nomenclature nomenclature;
        public INavigation Navigation;

        public NomenclatureItemViewModel(NomenclatureModel Nomenclature = null)
        {
            nomenclature = new Nomenclature();

            if (Nomenclature == null)
            {
                Title = Resource.AddNomenclature;
            }
            else
            {
                Title = Resource.EditNomenclature;
                nomenclature.Uid = Nomenclature.Uid;
                Name = Nomenclature.Name;
                Artikul = Nomenclature.Artikul;
                BaseUnit = Nomenclature.BaseUnit;
                BaseNomenclatureKind = Nomenclature.NomenclaturesKind;
            }

            LoadUnitsList();
            LoadNomenclatureKindsList();
        }

        public ObservableCollection<Unit> BaseUnitsList { get; set; }

        public ObservableCollection<NomenclaturesKind> NomenclatureKindsList { get; set; }

        public string Name
        {
            get => nomenclature.Name;
            set
            {
                nomenclature.Name = value;
                OnPropertyChanged("Name");
            }
        }


        public string Artikul
        {
            get => nomenclature.Artikul;
            set
            {
                nomenclature.Artikul = value;
                OnPropertyChanged("Artikul");
            }
        }

        public Unit BaseUnit
        {
            get => nomenclature.BaseUnit;
            set
            {
                nomenclature.BaseUnit = value;
                OnPropertyChanged("BaseUnit");
            }
        }

        public NomenclaturesKind BaseNomenclatureKind
        {
            get => nomenclature.NomenclaturesKind;
            set
            {
                nomenclature.NomenclaturesKind = value;
                OnPropertyChanged("BaseNomenclatureKind");
            }
        }

        public Command SaveButton => new Command(async () =>
        {
            if (nomenclature != null)
            {
                var uid = await DataRepository.Instance.SaveNomenclatureAsync(nomenclature);

                if (uid == Guid.Empty)
                {
                    MessagingCenter.Send("Error! Nomenclature is not saved!", "DisplayAlert");
                    return;
                }
            }

            MessagingCenter.Send(nomenclature, "SaveNomenclature");
            await Navigation.PopAsync();
        });

        public Command CancelButton => new Command(async () => { await Navigation.PopAsync(); });

        private async void LoadUnitsList()
        {
            var UnitsList = await DataRepository.Instance.GetUnitsAsync();
            BaseUnitsList = new ObservableCollection<Unit>(UnitsList);

            OnPropertyChanged("BaseUnitsList");
        }

        private async void LoadNomenclatureKindsList()
        {
            var NomenclatureKinds = await DataRepository.Instance.GetNomenclatureKindsAsync();
            NomenclatureKindsList = new ObservableCollection<NomenclaturesKind>(NomenclatureKinds);

            OnPropertyChanged("NomenclatureKindsList");
        }
    }
}