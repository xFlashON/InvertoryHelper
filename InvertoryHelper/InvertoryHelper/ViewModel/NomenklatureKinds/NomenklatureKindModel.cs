using InvertoryHelper.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvertoryHelper.ViewModel.NomenclatureKinds
{
    public class NomenclatureKindModel : INotifyPropertyChanged
    {

       
        private string name;

        public Guid Uid { get; set; }

        public NomenclatureKindModel(NomenclaturesKind nomenclatureKind)
        {
            Uid = nomenclatureKind.Uid;
            name = nomenclatureKind.Name;
        }

        public string Name
        {
            get => name;
            set
            {
                name = value;
                OnPropertyChanged("Name");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public override string ToString()
        {
            return Name;
        }

        public void OnPropertyChanged(string property)
        {
            if (PropertyChanged != null)
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }

    }
}