using GildedTros.App.Interfaces;
using System.Collections.Generic;

namespace GildedTros.App.Classes
{
    public class TimeBasedQualityItem : ComplexItem
    {
        public TimeBasedQualityItem(Item item) : base(item) { }

        public List<QualityAdjustmentRule> QualityRules { get; set; } = new List<QualityAdjustmentRule>();
    }

    public class QualityAdjustmentRule
    {
        public int From { get; set; }
        public int To { get; set; }

        public int? QualityChangePerDay { get; set; }
        public Operation? Operation { get; set; } = Classes.Operation.Add;
        public int? AbsoluteQuality { get; set; }
    }

    public enum Operation
    {
        Subtract,
        Add
    }
}