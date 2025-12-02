using GildedTros.App.ComplexClasses;
using System.Collections.Generic;
using System.Linq;

namespace GildedTros.App
{
    public class GildedTros
    {
        private int QualityDegradation = 1;
        private int QualityImprovement = 1;
        private int MaxQuality = 50;
        private int MaxQualityLegendary = 80;
        private string[] LegendaryItems = new[] { "B-DAWG Keychain" };
        private string[] ImprovementItems = new[] { "Good Wine" };

        IList<Item> Items;
        public GildedTros(IList<Item> Items)
        {
            this.Items = Items;
        }

        public void UpdateQuality()
        {
            for (var i = 0; i < Items.Count; i++)
            {
                if (LegendaryItems.Contains(Items[i].Name))
                {
                    ApplyPostUpdateRules(Items[i], true, false);
                    continue;
                }

                var qualityDegradation = QualityDegradation;
                var qualityImprovement = QualityImprovement;
                if (Items[i].SellIn <= 0)
                {
                    qualityDegradation = 2;
                    qualityImprovement = 2;
                }

                if (ImprovementItems.Contains(Items[i].Name))
                {
                    Items[i].Quality = Items[i].Quality + qualityImprovement;
                    ApplyPostUpdateRules(Items[i], false, true);
                    continue;
                }
                else
                {
                    if (Items[i].Quality <= 0)
                    {
                        ApplyPostUpdateRules(Items[i], false, false);
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
                        Items[i].Quality = Items[i].Quality + QualityImprovement;

                    ApplyPostUpdateRules(Items[i], false, false);
                    continue;
                }

                Items[i].Quality = Items[i].Quality - qualityDegradation;
                ApplyPostUpdateRules(Items[i], false, false);
            }
        }

        private void ApplyPostUpdateRules(Item item, bool isLegendary, bool isImprovement)
        {
            if (!isLegendary)
                item.SellIn = item.SellIn - 1;

            var maxQuality = LegendaryItems.Contains(item.Name) ? MaxQualityLegendary : MaxQuality;
            if (item.Quality > maxQuality)
                item.Quality = maxQuality;

            if (item.Quality < 0 && !isImprovement)
                item.Quality = 0;

            return;
        }
    }
}
