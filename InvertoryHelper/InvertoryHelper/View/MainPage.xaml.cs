using InvertoryHelper.ViewModel;
using Xamarin.Forms;

namespace InvertoryHelper.View
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

            var vm = new MainViewModel();
            vm.navigation = Navigation;
            BindingContext = vm;
        }
    }
}