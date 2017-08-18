using System;
using System.Collections.ObjectModel;
using InvertoryHelper.Common;
using InvertoryHelper.Model;
using InvertoryHelper.Resourses;
using Xamarin.Forms;

namespace InvertoryHelper.ViewModel.Characteristics
{
    internal class CharacteristicItemViewModel : BaseViewModel
    {
        private readonly Characteristic characteristic;
        public INavigation Navigation;

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
                NomenclaturesKind = Characteristic.NomenclaturesKind;
            }

            LoadNomenclatureKindsList();
        }

        public ObservableCollection<NomenclaturesKind> NomenclatureKindsList { get; set; }

        public string Name
        {
            get => characteristic.Name;
            set
            {
                characteristic.Name = value;
                OnPropertyChanged("Name");
            }
        }

        public NomenclaturesKind NomenclaturesKind
        {
            get => characteristic.NomenclaturesKind;

            set
            {
                characteristic.NomenclaturesKind = value;
                OnPropertyChanged("NomenclaturesKind");
            }
        }

        public Command SaveButton => new Command(async () =>
        {
            if (NomenclaturesKind == null)
            {
                MessagingCenter.Send(Resource.CheckNomenclatureKind, "DisplayAlert");
                return;
            }

            if (characteristic != null)
            {
                var uid = await DataRepository.Instance.SaveCharacteristicAsync(characteristic);

                if (uid == Guid.Empty)
                {
                    MessagingCenter.Send("Error! Characteristic is not saved!", "DisplayAlert");
                    return;
                }
            }

            MessagingCenter.Send(characteristic, "SaveCharacteristic");
            await Navigation?.PopAsync();
        });

        public Command CancelButton => new Command(async () => { await Navigation?.PopAsync(); });

        private async void LoadNomenclatureKindsList()
        {
            var NomenclatureKinds = await DataRepository.Instance.GetNomenclatureKindsAsync();
            NomenclatureKindsList = new ObservableCollection<NomenclaturesKind>(NomenclatureKinds);

            OnPropertyChanged("NomenclatureKindsList");
        }
    }
}