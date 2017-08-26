using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.Content.Res;
using InvertoryHelper.Common;
using InvertoryHelper.Model.Documents.Order;
using InvertoryHelper.View.Documents;
using InvertoryHelper.View.Documents.Orders;
using Xamarin.Forms;

namespace InvertoryHelper.ViewModel.Documents.Orders
{
    public class OrdersViewModel:BaseViewModel
    {
        public INavigation Navigation;

        public ObservableCollection<OrderModel> OrdersList { get; set; }

        public OrderModel CurrentOrder { get; set; }

        public Command EditCommand { get=>new Command(() =>
            {
                if (CurrentOrder != null)
                Navigation?.PushAsync(new OrderPage(CurrentOrder));
            }
        );}

        public Command AddCommand
        {
            get { return new Command(() => { Navigation?.PushAsync(new OrderPage()); }); }
        }

    }
}
