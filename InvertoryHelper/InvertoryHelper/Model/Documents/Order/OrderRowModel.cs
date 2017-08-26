using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using InvertoryHelper.Common;
using Org.Apache.Http.Impl.IO;

namespace InvertoryHelper.Model.Documents.Order
{
    public class OrderRowModel : ObservableObject
    {
        private readonly OrderRow _orderRow;

        public Guid Uid
        {
            get => _orderRow.Uid;
            set
            {
                _orderRow.Uid = value;
                OnPropertyChanged("Uid");
            }
        }

        public Order Order
        {
            get => _orderRow.Order;
            private set
            {
                _orderRow.Order = value;
                OnPropertyChanged("Order");
            }
        }

        public Nomenclature Nomenclature
        {
            get => _orderRow.Nomenclature;
            set
            {
                _orderRow.Nomenclature = value; 
                OnPropertyChanged("Nomenclature");
            }
        }

        public Unit Unit
        {
            get => _orderRow.Unit;
            set
            {
                _orderRow.Unit = value;
                OnPropertyChanged("Unit");
            }
        }

        public Characteristic Characteristic
        {
            get => _orderRow.Characteristic;
            set
            {
                _orderRow.Characteristic = value;
                OnPropertyChanged("Characteristic");
            }
        }

        public Decimal Amount
        {
            get => _orderRow.Amount;
            set
            {
                _orderRow.Amount = value;
                OnPropertyChanged("Amount");
            }
        }

        public Decimal Price
        {
            get => _orderRow.Price;
            set
            {
                _orderRow.Price = value;
                OnPropertyChanged("Price");
            }
        }

        public Decimal Total
        {
            get => _orderRow.Total;
            set
            {
                _orderRow.Total = value;
                OnPropertyChanged("Total");
            }
        }

        public OrderRowModel()
        {
            _orderRow = new OrderRow();
        }

        public OrderRowModel(OrderRow orderRow)
        {
            _orderRow = orderRow;
        }
    }
}
