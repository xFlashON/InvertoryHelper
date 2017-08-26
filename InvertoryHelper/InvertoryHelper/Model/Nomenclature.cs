using System;
using InvertoryHelper.ViewModel.Nomenclatures;
using SQLite.Net.Attributes;
using SQLiteNetExtensions.Attributes;

namespace InvertoryHelper.Model
{
    [Table("Nomenclatures")]
    public class Nomenclature
    {
        public Nomenclature()
        {
            Name = string.Empty;
            Artikul = string.Empty;
        }

        public Nomenclature(NomenclatureModel nomenclatureModel)
        {
            Uid = nomenclatureModel.Uid;
            Name = nomenclatureModel.Name;
            Artikul = nomenclatureModel.Artikul;
            BaseUnit = nomenclatureModel.BaseUnit;
            NomenclaturesKind = nomenclatureModel.NomenclaturesKind;
        }

        [PrimaryKey]
        [Unique]
        [AutoIncrement]
        public Guid Uid { get; set; }

        [MaxLength(150)]
        public string Name { get; set; }

        [Indexed]
        [MaxLength(40)]
        public string Artikul { get; set; }

        [ForeignKey(typeof(Unit))]
        public Guid BaseUnitUid { get; set; }

        [ManyToOne]
        public Unit BaseUnit { get; set; }

        [ForeignKey(typeof(NomenclaturesKind))]
        public Guid NomenclaturesKindUid { get; set; }

        [ManyToOne]
        public NomenclaturesKind NomenclaturesKind { get; set; }

        public override bool Equals(object obj)
        {
            if (obj is Nomenclature)
                return ((Nomenclature) obj).Uid == Uid && ((Nomenclature) obj).Name == Name;

            return false;
        }
    }
}