using System;
using System.Collections.Generic;
using SQLite.Net.Attributes;
using SQLiteNetExtensions.Attributes;

namespace InvertoryHelper.Model
{
    [Table("NomenclaturesKinds")]
    public class NomenclaturesKind
    {
        [PrimaryKey]
        [Unique]
        [AutoIncrement]
        public Guid Uid { get; set; }

        [MaxLength(150)]
        public string Name { get; set; }

        [OneToMany]
        public List<Characteristic> CharacteristicsList { get; set; }

        public override string ToString()
        {
            return Name;
        }

        public override bool Equals(object obj)
        {
            if (obj is NomenclaturesKind)
                return (obj as NomenclaturesKind).Uid == Uid && (obj as NomenclaturesKind).Name == Name;
            return false;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}