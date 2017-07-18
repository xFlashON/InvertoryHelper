using InvertoryHelper.ViewModel.Nomenclatures;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace InvertoryHelper.View.Nomenclatures
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NomenclaturesPage : ContentPage
    {
        public NomenclaturesPage()
        {
            InitializeComponent();

            var vm = new NomenclaturesViewModel();

            vm.Navigation = Navigation;

            BindingContext = vm;

        }
    }
}