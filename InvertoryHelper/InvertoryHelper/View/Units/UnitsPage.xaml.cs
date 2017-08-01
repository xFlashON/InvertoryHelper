using InvertoryHelper.ViewModel.Units;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace InvertoryHelper.View.Units
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UnitsPage : ContentPage
    {
        public UnitsPage()
        {
            InitializeComponent();

            var vm = new UnitsViewModel();

            vm.Navigation = Navigation;

            BindingContext = vm;
        }
    }
}