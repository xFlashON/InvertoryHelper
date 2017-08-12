using InvertoryHelper.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvertoryHelper.ViewModel.Prices
{
    public class PriceModel : INotifyPropertyChanged
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

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string property)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }
    }
}
