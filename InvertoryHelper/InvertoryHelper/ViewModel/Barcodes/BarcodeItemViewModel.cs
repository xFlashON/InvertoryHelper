using System;
using System.Collections.ObjectModel;
using System.Linq;
using InvertoryHelper.Common;
using InvertoryHelper.Model;
using InvertoryHelper.Resourses;
using InvertoryHelper.View.Nomenclatures;
using Xamarin.Forms;

namespace InvertoryHelper.ViewModel.Barcodes
{
    internal class BarcodeItemViewModel : BaseViewModel
    {
        private readonly Barcode barcode;
        public INavigation Navigation;

        public BarcodeItemViewModel(BarcodeModel Barcode = null)
        {
            barcode = new Barcode();

            if (Barcode == null)
            {
                Title = Resource.AddBarcode;
            }
            else
            {
                Title = Resource.EditBarcode;
                barcode.Uid = Barcode.Uid;
                Code = Barcode.Code;
                Nomenclature = Barcode.Nomenclature;
                Characteristic = Barcode.Characteristic;
            }

            LoadCharacteristicsList();

            MessagingCenter.Subscribe<Nomenclature>(this, "SelectedNomenclature", NomenclatureSelected);
        }

        public ObservableCollection<NomenclaturesKind> NomenclatureKindsList { get; set; }

        public ObservableCollection<Characteristic> CharacteristicsList { get; set; }

        public string Code
        {
            get => barcode.Code;
            set
            {
                barcode.Code = value;
                OnPropertyChanged("Code");
            }
        }

        public Nomenclature Nomenclature
        {
            get => barcode.Nomenclature;

            set
            {
                barcode.Nomenclature = value;
                OnPropertyChanged("Nomenclature");

                LoadCharacteristicsList();
            }
        }

        public Characteristic Characteristic
        {
            get => barcode.Characteristic;

            set
            {
                barcode.Characteristic = value;
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

            if (string.IsNullOrEmpty(Code))
            {
                MessagingCenter.Send(Resource.CheckBarcode, "DisplayAlert");
                return;
            }

            var dublicates = DataRepository.Instance
                .GetBarcodesAsync(b => b.Code == Code && !b.Equals(barcode))
                .Result.Count;

            if (dublicates > 0)
            {
                MessagingCenter.Send(Resource.DuplicateBarcode, "DisplayAlert");
                return;
            }

            if (barcode != null)
            {
                var uid = await DataRepository.Instance.SaveBarcodeAsync(barcode);

                if (uid == Guid.Empty)
                {
                    MessagingCenter.Send("Error! Barcode is not saved!", "DisplayAlert");
                    return;
                }
            }

            MessagingCenter.Send(barcode, "SaveBarcode");
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