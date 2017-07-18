using System;
using SQLite.Net.Attributes;

namespace InvertoryHelper.Model
{
    [Table("Units")]
    public class Unit
    {
        [PrimaryKey]
        [Unique]
        [AutoIncrement]
        public Guid Uid { get; set; }

        [MaxLength(50)]
        public string Name { get; set; }
    }
}