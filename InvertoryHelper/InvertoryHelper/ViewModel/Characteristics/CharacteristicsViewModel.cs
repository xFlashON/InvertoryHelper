using System;
using System.Collections.ObjectModel;
using InvertoryHelper.Common;
using InvertoryHelper.Model;
using InvertoryHelper.Resourses;
using InvertoryHelper.View.Characteristics;
using Xamarin.Forms;

namespace InvertoryHelper.ViewModel.Characteristics
{
    public class CharacteristicsViewModel : BaseViewModel
    {
        private NomenclaturesKind _selectedNomenclatureKind;
        public INavigation Navigation;

        public CharacteristicsViewModel()
        {
            Title = Resource.Loading;

            CharacteristicsList = new ObservableCollection<CharacteristicModel>();

            LoadCharacteristicsList();

            LoadNomenclatureKindsList();

            MessagingCenter.Subscribe<Characteristic>(this, "SaveCharacteristic", SaveCharacteristic);
        }

        public ObservableCollection<NomenclaturesKind> NomenclatureKindsList { get; set; }

        public NomenclaturesKind SelectedNomenclatureKind
        {
            get => _selectedNomenclatureKind;
            set
            {
                _selectedNomenclatureKind = value;
                OnPropertyChanged("SelectedNomenclatureKind");
                SearchCommand();
            }
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

        public Command ClearCommand
        {
            get { return new Command(() => SelectedNomenclatureKind = null); }
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

        private async void SearchCommand()
        {
            if (SelectedNomenclatureKind == null)
            {
                LoadCharacteristicsList();
                return;
            }


            IsBusy = true;

            CharacteristicsList.Clear();

            var characteristicsList =
                await DataRepository.Instance.GetCharacteristicsAsync(
                    Ch => SelectedNomenclatureKind.Equals(Ch.NomenclaturesKind));

            foreach (var characteristic in characteristicsList)
                CharacteristicsList.Add(new CharacteristicModel(characteristic));

            OnPropertyChanged("CharacteristicsList");

            Title = Resource.Characteristics;

            IsBusy = false;
        }

        private async void LoadNomenclatureKindsList()
        {
            var nomenclatureKinds = await DataRepository.Instance.GetNomenclatureKindsAsync();
            NomenclatureKindsList = new ObservableCollection<NomenclaturesKind>(nomenclatureKinds);

            OnPropertyChanged("NomenclatureKindsList");
        }
    }
}