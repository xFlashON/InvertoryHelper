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

        [OneToOne]
        public Nomenclature Nomenclature { get; set; }

        [ForeignKey(typeof(Characteristic))]
        public Guid CharacteristicUid { get; set; }

        [OneToOne]
        public Characteristic Characteristic { get; set; }
    }
}