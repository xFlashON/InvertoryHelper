using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite.Net.Attributes;
using SQLiteNetExtensions.Attributes;

namespace InvertoryHelper.Model.Documents.Order
{
    [Table("OrderRows")]
    public class OrderRow
    {
        [PrimaryKey]
        [Unique]
        [AutoIncrement]
        public Guid Uid { get; set; }

        [ForeignKey(typeof(Order))]
        public Guid OrderGuid { get; set; }

        [ManyToOne]
        public Order Order { get; set; }

        [ForeignKey(typeof(Nomenclature))]
        public Guid NomenclatureUid { get; set; }

        [ManyToOne]
        public Nomenclature Nomenclature { get; set; }

        [ForeignKey(typeof(Unit))]
        public Guid UnitUid { get; set; }

        [ManyToOne]
        public Unit Unit { get; set; }

        [ForeignKey(typeof(Characteristic))]
        public Guid CharacteristicUid { get; set; }

        [ManyToOne]
        public Characteristic Characteristic { get; set; }

        public Decimal Amount;
        public Decimal Price;
        public Decimal Total;

    }
}
