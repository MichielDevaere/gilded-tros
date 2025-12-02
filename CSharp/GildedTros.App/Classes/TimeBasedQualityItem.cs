using GildedTros.App.Interfaces;
using System.Collections.Generic;

namespace GildedTros.App.Classes
{
    public class TimeBasedQualityItem : Item, ISettings
    {
        public TimeBasedQualityItem(Item item)
        {
            Name = item.Name;
            SellIn = item.SellIn;
            Quality = item.Quality;
        }
        public List<QualityAdjustmentRule> QualityRules { get; set; } = new List<QualityAdjustmentRule>();
        public Settings Settings => Settings.GetSpecialItemSettings(nameof(TimeBasedQualityItem));
    }

    public class QualityAdjustmentRule
    {
        public int DaysThreshold { get; set; }
        public int? QualityChangePerDay { get; set; }
        public int? AbsoluteQuality { get; set; }
    }
}