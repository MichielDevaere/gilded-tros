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
                    ApplyTimeBasedQualityRule(timeBasedQualityItem);
                    return;
            }

            if (item.Quality <= 0) return;

            item.Quality -= GetQualityDegradation(item);
        }

        private void ApplyTimeBasedQualityRule(TimeBasedQualityItem item)
        {
            var rule = item.QualityRules
                .FirstOrDefault(r => item.SellIn <= r.DaysThreshold);

            if (rule != null)
            {
                if (rule.QualityChangePerDay.HasValue)
                    item.Quality += rule.QualityChangePerDay.Value * (rule.Operation == Operation.Add ? 1 : -1);
                if (rule.AbsoluteQuality.HasValue)
                    item.Quality = rule.AbsoluteQuality.Value;
            }
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
