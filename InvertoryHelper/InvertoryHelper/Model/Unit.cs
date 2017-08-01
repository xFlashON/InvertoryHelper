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

        public override string ToString()
        {
            return Name;
        }

        public override bool Equals(object obj)
        {
            if (obj is Unit)
                return (obj as Unit).Uid == Uid && (obj as Unit).Name == Name;
            return
                false;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}