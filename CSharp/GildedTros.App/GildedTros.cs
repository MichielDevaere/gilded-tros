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
                    // TODO remove duplicate code
                    if (Items[i].Name != "B-DAWG Keychain")
                    {
                        Items[i].SellIn = Items[i].SellIn - 1;
                    }
                    // TODO remove duplicate code
                    if (Items[i].Quality > MaxQuality)
                    {
                        Items[i].Quality = MaxQuality;
                    }
                    continue;
                }
                else
                {
                    if (Items[i].Quality <= 0)
                    {
                        // TODO remove duplicate code
                        if (Items[i].Name != "B-DAWG Keychain")
                        {
                            Items[i].SellIn = Items[i].SellIn - 1;
                        }
                        continue;
                    }
                }

                if (Items[i].Name != "Backstage passes for Re:factor"
                    && Items[i].Name != "Backstage passes for HAXX")
                {
                    if (Items[i].Quality > 0)
                    {
                        if (Items[i].Name != "B-DAWG Keychain")
                        {
                            Items[i].Quality = Items[i].Quality - qualityDegradation;
                        }
                    }
                }
                else
                {
                    if (Items[i].Quality < 50)
                    {
                        Items[i].Quality = Items[i].Quality + 1;

                        if (Items[i].Name == "Backstage passes for Re:factor"
                        || Items[i].Name == "Backstage passes for HAXX")
                        {
                            if (Items[i].SellIn < 11)
                            {
                                Items[i].Quality = Items[i].Quality + 1;
                            }

                            if (Items[i].SellIn < 6)
                            {
                                Items[i].Quality = Items[i].Quality + 1;
                            }
                        }
                    }
                }

                if (Items[i].Name != "B-DAWG Keychain")
                {
                    Items[i].SellIn = Items[i].SellIn - 1;
                }

                if (Items[i].SellIn < 0)
                {
                    if (Items[i].Name != "Backstage passes for Re:factor"
                        && Items[i].Name != "Backstage passes for HAXX")
                    {
                        if (Items[i].Quality > 0)
                        {
                            if (Items[i].Name != "B-DAWG Keychain")
                            {
                                //Items[i].Quality = Items[i].Quality - 1;
                            }
                        }
                    }
                    else
                    {
                        Items[i].Quality = Items[i].Quality - Items[i].Quality;
                    }

                }

                if (Items[i].Quality < 0 && !ImprovementItems.Contains(Items[i].Name))
                {
                    Items[i].Quality = 0;
                }

                var maxQuality = LegendaryItems.Contains(Items[i].Name) ? MaxQualityLegendary : MaxQuality;
                if (Items[i].Quality > maxQuality)
                {
                    Items[i].Quality = maxQuality;
                }
            }
        }
    }
}
