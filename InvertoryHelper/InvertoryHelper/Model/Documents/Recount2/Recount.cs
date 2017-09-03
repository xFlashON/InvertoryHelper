using System;
using System.Collections.Generic;
using SQLite.Net.Attributes;
using SQLiteNetExtensions.Attributes;

namespace InvertoryHelper.Model.Documents.Recount
{
    [Table("Recounts")]
    public class Recount
    {
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
        public List<RecountRow> RecountRows { get; set; }

        public string Comment { get; set; }

        public decimal Number { get; set; }

        public decimal Total { get; set; }
    }
}