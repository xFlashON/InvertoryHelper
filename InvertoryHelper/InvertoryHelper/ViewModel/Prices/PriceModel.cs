using System;
using System.ComponentModel;
using InvertoryHelper.Common;
using InvertoryHelper.Model;

namespace InvertoryHelper.ViewModel.Prices
{
    public class PriceModel : ObservableObject
    {
        private Characteristic _characteristic;

        private Nomenclature _nomenclature;

        private decimal _price;

        public PriceModel(Price price)
        {
            Uid = price.Uid;
            Nomenclature = price.Nomenclature;
            Characteristic = price.Characteristic;
            _price = price.price;
        }

        public Guid Uid { get; set; }

        public Nomenclature Nomenclature
        {
            get => _nomenclature;
            set
            {
                _nomenclature = value;
                OnPropertyChanged("Nomenclature");
            }
        }

        public Characteristic Characteristic
        {
            get => _characteristic;
            set
            {
                _characteristic = value;
                OnPropertyChanged("Characteristic");
            }
        }

        public decimal Price
        {
            get => _price;
            set
            {
                _price = value;
                OnPropertyChanged("Price");
            }
        }

    }
}