using InvertoryHelper.ViewModel.Units;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace InvertoryHelper.View.Units
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UnitItemPage : ContentPage
    {
        public UnitItemPage(INavigation Navigation = null, UnitModel unit = null)
        {
            InitializeComponent();

            var vm = new UnitItemViewModel(unit);

            vm.Navigation = Navigation;

            BindingContext = vm;
        }
    }
}