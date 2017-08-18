using System.Collections.ObjectModel;
using InvertoryHelper.Common;
using InvertoryHelper.Model;
using InvertoryHelper.Resourses;
using InvertoryHelper.View.Prices;
using Xamarin.Forms;

namespace InvertoryHelper.ViewModel.Prices
{
    public class PricesViewModel : BaseViewModel
    {
        private PriceModel _selectedPrice;
        public INavigation Navigation;

        public PricesViewModel()
        {
            Title = Resource.Loading;

            PricesList = new ObservableCollection<PriceModel>();

            LoadPricesList();

            MessagingCenter.Subscribe<Price>(this, "SavePrice", SavePrice);
        }

        public ObservableCollection<PriceModel> PricesList { get; set; }

        public PriceModel SelectedPrice
        {
            get => _selectedPrice;
            set
            {
                _selectedPrice = value;
                OnPropertyChanged("SelectedPrice");
            }
        }

        public string SearchText { get; set; }

        public Command AddCommand
        {
            get { return new Command(() => AddPrice()); }
        }

        public Command EditCommand
        {
            get { return new Command(() => EditPrice()); }
        }

        public Command SearchCommand
        {
            get { return new Command(() => LoadPricesList()); }
        }

        private async void LoadPricesList()
        {
            if (IsBusy)
                return;

            if (string.IsNullOrEmpty(SearchText))
            {
                IsBusy = true;

                PricesList.Clear();

                var pricesList = await DataRepository.Instance.GetPricesAsync();

                foreach (var price in pricesList)
                    PricesList.Add(new PriceModel(price));

                Title = Resource.Prices;

                IsBusy = false;
            }
            else
            {
                IsBusy = true;

                Title = Resource.Searching;

                PricesList.Clear();

                var pricesList = await DataRepository.Instance.GetPricesAsync(P =>
                {
                    if (P.Nomenclature.Name.ToUpper().Contains(SearchText.ToUpper()) ||
                        P.Nomenclature.Artikul != null &&
                        P.Nomenclature.Artikul.ToUpper().Contains(SearchText.ToUpper()))
                        return true;
                    return false;
                });

                foreach (var price in pricesList)
                    PricesList.Add(new PriceModel(price));

                OnPropertyChanged("PriceList");

                Title = Resource.Prices;

                IsBusy = false;
            }
        }

        private async void AddPrice()
        {
            if (Navigation != null)
                await Navigation.PushAsync(new PriceItemPage(Navigation));
        }

        private async void EditPrice()
        {
            if (SelectedPrice != null && Navigation != null)
                await Navigation.PushAsync(new PriceItemPage(Navigation, SelectedPrice));
        }

        private async void SavePrice(Price price)
        {
            if (!IsBusy)
                LoadPricesList();
        }
    }
}