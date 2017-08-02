using InvertoryHelper.Common;
using InvertoryHelper.Model;
using InvertoryHelper.Resourses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace InvertoryHelper.ViewModel.Characteristics
{
    class CharacteristicItemViewModel : BaseViewModel
    {
        public INavigation Navigation;
        private readonly Characteristic characteristic;

        public CharacteristicItemViewModel(CharacteristicModel Characteristic = null)
        {
            characteristic = new Characteristic();

            if (Characteristic == null)
            {
                Title = Resource.AddCharacteristic;
            }
            else
            {
                Title = Resource.EditCharacteristic;
                characteristic.Uid = Characteristic.Uid;
                Name = Characteristic.Name;
            }
        }

        public string Name
        {
            get => characteristic.Name;
            set
            {
                characteristic.Name = value;
                OnPropertyChanged("Name");
            }
        }

        public Command SaveButton => new Command(async () =>
        {
            MessagingCenter.Send(characteristic, "SaveCharacteristic");
            await Navigation?.PopAsync();
        });

        public Command CancelButton => new Command(async () => { await Navigation?.PopAsync(); });
    }
}