using System;
using System.Collections.ObjectModel;
using InvertoryHelper.Common;
using InvertoryHelper.Model;
using InvertoryHelper.Resourses;
using InvertoryHelper.View.Barcodes;
using Xamarin.Forms;

namespace InvertoryHelper.ViewModel.Barcodes
{
    public class BarcodesViewModel : BaseViewModel
    {
        private BarcodeModel _selectedBarcode;
        public INavigation Navigation;

        public BarcodesViewModel()
        {
            Title = Resource.Loading;

            BarcodesList = new ObservableCollection<BarcodeModel>();

            LoadBarcodesList();

            MessagingCenter.Subscribe<Barcode>(this, "SaveBarcode", SaveBarcode);
        }

        public ObservableCollection<BarcodeModel> BarcodesList { get; set; }

        public BarcodeModel SelectedBarcode
        {
            get => _selectedBarcode;
            set
            {
                _selectedBarcode = value;
                OnPropertyChanged("SelectedBarcode");
            }
        }

        public string SearchText { get; set; }

        public Command AddCommand
        {
            get { return new Command(() => AddBarcode()); }
        }

        public Command EditCommand
        {
            get { return new Command(() => EditBarcode()); }
        }

        public Command SearchCommand
        {
            get { return new Command(() => LoadBarcodesList()); }
        }

        private async void LoadBarcodesList()
        {
            if (IsBusy)
                return;

            if (string.IsNullOrEmpty(SearchText))
            {
                IsBusy = true;

                BarcodesList.Clear();

                var barcodesList = await DataRepository.Instance.GetBarcodesAsync();

                foreach (var barcode in barcodesList)
                    BarcodesList.Add(new BarcodeModel(barcode));

                Title = Resource.Barcodes;

                IsBusy = false;

            }
            else
            {
                IsBusy = true;

                Title = Resource.Searching;

                BarcodesList.Clear();

                var pricesList = await DataRepository.Instance.GetBarcodesAsync(P =>
                {
                    if (P.Nomenclature.Name.ToUpper().Contains(SearchText.ToUpper()) || P.Nomenclature.Artikul != null &&
                        P.Nomenclature.Artikul.ToUpper().Contains(SearchText.ToUpper()))
                        return true;
                    return false;
                });

                foreach (var price in pricesList)
                    BarcodesList.Add(new BarcodeModel(price));

                OnPropertyChanged("BarcodeList");

                Title = Resource.Barcodes;

                IsBusy = false;
            }

        }

        private async void AddBarcode()
        {
            if (Navigation != null)
                await Navigation.PushAsync(new BarcodeItemPage(Navigation));
        }

        private async void EditBarcode()
        {
            if (SelectedBarcode != null)
                if (Navigation != null)
                    await Navigation.PushAsync(new BarcodeItemPage(Navigation, SelectedBarcode));
        }

        private async void SaveBarcode(Barcode barcode)
        {
            if (barcode != null)
            {
                var uid = await DataRepository.Instance.SaveBarcodeAsync(barcode);

                if (uid == Guid.Empty)
                {
                    MessagingCenter.Send("Error! Barcode is not saved!", "DisplayAlert");
                    return;
                }

                LoadBarcodesList();
            }
        }
    }
}