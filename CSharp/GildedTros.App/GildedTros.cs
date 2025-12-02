using GildedTros.App.Classes;
using GildedTros.App.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace GildedTros.App
{
    public class GildedTros
    {
        private Settings _settings = Settings.GetSettings();

        IList<Item> Items;
        public GildedTros(IList<Item> Items)
        {
            this.Items = Items;
        }

        public void UpdateQuality()
        {
            foreach (var item in Items)
            {
                if (item is LegendaryItem)
                {
                    ApplyPostUpdateRules(item);
                    continue;
                }

                var settingsItem = item as ISettings;
                var qualityDegradation = settingsItem?.Settings?.Degradation?.BeforeSellInExpired ?? _settings.Degradation.BeforeSellInExpired;
                if (item.SellIn <= 0)
                    qualityDegradation = settingsItem?.Settings?.Degradation?.AfterSellInExpired ?? _settings.Degradation.AfterSellInExpired;

                if (item is ImprovementItem improvementItem)
                {
                    ApplyQualityIncrease(item);
                    ApplyPostUpdateRules(item);
                    continue;
                }
                else
                {
                    if (item.Quality <= 0)
                    {
                        ApplyPostUpdateRules(item);
                        continue;
                    }
                }

                if (item is TimeBasedQualityItem timeBasedQualityItem)
                {
                    var rule = timeBasedQualityItem.QualityRules.FirstOrDefault(r => item.SellIn <= r.DaysThreshold);
                    if (rule != null)
                    {
                        if (rule.QualityChangePerDay != null)
                            item.Quality += rule.QualityChangePerDay.Value;
                        if (rule.AbsoluteQuality != null)
                            item.Quality = rule.AbsoluteQuality.Value;
                    }
                    else
                        ApplyQualityIncrease(item);

                    ApplyPostUpdateRules(item);
                    continue;
                }

                item.Quality = item.Quality - qualityDegradation;
                ApplyPostUpdateRules(item);
            }
        }

        private void ApplyPostUpdateRules(Item item)
        {
            var legendaryItem = item as LegendaryItem;

            if (legendaryItem == null)
                item.SellIn = item.SellIn - 1;

            var maxQuality = item as ISettings;
            var quality = maxQuality?.Settings.MaxQuality ?? _settings.MaxQuality;
            if (item.Quality > quality)
                item.Quality = quality;

            if (item.Quality < 0 && item is not ImprovementItem)
                item.Quality = 0;

            return;
        }

        private void ApplyQualityIncrease(Item item)
        {
            var settings = (item as ISettings)?.Settings;
            if (settings == null)
                return;

            if (item.SellIn <= 0)
                item.Quality = item.Quality + settings.Improvement.AfterSellInExpired;
            else
                item.Quality = item.Quality + settings.Improvement.BeforeSellInExpired;
        }
    }
}
