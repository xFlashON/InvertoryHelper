using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InvertoryHelper.Model;

namespace InvertoryHelper.ViewModel.Units
{
    public class UnitModel : INotifyPropertyChanged
    {

        public Guid Uid;
        private string name;


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
