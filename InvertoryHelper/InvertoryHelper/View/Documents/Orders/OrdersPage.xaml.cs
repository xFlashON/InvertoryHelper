using InvertoryHelper.ViewModel.Documents.Orders;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace InvertoryHelper.View.Documents.Orders
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OrdersPage : ContentPage
    {
        public OrdersPage()
        {
            InitializeComponent();

            var vm = new OrdersViewModel();
            vm.Navigation = Navigation;
            BindingContext = vm;
        }
    }
}