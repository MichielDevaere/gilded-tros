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

        private int? _maxQuality;
        public int MaxQuality
        {
            get => _maxQuality ?? Settings.GetMaxQuality(nameof(TimeBasedQualityItem));
            set => _maxQuality = value;
        }
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
        Subtract,
        Add
    }
}