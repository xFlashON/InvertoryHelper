using System;
using System.Linq;
using Android.Util;
using InvertoryHelper.Common;
using InvertoryHelper.Model;
using InvertoryHelper.Model.Documents.Recount;
using InvertoryHelper.Resourses;
using InvertoryHelper.View.Nomenclatures;
using Xamarin.Forms;

namespace InvertoryHelper.ViewModel.Documents.Recounts
{
    public class RecountViewModel : ObservableObject
    {
        private RecountRowModel _selectedRow;
        private Command _selectNomenclatureCommand;

        public INavigation Navigation;

        public RecountViewModel(RecountModel recount = null)
        {
            Recount = recount ?? new RecountModel();

            MessagingCenter.Subscribe<Nomenclature>(this, "SelectedNomenclature", SelectedNomenclature);
        }

        public RecountModel Recount { get; set; }

        public RecountRowModel SelectedRow
        {
            get => _selectedRow;
            set
            {
                _selectedRow = value;
                OnPropertyChanged("SelectedRow");
            }
        }


        public Command AddRowCommand => new Command(() =>
        {
            var newRow = new RecountRowModel();

            Recount.RecountRows?.Add(newRow);

            SelectedRow = newRow;
        });

        public Command DeleteRowCommand => new Command(() =>
        {
            if (SelectedRow != null)
                Recount.RecountRows?.Remove(SelectedRow);
        });

        public Command SelectNomenclatureCommand => _selectNomenclatureCommand ?? (_selectNomenclatureCommand =
                                                        new Command(
                                                            async p =>
                                                            {
                                                                SelectedRow = p as RecountRowModel;
                                                                await Navigation.PushAsync(new NomenclaturesPage(true));
                                                            }));

        public Command CurrentRowChanged => new Command(p =>
        {
            var param = p as RecountRowModel;
            if (SelectedRow != param)
                SelectedRow = param;
        });

        public Command ScanCommand => new Command(ScanBarcode);

        public Command CloseCommand => new Command(async () => await Navigation?.PopAsync());

        public Command SaveCommand => new Command(async () =>
        {
            var recount = Recount.GetRecount();

            if (recount == null)
                return;

            var uid = await DataRepository.Instance.SaveRecountAsync(recount);

            if (uid == Guid.Empty)
            {
                MessagingCenter.Send("Error! Recount is not saved!", "DisplayAlert");
                return;
            }

            await Navigation?.PopAsync();

            MessagingCenter.Send(recount, "SaveRecount");
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
                    p => nomenclature.Equals(p.Nomenclature) && (characteristic == null
                             ? p.Characteristic == null
                             : characteristic?.Equals(p.Characteristic) == true));

            var resultPrice = priceList.FirstOrDefault();

            var price = resultPrice?.price ?? 0;

            RecountRowModel exRow = null;

            exRow = Recount.RecountRows
                .FirstOrDefault(r => nomenclature.Equals(r.Nomenclature) && characteristic == null
                    ? r.Characteristic == null
                    : characteristic?.Equals(r.Characteristic) == true);


            if (exRow != null)
            {
                exRow.Amount += 1;
            }
            else
            {
                var newRow = new RecountRowModel();
                newRow.Nomenclature = nomenclature;
                newRow.Characteristic = characteristic;
                newRow.Price = price;
                newRow.Amount = 1;

                Recount.RecountRows.Add(newRow);

                SelectedRow = newRow;
            }
        }
    }
}