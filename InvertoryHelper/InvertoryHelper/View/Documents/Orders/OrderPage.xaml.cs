using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InvertoryHelper.Model.Documents.Order;
using InvertoryHelper.ViewModel.Documents.Orders;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace InvertoryHelper.View.Documents.Orders
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OrderPage : TabbedPage
    {
        public OrderPage(OrderModel order = null)
        {
            InitializeComponent();

            var vm = new OrderViewModel(order);
            vm.Navigation = Navigation;
            BindingContext = vm;
        }

    }
}