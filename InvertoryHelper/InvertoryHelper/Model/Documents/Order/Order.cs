using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite.Net.Attributes;
using SQLiteNetExtensions.Attributes;

namespace InvertoryHelper.Model.Documents.Order
{
    [Table("Orders")]
    public class Order
    {
        [PrimaryKey]
        [Unique]
        [AutoIncrement]
        public Guid Uid { get; set; }

        public DateTime Date { get; set; }

        public Decimal Number;

        public String Comment;

        public Decimal Total;

        [ForeignKey(typeof(Storage))]
        public Guid StorageUid { get; set; }

        [ManyToOne]
        public Storage Storage { get; set; }

        [OneToMany]
        public List<OrderRow> OrderRows { get; set; } 

    }
}
