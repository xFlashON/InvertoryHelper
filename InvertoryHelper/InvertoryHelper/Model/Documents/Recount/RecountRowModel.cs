using System;
using System.Collections.ObjectModel;
using System.Linq;
using InvertoryHelper.Common;

namespace InvertoryHelper.Model.Documents.Recount
{
    public class RecountRowModel : ObservableObject
    {
        private readonly RecountRow _recountRow;

        public RecountRowModel()
        {
            _recountRow = new RecountRow();
        }

        public RecountRowModel(RecountRow recountRow)
        {
            _recountRow = recountRow;
        }

        public Guid Uid
        {
            get => _recountRow.Uid;
            set
            {
                _recountRow.Uid = value;
                OnPropertyChanged("Uid");
            }
        }

        public Recount Recount
        {
            get => _recountRow.Recount;
            private set
            {
                _recountRow.Recount = value;
                OnPropertyChanged("Recount");
            }
        }

        public Nomenclature Nomenclature
        {
            get => _recountRow.Nomenclature;
            set
            {
                _recountRow.Nomenclature = value;
                OnPropertyChanged("Nomenclature");
                OnPropertyChanged("CharacteristicsList");

                FillPrice();
            }
        }

        public Unit Unit
        {
            get => _recountRow.Unit;
            set
            {
                _recountRow.Unit = value;
                OnPropertyChanged("Unit");
            }
        }

        public Characteristic Characteristic
        {
            get => _recountRow.Characteristic;
            set
            {
                _recountRow.Characteristic = value;
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
            get => _recountRow.Amount;
            set
            {
                _recountRow.Amount = value;
                OnPropertyChanged("Amount");
                RecalculateCurrentRow();
            }
        }

        public decimal Price
        {
            get => _recountRow.Price;
            set
            {
                _recountRow.Price = value;
                OnPropertyChanged("Price");
                RecalculateCurrentRow();
            }
        }

        public decimal Total
        {
            get => _recountRow.Total;
            set
            {
                _recountRow.Total = value;
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

            var prices = await DataRepository.Instance.GetPricesAsync(p =>
            {
                return Nomenclature.Equals(p.Nomenclature) && (Characteristic == null
                           ? p.Characteristic == null
                           : Characteristic?.Equals(p.Characteristic) == true);
            });

            var priceItem = prices.FirstOrDefault();

            if (priceItem != null)
                Price = priceItem.price;
        }
    }
}