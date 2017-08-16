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
        private string _code;
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

        public string Code
        {
            get => _code;
            set
            {
                _code = value;
                OnPropertyChanged("Code");
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
                string result = await DependencyService.Get<IOnPlatform>()
                    .ScanBarcode(Resourses.Resource.ScaningBarcode);

                if (result != null)
                    BarcodeHandling(result);
            }
            catch (Exception ex)
            {
                Log.Error("Error", ex.Message);
                MessagingCenter.Send(Resourses.Resource.ScanningError, "DisplayAlert");
            }

        }

        private async void BarcodeHandling(string barcode)
        {
            if (barcode == string.Empty)
                return;

            Code = barcode;


            
            var barcodesList = await DataRepository.Instance.GetBarcodesAsync(new Func<Barcode, bool>((b) => b.Code == barcode));

            var resultBarcode = barcodesList.FirstOrDefault();

            if (resultBarcode == null)
            {
                Nomenclature = null;
                Characteristic = null;
                Price = 0;
                return;
            }

            DependencyService.Get<IOnPlatform>().PlaySound("sucsess.wav");

                Nomenclature = resultBarcode.Nomenclature;
                Characteristic = resultBarcode.Characteristic;

            var priceList =
                await DataRepository.Instance.GetPricesAsync(
                    new Func<Price, bool>((f) => !f.Nomenclature.Equals(Nomenclature) || (Characteristic == null || f.Characteristic == null) || f.Characteristic.Equals(Characteristic)));

            var resultPrice = priceList.FirstOrDefault();

            Price = resultPrice?.price ?? 0;



        }
        
    }
}
