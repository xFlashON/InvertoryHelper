using InvertoryHelper.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvertoryHelper.ViewModel.Nomenclatures
{
    public class NomenclatureModel : INotifyPropertyChanged
    {

        private string name;
        private string artikul;
        private Unit baseUnit;
        private NomenclaturesKind nomenclatureKind;


        public event PropertyChangedEventHandler PropertyChanged;

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

        protected void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged == null)
                return;

            PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
