using InvertoryHelper.Common;
using InvertoryHelper.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.Util;
using Java.IO;
using Xamarin.Forms;
using ZXing;
using ZXing.Mobile;

namespace InvertoryHelper.ViewModel
{
    public class PriceCheckingViewModel: BaseViewModel
    {
        private Nomenclature _nomenclature;
        private Characteristic _characteristic;
        private decimal _price;
        private Command _scanCommand;

        public INavigation Navigation;

        public Nomenclature Nomenclature
        {
            get => _nomenclature;
            set
            {
                _nomenclature = value;
                OnPropertyChanged("Nomenclature");
            }
        }

        public Characteristic Characteristic
        {
            get => _characteristic;
            set
            {
                _characteristic = value;
                OnPropertyChanged("Characteristic");
            }
        }

        public decimal Price
        {
            get => _price;
            set
            {
                _price = value;
                OnPropertyChanged("Price");
            }
        }

        public Command ScanCmd => _scanCommand ?? (_scanCommand = new Command(Scan));

        private async void Scan()
        {

            try
            {
                var scanner = new ZXing.Mobile.MobileBarcodeScanner();

                var result = await scanner.Scan();

                if (result != null)
                    BarcodeHandling(result.Text);
            }
            catch (Exception ex)
            {
                Log.Error("CameraError",ex.Message);
                MessagingCenter.Send(Resourses.Resource.ScanningError, "DisplayAlert");
            }

        }

        private void BarcodeHandling(string barcode)
        {
            MessagingCenter.Send(barcode, "DisplayAlert");
        }

    }
}
