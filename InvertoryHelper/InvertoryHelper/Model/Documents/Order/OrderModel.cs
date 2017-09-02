using System;
using System.Collections.ObjectModel;
using System.Linq;
using InvertoryHelper.Common;

namespace InvertoryHelper.Model.Documents.Order
{
    public class OrderModel : ObservableObject
    {
        private Order _order;

        public OrderModel(Order order = null)
        {
            if (order == null)
            {
                _order = new Order();
                OrderRows = new ObservableCollection<OrderRowModel>();
                Date = DateTime.Now;
            }
            else
            {
                _order = order;
                OrderRows = new ObservableCollection<OrderRowModel>(_order.OrderRows.Select(r => new OrderRowModel(r)));
            }
        }

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
            get { return _order.Date; }
            set
            {
                _order.Date = value;
                OnPropertyChanged("Date");
            }
        }

        public decimal Number
        {
            get => _order.Number;
            set
            {
                _order.Number = value;
                OnPropertyChanged("Number");
            }
        }

        public decimal Total
        {
            get => _order.Total;
            set
            {
                _order.Total = value;
                OnPropertyChanged("Total");
            }
        }

        public string Comment
        {
            get => _order.Comment;
            set
            {
                _order.Comment = value;
                OnPropertyChanged("Comment");
            }
        }

        public ObservableCollection<OrderRowModel> OrderRows { get; set; }

        public Order GetOrder()
        {
            _order.OrderRows = OrderRows.Select(r => new OrderRow
                {
                    Nomenclature = r.Nomenclature,
                    Characteristic = r.Characteristic,
                    Price = r.Price,
                    Amount = r.Amount,
                    Total = r.Total,
                    Uid = r.Uid,
                    Order = _order
                })
                .ToList();

            _order.Total = _order.OrderRows.Sum(r => r.Total);

            return _order;
        }
    }
}