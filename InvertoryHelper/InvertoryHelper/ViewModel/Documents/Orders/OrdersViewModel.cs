using System.Collections.ObjectModel;
using InvertoryHelper.Common;
using InvertoryHelper.Model.Documents.Order;
using InvertoryHelper.View.Documents.Orders;
using Xamarin.Forms;

namespace InvertoryHelper.ViewModel.Documents.Orders
{
    public class OrdersViewModel : BaseViewModel
    {
        public INavigation Navigation;

        public ObservableCollection<OrderModel> OrdersList { get; set; }

        public OrderModel CurrentOrder { get; set; }

        public Command EditCommand => new Command(() =>
            {
                if (CurrentOrder != null)
                    Navigation?.PushAsync(new OrderPage(CurrentOrder));
            }
        );

        public Command AddCommand
        {
            get { return new Command(() => { Navigation?.PushAsync(new OrderPage()); }); }
        }
    }
}