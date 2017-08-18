using InvertoryHelper.ViewModel.Prices;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace InvertoryHelper.View.Prices
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PriceItemPage : ContentPage
    {
        public PriceItemPage(INavigation Navigation = null, PriceModel barcode = null)
        {
            InitializeComponent();

            var vm = new PriceItemViewModel(barcode);

            vm.Navigation = Navigation;

            BindingContext = vm;
        }
    }
}