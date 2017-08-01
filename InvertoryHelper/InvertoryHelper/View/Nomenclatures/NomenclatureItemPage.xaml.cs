using InvertoryHelper.ViewModel.Nomenclatures;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace InvertoryHelper.View.Nomenclatures
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NomenclatureItemPage : ContentPage
    {
        public NomenclatureItemPage(INavigation Navigation = null, NomenclatureModel nomenclature = null)
        {
            InitializeComponent();

            var vm = new NomenclatureItemViewModel(nomenclature);

            vm.Navigation = Navigation;

            BindingContext = vm;
        }
    }
}