using GildedTros.App.Interfaces;
using System.Linq;

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
