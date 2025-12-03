using GildedTros.App.Interfaces;
using System.Collections.Generic;
using System.Data;

namespace GildedTros.App.Classes
{
    public class TimeBasedQualityItem : ComplexItem
    {
        public TimeBasedQualityItem(Item item) : base(item) { }

        public List<QualityAdjustmentRule> QualityRules { get; set; } = new List<QualityAdjustmentRule>();
    }

    public class QualityAdjustmentRule
    {
        public enum OperationEnum
        {
            Subtract,
            Add
        }
        public int From { get; set; }
        public int To { get; set; }

        public int? QualityChangePerDay { get; set; }
        public OperationEnum? Operation { get; set; } = OperationEnum.Add;
        public int? AbsoluteQuality { get; set; }

        public bool IsInRange(int sellIn)
        {
            return sellIn >= From && sellIn <= To;
        }

        public int CalculateQualityChange(int quality)
        {
            if (QualityChangePerDay.HasValue)
                quality += QualityChangePerDay.Value * (Operation == OperationEnum.Add ? 1 : -1);
            if (AbsoluteQuality.HasValue)
                quality = AbsoluteQuality.Value;

            return quality;
        }
    }
}