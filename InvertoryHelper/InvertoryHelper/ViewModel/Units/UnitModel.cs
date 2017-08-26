using System;
using System.ComponentModel;
using InvertoryHelper.Common;
using InvertoryHelper.Model;

namespace InvertoryHelper.ViewModel.Units
{
    public class UnitModel : ObservableObject
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

        public override string ToString()
        {
            return Name;
        }


    }
}