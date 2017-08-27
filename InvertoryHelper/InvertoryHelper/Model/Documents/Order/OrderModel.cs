using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InvertoryHelper.Common;
using Xamarin.Forms;

namespace InvertoryHelper.Model.Documents.Order
{
    public class OrderModel : ObservableObject
    {
        private readonly Order _order;

        public Guid Uid
        {
            get => _order.Uid;
            set
            {
                _order.Uid = value;
                OnPropertyChanged("Uid");
            }
        }

        public DateTime Date
        {
            get => _order.Date;
            set
            {
                _order.Date = value;
                OnPropertyChanged("Date");
            }
        }

        public Decimal Number
        {
            get => _order.Number;
            set
            {
                _order.Number = value;
                OnPropertyChanged("Number");
            }
        }

        public Decimal Total
        {
            get => _order.Total;
            set
            {
                _order.Total = value;
                OnPropertyChanged("Total");
            }
        }

        public ObservableCollection<OrderRowModel> OrderRows { get; set; }

        public OrderModel()
        {
            _order = new Order();
            OrderRows = new ObservableCollection<OrderRowModel>();
        }

        public OrderModel(Order order)
        {
            _order = order;
            OrderRows = new ObservableCollection<OrderRowModel>(_order.OrderRows.Select((r)=>new OrderRowModel(r)));
        }

    }
}
