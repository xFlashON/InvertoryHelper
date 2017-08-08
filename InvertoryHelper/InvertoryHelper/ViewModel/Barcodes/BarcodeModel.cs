using System;
using System.ComponentModel;
using InvertoryHelper.Model;

namespace InvertoryHelper.ViewModel.Barcodes
{
    public class BarcodeModel : INotifyPropertyChanged
    {
        private Characteristic _characteristic;

        private string _code;

        private Nomenclature _nomenclature;

        public BarcodeModel(Barcode barcode)
        {
            Uid = barcode.Uid;
            Nomenclature = barcode.Nomenclature;
            Characteristic = barcode.Characteristic;
            Code = barcode.Code;
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

        public string Code
        {
            get => _code;
            set
            {
                _code = value;
                OnPropertyChanged("Code");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string property)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }
    }
}