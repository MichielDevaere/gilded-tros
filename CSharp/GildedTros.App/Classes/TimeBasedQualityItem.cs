using GildedTros.App.Interfaces;
using System.Collections.Generic;

namespace GildedTros.App.Classes
{
    public class TimeBasedQualityItem : Item, IQualityImprovement
    {
        public TimeBasedQualityItem(Item item)
        {
            Name = item.Name;
            SellIn = item.SellIn;
            Quality = item.Quality;
        }
        public List<QualityAdjustmentRule> QualityRules { get; set; } = new List<QualityAdjustmentRule>();
        public int QualityImprovementPerDay { get; set; } = 1;
        public int QualityImprovementPerDayAfterSellIn { get; set; } = 1;
    }

    public class QualityAdjustmentRule
    {
        public int DaysThreshold { get; set; }
        public int? QualityChangePerDay { get; set; }
        public int? AbsoluteQuality { get; set; }
    }
}