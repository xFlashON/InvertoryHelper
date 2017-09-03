using System;
using System.Collections.ObjectModel;
using System.Linq;
using InvertoryHelper.Common;

namespace InvertoryHelper.Model.Documents.Order
{
    public class OrderRowModel : ObservableObject
    {
        private readonly OrderRow _orderRow;

        public OrderRowModel()
        {
            _orderRow = new OrderRow();
        }

        public OrderRowModel(OrderRow orderRow)
        {
            _orderRow = orderRow;
        }

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
                OnPropertyChanged("CharacteristicsList");

                FillPrice();
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

                FillPrice();
            }
        }

        public ObservableCollection<Characteristic> CharacteristicsList
        {
            get
            {
                if (Nomenclature == null)
                    return new ObservableCollection<Characteristic>();

                var _nomenclature = DataRepository.Instance.GetNomenclaturesAsync(n => Nomenclature.Equals(n))
                    .Result.FirstOrDefault();

                if (_nomenclature?.NomenclaturesKind == null)
                {
                    return new ObservableCollection<Characteristic>();
                    ;
                }

                var characteristics =
                    DataRepository.Instance.GetCharacteristicsAsync(
                            n => _nomenclature.NomenclaturesKind.Equals(n.NomenclaturesKind))
                        .Result;

                return new ObservableCollection<Characteristic>(characteristics);
            }
        }

        public decimal Amount
        {
            get => _orderRow.Amount;
            set
            {
                _orderRow.Amount = value;
                OnPropertyChanged("Amount");
                RecalculateCurrentRow();
            }
        }

        public decimal Price
        {
            get => _orderRow.Price;
            set
            {
                _orderRow.Price = value;
                OnPropertyChanged("Price");
                RecalculateCurrentRow();
            }
        }

        public decimal Total
        {
            get => _orderRow.Total;
            set
            {
                _orderRow.Total = value;
                OnPropertyChanged("Total");
            }
        }

        private void RecalculateCurrentRow()
        {
            Total = Price * Amount;
        }

        private async void FillPrice()
        {
            if (Nomenclature == null)
            {
                Price = 0;
                return;
            }

            var prices = await DataRepository.Instance.GetPricesAsync(new Func<Price, bool>((p)=>
                {
                    return Nomenclature.Equals(p.Nomenclature) && (Characteristic == null? p.Characteristic == null: Characteristic?.Equals(p.Characteristic) == true);
                }));

            var priceItem = prices.FirstOrDefault();

            if (priceItem != null)
                Price = priceItem.price;

        }
    }
}