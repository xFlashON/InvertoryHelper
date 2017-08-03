using System;
using System.ComponentModel;
using InvertoryHelper.Model;

namespace InvertoryHelper.ViewModel.Characteristics
{
    public class CharacteristicModel : INotifyPropertyChanged
    {
        private string _name;

        private NomenclaturesKind _nomenclaturesKind;

        public CharacteristicModel(Characteristic characteristic)
        {
            Uid = characteristic.Uid;
            Name = characteristic.Name;
            NomenclaturesKind = characteristic.NomenclaturesKind;
        }

        public Guid Uid { get; set; }

        public NomenclaturesKind NomenclaturesKind
        {
            get => _nomenclaturesKind;
            set
            {
                _nomenclaturesKind = value;
                OnPropertyChanged("NomenclaturesKind");
            }
        }

        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged("Name");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string property)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }
    }
}