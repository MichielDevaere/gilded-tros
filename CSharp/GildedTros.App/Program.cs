using GildedTros.App.Classes;
using System;
using System.Collections.Generic;

namespace GildedTros.App
{
    class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("OMGHAI!");

            IList<Item> Items = new List<Item>{
                new() {Name = "Ring of Cleansening Code", SellIn = 10, Quality = 20},
                new ImprovementItem(new Item { Name = "Good Wine", SellIn = 2, Quality = 0 }),
                new() {Name = "Elixir of the SOLID", SellIn = 5, Quality = 7},
                new LegendaryItem(new Item { Name = "B-DAWG Keychain", SellIn = 0, Quality = 80 }),
                new LegendaryItem(new Item { Name = "B-DAWG Keychain", SellIn = -1, Quality = 80 }),
                new TimeBasedQualityItem(new Item { Name = "Backstage passes for Re:factor", SellIn = 15, Quality = 20 })
                {
                    QualityRules = new List<QualityAdjustmentRule>
                        {
                            new() { DaysThreshold = 0, AbsoluteQuality = 0 },
                            new() { DaysThreshold = 5, QualityChangePerDay = 3 },
                            new() { DaysThreshold = 10, QualityChangePerDay = 2 }
                        }
                },
                new TimeBasedQualityItem(new Item { Name = "Backstage passes for Re:factor", SellIn = 10, Quality = 49 })
                {
                    QualityRules = new List<QualityAdjustmentRule>
                        {
                            new() { DaysThreshold = 0, AbsoluteQuality = 0 },
                            new() { DaysThreshold = 5, QualityChangePerDay = 3 },
                            new() { DaysThreshold = 10, QualityChangePerDay = 2 }
                        }
                },
                new TimeBasedQualityItem(new Item { Name = "Backstage passes for HAXX", SellIn = 5, Quality = 49 })
                {
                    QualityRules = new List<QualityAdjustmentRule>
                        {
                            new() { DaysThreshold = 0, AbsoluteQuality = 0 },
                            new() { DaysThreshold = 5, QualityChangePerDay = 3 },
                            new() { DaysThreshold = 10, QualityChangePerDay = 2 }
                        }
                },
                // these smelly items do not work properly yet
                new() {Name = "Duplicate Code", SellIn = 3, Quality = 6},
                new() {Name = "Long Methods", SellIn = 3, Quality = 6},
                new() {Name = "Ugly Variable Names", SellIn = 3, Quality = 6}
            };

            var app = new GildedTros(Items);


            for (var i = 0; i < 31; i++)
            {
                Console.WriteLine("-------- day " + i + " --------");
                Console.WriteLine("name, sellIn, quality");
                for (var j = 0; j < Items.Count; j++)
                {
                    System.Console.WriteLine(Items[j].Name + ", " + Items[j].SellIn + ", " + Items[j].Quality);
                }
                Console.WriteLine("");
                app.UpdateQuality();
            }
        }
    }
}
