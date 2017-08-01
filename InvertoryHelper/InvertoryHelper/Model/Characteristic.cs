using System;
using SQLite.Net.Attributes;
using SQLiteNetExtensions.Attributes;

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


        [ForeignKey(typeof(NomenclaturesKind))]
        public Guid NomenclaturesKindUid { get; set; }

        [ManyToOne]
        public NomenclaturesKind NomenclaturesKind { get; set; }
    }
}