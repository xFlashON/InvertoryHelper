using System;
using SQLite.Net.Attributes;
using SQLiteNetExtensions.Attributes;

namespace InvertoryHelper.Model
{
    [Table("Prices")]
    public class Price
    {
        [PrimaryKey]
        [Unique]
        [AutoIncrement]
        public Guid Uid { get; set; }

        public decimal price { get; set; }

        [ForeignKey(typeof(Nomenclature))]
        public Guid NomenclatureUid { get; set; }

        [ManyToOne(CascadeOperations = CascadeOperation.None)]
        public Nomenclature Nomenclature { get; set; }

        [ForeignKey(typeof(Characteristic))]
        public Guid CharacteristicUid { get; set; }

        [ManyToOne(CascadeOperations = CascadeOperation.None)]
        public Characteristic Characteristic { get; set; }

        public override bool Equals(object obj)
        {
            if (obj is Price)
                return ((Price) obj).Uid == Uid;

            return false;
        }
    }
}