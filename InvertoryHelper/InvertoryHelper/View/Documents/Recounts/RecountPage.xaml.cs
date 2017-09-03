using InvertoryHelper.Model.Documents.Recount;
using InvertoryHelper.ViewModel.Documents.Recounts;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace InvertoryHelper.View.Documents.Recounts
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RecountPage : TabbedPage
    {
        public RecountPage(RecountModel recount = null)
        {
            InitializeComponent();

            var vm = new RecountViewModel(recount);
            vm.Navigation = Navigation;
            BindingContext = vm;
        }
    }
}