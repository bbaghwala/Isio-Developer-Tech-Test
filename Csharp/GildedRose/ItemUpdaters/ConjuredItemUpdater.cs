using GildedRoseKata.Configuration;
using Microsoft.Extensions.Options;
using System;

namespace GildedRoseKata.ItemUpdaters
{
    public class ConjuredItemUpdater : BaseItemUpdater
    {
        public ConjuredItemUpdater(IOptions<ConfigurationSettings> options) : base(options) { }

        public override void Update(Item item)
        {
            var qualityReductionRateAfterSellIn = _settings.NormalItemQualityReductionRateAfterSellIn * _settings.ConjuredItemRateMultiplier;
            var qualityReductionRateBeforeSellIn = _settings.NormalItemQualityReductionRateBeforeSellIn * _settings.ConjuredItemRateMultiplier;
            item.SellIn--;
            int degradeRate = item.SellIn < 0 ? qualityReductionRateAfterSellIn : qualityReductionRateBeforeSellIn;
            item.Quality = Math.Max(_settings.MinQuality, item.Quality - degradeRate);
        }
    }
}
