using System;
using System.ComponentModel;
using InvertoryHelper.Model;

namespace InvertoryHelper.ViewModel.NomenclatureKinds
{
    public class NomenclatureKindModel : INotifyPropertyChanged
    {
        private string name;

        public NomenclatureKindModel(NomenclaturesKind nomenclatureKind)
        {
            Uid = nomenclatureKind.Uid;
            name = nomenclatureKind.Name;
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

        public event PropertyChangedEventHandler PropertyChanged;

        public override string ToString()
        {
            return Name;
        }

        public void OnPropertyChanged(string property)
        {
            if (PropertyChanged != null)
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }
    }
}