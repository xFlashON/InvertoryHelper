using InvertoryHelper.ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace InvertoryHelper.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SettingsPage : ContentPage
    {
        public SettingsPage()
        {
            InitializeComponent();

            var vm = new SettingsViewModel();

            vm.Navigation = Navigation;

            BindingContext = vm;
        }
    }
}