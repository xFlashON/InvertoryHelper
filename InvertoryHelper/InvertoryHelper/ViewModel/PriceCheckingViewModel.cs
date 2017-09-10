using System;
using System.Linq;
using System.Threading.Tasks;
using Android.Util;
using InvertoryHelper.Common;
using InvertoryHelper.Model;
using InvertoryHelper.Resourses;
using Xamarin.Forms;

namespace InvertoryHelper.ViewModel
{
    public class PriceCheckingViewModel : BaseViewModel
    {
        private Characteristic _characteristic;
        private string _code;
        private Nomenclature _nomenclature;
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

        public Command AppearingCmd => new Command(async () =>
        {

            bool useScanner = false;
            if (App.Current.Properties.ContainsKey("UseScanner"))
                useScanner = (bool) App.Current.Properties["UseScanner"];

            if (useScanner)
            {
                string deviceName = string.Empty;

                if (App.Current.Properties.ContainsKey("DeviceName"))
                    deviceName = (string) App.Current.Properties["DeviceName"];

                var connectDevice = DependencyService.Get<IScanner>()?.ConnectDevice(deviceName);
                if (connectDevice != null)
                {
                    var result = await connectDevice;

                    if (result == null)
                    {
                        MessagingCenter.Subscribe<string>(this, "ScannedCode",BarcodeHandling);

                        Task task = new Task(() =>
                        {
                            DependencyService.Get<IScanner>().GetBarcode();
                        });

                        task.Start(); 
                    }
                    else
                    {
                        MessagingCenter.Send("Scanner: "+result.Message, "DisplayAlert");
                    }
                        
                }
            }


        });

        public Command DisappearingCmd => new Command(() =>
        {
            bool useScanner = false;
            if (App.Current.Properties.ContainsKey("UseScanner"))
                useScanner = (bool)App.Current.Properties["UseScanner"];

            if (useScanner)
            {
               MessagingCenter.Unsubscribe<string>(this, "ScannedCode");

                var result = DependencyService.Get<IScanner>()?.DisconnectDevice(); ;

                if (result != null)
                    MessagingCenter.Send(result.Message, "DisplayAlert");
            }

        });

        private async void Scan()
        {
            try
            {
                var result = await DependencyService.Get<IOnPlatform>()
                    .ScanBarcode(Resource.ScaningBarcode);

                if (result != null)
                    BarcodeHandling(result);
            }
            catch (Exception ex)
            {
                Log.Error("Error", ex.Message);
                MessagingCenter.Send(Resource.ScanningError, "DisplayAlert");
            }
        }

        private async void BarcodeHandling(string barcode)
        {
            if (barcode == string.Empty)
                return;

            Code = barcode;

            var barcodesList = await DataRepository.Instance.GetBarcodesAsync(b => b.Code == barcode);

            var resultBarcode = barcodesList.FirstOrDefault();

            if (resultBarcode == null)
            {
                Nomenclature = null;
                Characteristic = null;
                Price = 0;

                DependencyService.Get<IOnPlatform>().PlaySound("err.wav");

                return;
            }

            DependencyService.Get<IOnPlatform>().PlaySound("sucsess.wav");

            Nomenclature = resultBarcode.Nomenclature;
            Characteristic = resultBarcode.Characteristic;

            var priceList =
                await DataRepository.Instance.GetPricesAsync(
                    p => Nomenclature.Equals(p.Nomenclature) && (Characteristic == null
                             ? p.Characteristic == null
                             : Characteristic?.Equals(p.Characteristic) == true));

            var resultPrice = priceList.FirstOrDefault();

            Price = resultPrice?.price ?? 0;
        }
    }
}