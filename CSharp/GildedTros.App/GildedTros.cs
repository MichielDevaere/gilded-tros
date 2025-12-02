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
                case ImprovementItem improvementItem:
                    ApplyQualityIncrease(improvementItem);
                    return;
                case TimeBasedQualityItem timeBasedQualityItem:
                    ApplyTimeBasedQualityRule(timeBasedQualityItem);
                    return;
            }

            if (item.Quality <= 0) return;

            item.Quality -= GetQualityDegradation(item);
        }

        private void ApplyQualityIncrease(Item item)
        {
            var settings = (item as ISettings)?.Settings;
            if (settings == null)
                return;

            item.Quality += GetQualityImprovement(item);
        }

        private void ApplyTimeBasedQualityRule(TimeBasedQualityItem item)
        {
            var rule = item.QualityRules
                .FirstOrDefault(r => item.SellIn <= r.DaysThreshold);

            if (rule != null)
            {
                if (rule.QualityChangePerDay.HasValue)
                    item.Quality += rule.QualityChangePerDay.Value;
                if (rule.AbsoluteQuality.HasValue)
                    item.Quality = rule.AbsoluteQuality.Value;
            }
            else
                ApplyQualityIncrease(item);
        }

        private void ApplyPostUpdateRules(Item item)
        {
            if (item is not LegendaryItem)
                item.SellIn--;

            var maxQuality = item as ISettings;
            var quality = maxQuality?.Settings.MaxQuality ?? _settings.MaxQuality;
            if (item.Quality > quality)
                item.Quality = quality;

            if (item.Quality < 0 && item is not ImprovementItem)
                item.Quality = 0;

            return;
        }

        private int GetQualityDegradation(Item item)
        {
            var settingsItem = item as ISettings;
            var qualityDegradation = settingsItem?.Settings?.Degradation?.BeforeSellInExpired ?? _settings.Degradation.BeforeSellInExpired;
            if (item.SellIn <= 0)
                qualityDegradation = settingsItem?.Settings?.Degradation?.AfterSellInExpired ?? _settings.Degradation.AfterSellInExpired;
            return qualityDegradation;
        }

        private int GetQualityImprovement(Item item)
        {
            var settingsItem = item as ISettings;
            var qualityDegradation = settingsItem?.Settings?.Improvement?.BeforeSellInExpired ?? _settings.Improvement.BeforeSellInExpired;
            if (item.SellIn <= 0)
                qualityDegradation = settingsItem?.Settings?.Improvement?.AfterSellInExpired ?? _settings.Improvement.AfterSellInExpired;
            return qualityDegradation;
        }
    }
}
