using GildedTros.App.Interfaces;

namespace GildedTros.App.Classes
{
    public class ImprovementItem : Item, IQualityImprovement, IMaxQuality
    {
        public ImprovementItem(Item item)
        {
            Name = item.Name;
            SellIn = item.SellIn;
            Quality = item.Quality;
        }

        public int QualityImprovementPerDay { get; set; } = 1;
        public int QualityImprovementPerDayAfterSellIn { get; set; } = 2;
        public int MaxQuality { get; set; } = 50;

    }
}
