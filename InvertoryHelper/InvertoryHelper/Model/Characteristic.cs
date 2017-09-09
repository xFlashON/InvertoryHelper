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


        [NotNull]
        [ForeignKey(typeof(NomenclaturesKind))]
        public Guid NomenclaturesKindUid { get; set; }

        [ManyToOne(CascadeOperations = CascadeOperation.None)]
        public NomenclaturesKind NomenclaturesKind { get; set; }

        public override string ToString()
        {
            return Name;
        }

        public override bool Equals(object obj)
        {
            if (obj is Characteristic)
                return (obj as Characteristic).Uid == Uid && (obj as Characteristic).Name == Name;
            return
                false;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}