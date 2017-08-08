using InvertoryHelper.ViewModel.Barcodes;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace InvertoryHelper.View.Barcodes
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BarcodesPage : ContentPage
    {
        public BarcodesPage()
        {
            InitializeComponent();

            var vm = new BarcodesViewModel();

            vm.Navigation = Navigation;

            BindingContext = vm;
        }
    }
}