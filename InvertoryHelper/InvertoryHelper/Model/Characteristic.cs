using System;
using SQLite.Net.Attributes;

namespace InvertoryHelper.Model
{
    [Table("Characteristics")]
    public class Characteristic
    {
        [PrimaryKey]
        [Unique]
        [AutoIncrement]
        public Guid Uid { get; set; }

        [MaxLength(150)]
        public string Name { get; set; }
    }
}