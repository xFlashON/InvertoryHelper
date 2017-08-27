using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Android.OS;
using Android.Util;
using InvertoryHelper.Common;
using InvertoryHelper.Model.Documents.Order;
using Xamarin.Forms;

namespace InvertoryHelper.ViewModel.Documents.Orders
{
    public class OrderViewModel : ObservableObject
    {
        private Command _selectNomenclatureCommand;

        public INavigation Navigation;

        public OrderModel Order { get; private set; }

        public OrderViewModel(OrderModel order = null)
        {
            Order = order ?? new OrderModel();
        }

        public Command AddRowCommand => new Command(() =>
        {

            Order.OrderRows?.Add(new OrderRowModel());

        });

        public Command SelectNomenclatureCommand => _selectNomenclatureCommand ?? (_selectNomenclatureCommand = new Command(
                                                        (p) =>
                                                        {
                                                            int a = 1;
                                                        }));
    }

}
