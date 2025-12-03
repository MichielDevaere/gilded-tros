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

        private int? _maxQuality;
        public int MaxQuality
        {
            get => _maxQuality ?? Settings.GetMaxQuality(nameof(LegendaryItem));
            set => _maxQuality = value;
        }
    }
}
