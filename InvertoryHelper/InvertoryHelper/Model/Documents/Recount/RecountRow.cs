﻿using System;
using SQLite.Net.Attributes;
using SQLiteNetExtensions.Attributes;

namespace InvertoryHelper.Model.Documents.Recount
{
    [Table("RecountRows")]
    public class RecountRow
    {
        [PrimaryKey]
        [Unique]
        [AutoIncrement]
        public Guid Uid { get; set; }

        [NotNull]
        [ForeignKey(typeof(Recount))]
        public Guid RecountUid { get; set; }

        [ManyToOne]
        public Recount Recount { get; set; }

        [ForeignKey(typeof(Nomenclature))]
        public Guid NomenclatureUid { get; set; }

        [ManyToOne]
        public Nomenclature Nomenclature { get; set; }

        [ForeignKey(typeof(Unit))]
        public Guid UnitUid { get; set; }

        [ManyToOne]
        public Unit Unit { get; set; }

        [ForeignKey(typeof(Characteristic))]
        public Guid CharacteristicUid { get; set; }

        [ManyToOne]
        public Characteristic Characteristic { get; set; }

        public decimal Amount { get; set; }
        public decimal Price { get; set; }
        public decimal Total { get; set; }
    }
}