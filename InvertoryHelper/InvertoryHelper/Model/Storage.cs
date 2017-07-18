using System;
using SQLite.Net.Attributes;

namespace InvertoryHelper.Model
{
    [Table("Storages")]
    public class Storage
    {
        [PrimaryKey]
        [Unique]
        [AutoIncrement]
        public Guid Uid { get; set; }

        [MaxLength(100)]
        public string Name { get; set; }
    }
}