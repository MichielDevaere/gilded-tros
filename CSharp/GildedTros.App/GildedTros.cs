using GildedTros.App.Classes;
using GildedTros.App.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace GildedTros.App
{
    public class GildedTros
    {
        private int QualityDegradation = 1;
        private int MaxQuality = 50;

        IList<Item> Items;
        public GildedTros(IList<Item> Items)
        {
            this.Items = Items;
        }

        public void UpdateQuality()
        {
            for (var i = 0; i < Items.Count; i++)
            {
                if (Items[i] is LegendaryItem)
                {
                    ApplyPostUpdateRules(Items[i]);
                    continue;
                }

                var qualityDegradation = QualityDegradation;
                if (Items[i].SellIn <= 0)
                    qualityDegradation = 2;

                if (Items[i] is ImprovementItem improvementItem)
                {
                    ApplyQualityIncrease(Items[i]);
                    ApplyPostUpdateRules(Items[i]);
                    continue;
                }
                else
                {
                    if (Items[i].Quality <= 0)
                    {
                        ApplyPostUpdateRules(Items[i]);
                        continue;
                    }
                }

                if (Items[i] is TimeBasedQualityItem timeBasedQualityItem)
                {
                    var rule = timeBasedQualityItem.QualityRules.FirstOrDefault(r => Items[i].SellIn <= r.DaysThreshold);
                    if (rule != null)
                    {
                        if (rule.QualityChangePerDay != null)
                            Items[i].Quality += rule.QualityChangePerDay.Value;
                        if (rule.AbsoluteQuality != null)
                            Items[i].Quality = rule.AbsoluteQuality.Value;
                    }
                    else
                        ApplyQualityIncrease(Items[i]);

                    ApplyPostUpdateRules(Items[i]);
                    continue;
                }

                Items[i].Quality = Items[i].Quality - qualityDegradation;
                ApplyPostUpdateRules(Items[i]);
            }
        }

        private void ApplyPostUpdateRules(Item item)
        {
            var legendaryItem = item as LegendaryItem;

            if (legendaryItem == null)
                item.SellIn = item.SellIn - 1;

            var maxQuality = item as IMaxQuality;
            var quality = legendaryItem?.MaxQuality ?? maxQuality?.MaxQuality ?? MaxQuality;
            if (item.Quality > quality)
                item.Quality = quality;

            if (item.Quality < 0 && !(item is ImprovementItem))
                item.Quality = 0;

            return;
        }

        private void ApplyQualityIncrease(Item item)
        {
            var qualityImprovement = item as IQualityImprovement;
            if (qualityImprovement == null)
                return;

            if (item.SellIn <= 0)
                item.Quality = item.Quality + qualityImprovement.QualityImprovementPerDayAfterSellIn;
            else
                item.Quality = item.Quality + qualityImprovement.QualityImprovementPerDay;
        }
    }
}
