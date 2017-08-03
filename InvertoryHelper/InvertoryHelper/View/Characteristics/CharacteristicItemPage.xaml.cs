using InvertoryHelper.ViewModel.Characteristics;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace InvertoryHelper.View.Characteristics
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CharacteristicItemPage : ContentPage
    {
        public CharacteristicItemPage(INavigation Navigation = null, CharacteristicModel characteristic = null)
        {
            InitializeComponent();

            var vm = new CharacteristicItemViewModel(characteristic);

            vm.Navigation = Navigation;

            BindingContext = vm;
        }
    }
}