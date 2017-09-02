using System;
using System.Linq;
using Android.Util;
using InvertoryHelper.Common;
using InvertoryHelper.Model;
using InvertoryHelper.Model.Documents.Order;
using InvertoryHelper.Resourses;
using InvertoryHelper.View.Nomenclatures;
using Xamarin.Forms;

namespace InvertoryHelper.ViewModel.Documents.Orders
{
    public class OrderViewModel : ObservableObject
    {
        private OrderRowModel _selectedRow;
        private Command _selectNomenclatureCommand;

        public INavigation Navigation;

        public OrderModel Order { get; set; }

        public OrderViewModel(OrderModel order = null)
        {
            Order = order ?? new OrderModel();

            MessagingCenter.Subscribe<Nomenclature>(this, "SelectedNomenclature", SelectedNomenclature);
        }

        public OrderRowModel SelectedRow
        {
            get => _selectedRow;
            set
            {
                _selectedRow = value;
                OnPropertyChanged("SelectedRow");
            }
        }

  

        public Command AddRowCommand => new Command(() => { Order.OrderRows?.Add(new OrderRowModel()); });

        public Command DeleteRowCommand => new Command(() =>
        {
            if (SelectedRow != null)
                Order.OrderRows?.Remove(SelectedRow);
        });

        public Command SelectNomenclatureCommand => _selectNomenclatureCommand ?? (_selectNomenclatureCommand =
                                                        new Command(
                                                            async p =>
                                                            {
                                                                SelectedRow = p as OrderRowModel;
                                                                await Navigation.PushAsync(new NomenclaturesPage(true));
                                                            }));

        public Command CurrentRowChanged => new Command(p =>
        {
            var param = p as OrderRowModel;
            if (SelectedRow != param)
                SelectedRow = param;
        });

        public Command ScanCommand => new Command(ScanBarcode);

        public Command CloseCommand => new Command(async () => await Navigation?.PopAsync());

        public Command SaveCommand => new Command(async () =>
        {
            var order = Order.GetOrder();

            if(order==null)
                return;

            var uid = await DataRepository.Instance.SaveOrderAsync(order);

            if (uid == Guid.Empty)
            {
                MessagingCenter.Send("Error! Order is not saved!", "DisplayAlert");
                return;
            }

            await Navigation?.PopAsync();

            MessagingCenter.Send(order, "SaveOrder");

        });

        private void SelectedNomenclature(Nomenclature nomenclature)
        {
            if (SelectedRow != null)
                SelectedRow.Nomenclature = nomenclature;
        }

        private async void ScanBarcode()
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

            var Code = barcode;

            var barcodesList = await DataRepository.Instance.GetBarcodesAsync(b => b.Code == barcode);

            var resultBarcode = barcodesList.FirstOrDefault();

            if (resultBarcode == null)
            {
                DependencyService.Get<IOnPlatform>().PlaySound("err.wav");

                return;
            }

            DependencyService.Get<IOnPlatform>().PlaySound("sucsess.wav");

            var nomenclature = resultBarcode.Nomenclature;
            var characteristic = resultBarcode.Characteristic;

            var priceList =
                await DataRepository.Instance.GetPricesAsync(
                    f => !f.Nomenclature.Equals(nomenclature) || characteristic == null || f.Characteristic == null ||
                         f.Characteristic.Equals(characteristic));

            var resultPrice = priceList.FirstOrDefault();

            var price = resultPrice?.price ?? 0;

            var exRow = Order.OrderRows
                .FirstOrDefault(r => r.Nomenclature != null && r.Nomenclature.Equals(nomenclature) &&
                                     (characteristic == null ||
                                      r.Characteristic != null && r.Characteristic.Equals(characteristic)));

            if (exRow != null)
            {
                exRow.Amount += 1;
            }
            else
            {
                var newRow = new OrderRowModel();
                newRow.Nomenclature = nomenclature;
                newRow.Characteristic = characteristic;
                newRow.Price = price;
                newRow.Amount = 1;

                Order.OrderRows.Add(newRow);

                SelectedRow = newRow;
            }
        }
    }
}