using GildedTros.App.Interfaces;

namespace GildedTros.App.Classes
{
    public class LegendaryItem : Item, IMaxQuality
    {
        public LegendaryItem(Item item)
        {
            Name = item.Name;
            SellIn = item.SellIn;
            Quality = item.Quality;
        }

        public int MaxQuality => Settings.GetMaxQuality(nameof(LegendaryItem));
    }
}
