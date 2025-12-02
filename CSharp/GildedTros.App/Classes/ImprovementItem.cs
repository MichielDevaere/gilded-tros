using GildedTros.App.Interfaces;

namespace GildedTros.App.Classes
{
    public class ImprovementItem : Item, ISettings
    {
        public ImprovementItem(Item item)
        {
            Name = item.Name;
            SellIn = item.SellIn;
            Quality = item.Quality;
        }
        public Settings Settings => Settings.GetSpecialItemSettings(nameof(ImprovementItem));

    }
}
