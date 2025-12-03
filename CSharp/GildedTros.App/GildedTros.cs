using GildedTros.App.Classes;
using GildedTros.App.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace GildedTros.App
{
    public class GildedTros
    {
        private readonly Settings _settings = Settings.GetSettings();

        private readonly IList<Item> Items;
        public GildedTros(IList<Item> Items)
        {
            this.Items = Items;
        }

        public void UpdateQuality()
        {
            foreach (var item in Items)
            {
                UpdateQualityOnItem(item);
                ApplyPostUpdateRules(item);
            }
        }

        private void UpdateQualityOnItem(Item item)
        {
            switch (item)
            {
                case LegendaryItem legendaryItem:
                    return;
                case TimeBasedQualityItem timeBasedQualityItem:
                    var skip = ApplyTimeBasedQualityRule(timeBasedQualityItem);
                    if (skip) 
                        return;
                    break;
            }

            if (item.Quality <= 0) 
                return;

            item.Quality -= GetQualityDegradation(item);
        }

        private static bool ApplyTimeBasedQualityRule(TimeBasedQualityItem item)
        {
            var rule = item.QualityRules.FirstOrDefault(r => r.IsInRange(item.SellIn));
            if (rule == null) return false;
            item.Quality = rule.CalculateQualityChange(item.Quality);
            return true;
        }

        private void ApplyPostUpdateRules(Item item)
        {
            if (item is not LegendaryItem)
                item.SellIn--;

            var maxQuality = item as IMaxQuality;
            var quality = maxQuality?.MaxQuality ?? _settings.MaxQuality;
            if (item.Quality > quality)
                item.Quality = quality;

            if (item.Quality < 0)
                item.Quality = 0;

            return;
        }

        private int GetQualityDegradation(Item item)
        {
            if (item.SellIn <= 0)
                return _settings.DefaultDegradation.AfterSellInExpired;
            return _settings.DefaultDegradation.BeforeSellInExpired;
        }
    }
}
