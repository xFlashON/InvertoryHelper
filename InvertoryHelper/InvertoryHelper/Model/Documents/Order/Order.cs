using System;
using System.Collections.Generic;
using SQLite.Net.Attributes;
using SQLiteNetExtensions.Attributes;

namespace InvertoryHelper.Model.Documents.Order
{
    [Table("Orders")]
    public class Order
    {
        public string Comment;

        public decimal Number;

        public decimal Total;

        [PrimaryKey]
        [Unique]
        [AutoIncrement]
        public Guid Uid { get; set; }

        public DateTime Date { get; set; }

        [ForeignKey(typeof(Storage))]
        public Guid StorageUid { get; set; }

        [ManyToOne]
        public Storage Storage { get; set; }

        [OneToMany]
        public List<OrderRow> OrderRows { get; set; }
    }
}