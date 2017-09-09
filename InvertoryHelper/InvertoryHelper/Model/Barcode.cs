using System;
using SQLite.Net.Attributes;
using SQLiteNetExtensions.Attributes;

namespace InvertoryHelper.Model
{
    [Table("Barcodes")]
    public class Barcode
    {
        [PrimaryKey]
        [Unique]
        [AutoIncrement]
        public Guid Uid { get; set; }

        [Indexed]
        [MaxLength(50)]
        public string Code { get; set; }

        [ForeignKey(typeof(Nomenclature))]
        public Guid NomenclatureUid { get; set; }

        [ManyToOne(CascadeOperations = CascadeOperation.None)]
        public Nomenclature Nomenclature { get; set; }

        [ForeignKey(typeof(Unit))]
        public Guid UnitUid { get; set; }

        [ManyToOne(CascadeOperations = CascadeOperation.None)]
        public Unit Unit { get; set; }

        [ForeignKey(typeof(Characteristic))]
        public Guid CharacteristicUid { get; set; }

        [ManyToOne(CascadeOperations = CascadeOperation.None)]
        public Characteristic Characteristic { get; set; }

        public override bool Equals(object obj)
        {
            if (obj is Barcode)
                return ((Barcode) obj).Uid == Uid && ((Barcode) obj).Code == Code;

            return false;
        }
    }
}