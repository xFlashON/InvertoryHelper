using System;
using System.ComponentModel;
using InvertoryHelper.Model;

namespace InvertoryHelper.ViewModel.Units
{
    public class UnitModel : INotifyPropertyChanged
    {
        private string name;

        public Guid Uid;


        public UnitModel(Unit unit)
        {
            Uid = unit.Uid;
            name = unit.Name;
        }

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