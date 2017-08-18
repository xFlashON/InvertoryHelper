using InvertoryHelper.ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace InvertoryHelper.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PriceCheckingPage : ContentPage
    {
        public PriceCheckingPage()
        {
            InitializeComponent();

            var vm = new PriceCheckingViewModel();

            vm.Navigation = Navigation;

            BindingContext = vm;
        }
    }
}