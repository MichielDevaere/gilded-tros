using GildedTros.App.Interfaces;

namespace GildedTros.App.Classes
{
    public class ComplexItem : Item, IMaxQuality
    {
        public ComplexItem(Item item)
        {
            Name = item.Name;
            SellIn = item.SellIn;
            Quality = item.Quality;
        }

        private int? _maxQuality;
        public int MaxQuality
        {
            get
            {
                if (!_maxQuality.HasValue)
                {
                    _maxQuality = Settings.GetMaxQuality(GetType().Name);
                }
                return _maxQuality.Value;
            }
            set => _maxQuality = value;
        }
    }
}
