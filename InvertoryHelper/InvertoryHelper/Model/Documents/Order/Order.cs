using System;
using System.Collections.Generic;
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

        [ForeignKey(typeof(Storage))]
        public Guid StorageUid { get; set; }

        [ManyToOne(CascadeOperations = CascadeOperation.None)]
        public Storage Storage { get; set; }

        [OneToMany(CascadeOperations = CascadeOperation.None)]
        public List<OrderRow> OrderRows { get; set; }

        public string Comment { get; set; }

        public decimal Number { get; set; }

        public decimal Total { get; set; }
    }
}