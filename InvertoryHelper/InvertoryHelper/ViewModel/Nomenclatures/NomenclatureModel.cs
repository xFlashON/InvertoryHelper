using System;
using System.ComponentModel;
using InvertoryHelper.Common;
using InvertoryHelper.Model;

namespace InvertoryHelper.ViewModel.Nomenclatures
{
    public class NomenclatureModel : ObservableObject
    {
        private string artikul;
        private Unit baseUnit;

        private string name;
        private NomenclaturesKind nomenclatureKind;

        public NomenclatureModel()
        {
        }

        public NomenclatureModel(Nomenclature nomenclature)
        {
            Uid = nomenclature.Uid;
            Name = nomenclature.Name;
            Artikul = nomenclature.Artikul;
            BaseUnit = nomenclature.BaseUnit;
            NomenclaturesKind = nomenclature.NomenclaturesKind;
        }

        public Guid Uid { get; set; }

        public string Name
        {
            get => name;
            set
            {
                name = value;
                OnPropertyChanged("Name");
            }
        }

        public string Artikul
        {
            get => artikul;
            set
            {
                artikul = value;
                OnPropertyChanged("Artikul");
            }
        }


        public Unit BaseUnit
        {
            get => baseUnit;
            set
            {
                baseUnit = value;
                OnPropertyChanged("BaseUnit");
            }
        }


        public NomenclaturesKind NomenclaturesKind
        {
            get => nomenclatureKind;
            set
            {
                nomenclatureKind = value;
                OnPropertyChanged("NomenclaturesKind");
            }
        }

    }
}