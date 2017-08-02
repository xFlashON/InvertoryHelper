using InvertoryHelper.Common;
using InvertoryHelper.Resourses;

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InvertoryHelper.Model;
using InvertoryHelper.View.Characteristics;

using Xamarin.Forms;

namespace InvertoryHelper.ViewModel.Characteristics
{
    public class CharacteristicsViewModel:BaseViewModel
    {
        public INavigation Navigation;

        public CharacteristicsViewModel()
        {
            Title = Resource.Loading;

            CharacteristicsList = new ObservableCollection<CharacteristicModel>();

            LoadCharacteristicsList();

            MessagingCenter.Subscribe<Characteristic>(this, "SaveCharacteristic", SaveCharacteristic);
        }

        public CharacteristicModel SelectedCharacteristic { get; set; }

        public ObservableCollection<CharacteristicModel> CharacteristicsList { get; set; }

        public Command AddCommand
        {
            get { return new Command(() => AddCharacteristic()); }
        }

        public Command EditCommand
        {
            get { return new Command(() => EditCharacteristic()); }
        }

        private async void LoadCharacteristicsList()
        {
            if (!IsBusy)
            {
                IsBusy = true;

                CharacteristicsList.Clear();

                var characteristicsList = await DataRepository.Instance.GetCharacteristicsAsync();

                foreach (var characteristic in characteristicsList)
                    CharacteristicsList.Add(new CharacteristicModel(characteristic));

                Title = Resource.Characteristics;

                IsBusy = false;
            }
        }

        private async void AddCharacteristic()
        {
            if (Navigation != null)
                await Navigation.PushAsync(new CharacteristicItemPage(Navigation));
        }

        private async void EditCharacteristic()
        {
            if (SelectedCharacteristic != null)
                if (Navigation != null)
                    await Navigation.PushAsync(new CharacteristicItemPage(Navigation, SelectedCharacteristic));
        }

        private async void SaveCharacteristic(Characteristic characteristic)
        {
            if (characteristic != null)
            {
                var uid = await DataRepository.Instance.SaveCharacteristicAsync(characteristic);

                if (uid == Guid.Empty)
                {
                    MessagingCenter.Send("Error! Characteristic is not saved!", "DisplayAlert");
                    return;
                }

                LoadCharacteristicsList();
            }
        }
    }
}