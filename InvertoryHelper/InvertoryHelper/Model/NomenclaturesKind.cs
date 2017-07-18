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
    }
}