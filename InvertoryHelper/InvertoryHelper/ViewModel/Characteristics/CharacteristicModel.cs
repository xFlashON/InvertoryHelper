using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InvertoryHelper.Model;

namespace InvertoryHelper.ViewModel.Characteristics
{
    public class CharacteristicModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private NomenclaturesKind _nomenclaturesKind;
        private string _name;

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

        public CharacteristicModel(Characteristic characteristic)
        {
            this.Uid = characteristic.Uid;
            this.Name = characteristic.Name;
            this.NomenclaturesKind = characteristic.NomenclaturesKind;

        }

        public void OnPropertyChanged(string property)
        {
            PropertyChanged?.Invoke(this,new PropertyChangedEventArgs(property));
        }
    }
}
