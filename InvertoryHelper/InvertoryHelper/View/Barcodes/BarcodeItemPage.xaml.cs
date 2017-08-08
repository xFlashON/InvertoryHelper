using InvertoryHelper.ViewModel.Barcodes;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace InvertoryHelper.View.Barcodes
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BarcodeItemPage : ContentPage
    {
        public BarcodeItemPage(INavigation Navigation = null, BarcodeModel barcode = null)
        {
            InitializeComponent();

            var vm = new BarcodeItemViewModel(barcode);

            vm.Navigation = Navigation;

            BindingContext = vm;
        }
    }
}