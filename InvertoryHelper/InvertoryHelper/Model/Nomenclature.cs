using System;
using InvertoryHelper.Common;
using SQLite.Net.Attributes;
using SQLiteNetExtensions.Attributes;

namespace InvertoryHelper.Model
{
    [Table("Nomenclatures")]
    public class Nomenclature
    {
        [PrimaryKey]
        [Unique]
        [AutoIncrement]
        public Guid Uid { get; set; }

        [MaxLength(150)]
        public string Name { get; set; }

        [ForeignKey(typeof(Unit))]
        public Guid BaseUnitUid { get; set; }

        [OneToOne]
        public Unit BaseUnit { get; set; }

        [ForeignKey(typeof(NomenclaturesKind))]
        public Guid NomenclaturesKindUid { get; set; }

        [OneToOne]
        public NomenclaturesKind NomenclaturesKind { get; set; }
    }
}