using InvertoryHelper.ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace InvertoryHelper.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ReferencesPage : ContentPage
    {
        public ReferencesPage()
        {
            InitializeComponent();

            BindingContext = new ReferencesViewModel { navigation = Navigation};
        }
    }
}