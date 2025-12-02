using GildedTros.App.Interfaces;

namespace GildedTros.App.Classes
{
    public class LegendaryItem : Item, ISettings
    {
        public LegendaryItem(Item item)
        {
            Name = item.Name;
            SellIn = item.SellIn;
            Quality = item.Quality;
        }

        public Settings Settings => Settings.GetSpecialItemSettings(nameof(LegendaryItem));
    }
}
