using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using InvertoryHelper.Common;
using InvertoryHelper.Model;
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

        public Command EditCommand => new Command( async () =>
            {
                if (CurrentOrder != null)
                    Navigation?.PushAsync(new OrderPage(new OrderModel(await DataRepository.Instance.GetOrderAsync(CurrentOrder.Uid))));
            }
        );

        public Command AddCommand
        {
            get { return new Command(() => { Navigation?.PushAsync(new OrderPage(new OrderModel()
            {
                Number = OrdersList.Max(o=>o.Number)+1
            })); }); }
        }

        public OrdersViewModel()
        {
            OrdersList = new ObservableCollection<OrderModel>();

            LoadOrders();
        }

        public async void LoadOrders()
        {


            var ordersList = await DataRepository.Instance.GetOrdersAsync();

            OrdersList = new ObservableCollection<OrderModel>(ordersList.Select((o)=>new OrderModel(o)).OrderBy((o=>o.Number)));

            OnPropertyChanged("OrdersList");

        }
    }
}