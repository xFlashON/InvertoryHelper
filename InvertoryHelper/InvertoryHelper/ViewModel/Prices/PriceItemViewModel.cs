using System;
using System.Collections.ObjectModel;
using System.Linq;
using InvertoryHelper.Common;
using InvertoryHelper.Model;
using InvertoryHelper.Resourses;
using InvertoryHelper.View.Nomenclatures;
using Xamarin.Forms;

namespace InvertoryHelper.ViewModel.Prices
{
    internal class PriceItemViewModel : BaseViewModel
    {
        private readonly Price _price;
        public INavigation Navigation;

        public PriceItemViewModel(PriceModel Price = null)
        {
            _price = new Price();

            if (Price == null)
            {
                Title = Resource.AddPrice;
            }
            else
            {
                Title = Resource.EditPrice;
                _price.Uid = Price.Uid;
                _price.price = Price.Price;
                Nomenclature = Price.Nomenclature;
                Characteristic = Price.Characteristic;
            }

            LoadCharacteristicsList();

            MessagingCenter.Subscribe<Nomenclature>(this, "SelectedNomenclature", NomenclatureSelected);
        }

        public ObservableCollection<NomenclaturesKind> NomenclatureKindsList { get; set; }

        public ObservableCollection<Characteristic> CharacteristicsList { get; set; }

        public decimal Price
        {
            get => _price.price;
            set
            {
                _price.price = value;
                OnPropertyChanged("Price");
            }
        }

        public Nomenclature Nomenclature
        {
            get => _price.Nomenclature;

            set
            {
                _price.Nomenclature = value;
                OnPropertyChanged("Nomenclature");

                LoadCharacteristicsList();
            }
        }

        public Characteristic Characteristic
        {
            get => _price.Characteristic;

            set
            {
                _price.Characteristic = value;
                OnPropertyChanged("Characteristic");
            }
        }

        public Command SaveButton => new Command(async () =>
        {
            if (Nomenclature == null)
            {
                MessagingCenter.Send(Resource.CheckNomenclature, "DisplayAlert");
                return;
            }

            //if (Price == 0)
            //{
            //    MessagingCenter.Send(Resource.CheckPrice, "DisplayAlert");
            //    return;
            //}

            var dublicates = DataRepository.Instance
                .GetPricesAsync(p => p.Nomenclature.Equals(Nomenclature) && p.Characteristic != null &&
                                     p.Characteristic.Equals(Characteristic) && !p.Equals(_price))
                .Result.Count;

            if (dublicates > 0)
            {
                MessagingCenter.Send(Resource.DuplicatePrice, "DisplayAlert");
                return;
            }

            if (_price != null)
            {
                var uid = await DataRepository.Instance.SavePriceAsync(_price);

                if (uid == Guid.Empty)
                {
                    MessagingCenter.Send("Error! Price is not saved!", "DisplayAlert");
                    return;
                }
            }

            MessagingCenter.Send(_price, "SavePrice");
            await Navigation?.PopAsync();
        });

        public Command CancelButton => new Command(async () => { await Navigation?.PopAsync(); });

        public Command SelectNomenclature => new Command(() => { Navigation?.PushAsync(new NomenclaturesPage(true)); });


        public Command ValidateNomenclature => new Command(p =>
        {
            var entry = p as Entry;
            if (entry == null || entry.Text == string.Empty)
            {
                Nomenclature = null;
                return;
            }

            Nomenclature =
                DataRepository.Instance
                    .GetNomenclaturesAsync(
                        n => n.Name.StartsWith(entry.Text, StringComparison.CurrentCultureIgnoreCase) ||
                             n.Artikul != null && n.Artikul.Contains(entry.Text))
                    .Result.FirstOrDefault();
        });

        private async void LoadCharacteristicsList()
        {
            if (Nomenclature == null)
            {
                CharacteristicsList?.Clear();
                return;
            }

            var _nomenclature = await DataRepository.Instance.GetNomenclaturesAsync(n => Nomenclature.Equals(n))
                .ContinueWith(res => res.Result.FirstOrDefault());

            if (_nomenclature?.NomenclaturesKind == null)
            {
                CharacteristicsList?.Clear();
                return;
            }

            var characteristics =
                await DataRepository.Instance.GetCharacteristicsAsync(
                    n => _nomenclature.NomenclaturesKind.Equals(n.NomenclaturesKind));
            CharacteristicsList = new ObservableCollection<Characteristic>(characteristics);

            OnPropertyChanged("CharacteristicsList");
        }

        private void NomenclatureSelected(Nomenclature nomenclature)
        {
            Nomenclature = nomenclature;
        }
    }
}