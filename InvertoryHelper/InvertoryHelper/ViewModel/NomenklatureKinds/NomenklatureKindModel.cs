using System;
using InvertoryHelper.Common;
using InvertoryHelper.Model;

namespace InvertoryHelper.ViewModel.NomenclatureKinds
{
    public class NomenclatureKindModel : ObservableObject
    {
        private string name;

        public NomenclatureKindModel(NomenclaturesKind nomenclatureKind)
        {
            Uid = nomenclatureKind.Uid;
            name = nomenclatureKind.Name;
        }

        public Guid Uid { get; set; }

        public string Name
        {
            get => name;
            set
            {
                name = value;
                OnPropertyChanged("Name");
            }
        }

        public override string ToString()
        {
            return Name;
        }
    }
}