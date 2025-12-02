using GildedTros.App.Interfaces;
using System.Collections.Generic;

namespace GildedTros.App.Classes
{
    public class TimeBasedQualityItem : Item, IMaxQuality
    {
        public TimeBasedQualityItem(Item item)
        {
            Name = item.Name;
            SellIn = item.SellIn;
            Quality = item.Quality;
        }
        public List<QualityAdjustmentRule> QualityRules { get; set; } = new List<QualityAdjustmentRule>();
        public int MaxQuality => Settings.GetMaxQuality(nameof(TimeBasedQualityItem));
    }

    public class QualityAdjustmentRule
    {
        public int DaysThreshold { get; set; }
        public int? QualityChangePerDay { get; set; }
        public Operation? Operation { get; set; } = Classes.Operation.Add;
        public int? AbsoluteQuality { get; set; }
    }

    public enum Operation
    {
        Subtrack,
        Add
    }
}