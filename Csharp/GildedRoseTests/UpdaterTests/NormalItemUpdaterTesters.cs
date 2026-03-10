using GildedRoseKata;
using GildedRoseKata.Configuration;
using GildedRoseKata.ItemUpdaters;
using Microsoft.Extensions.Options;
using Xunit;

namespace GildedRoseTests.UpdaterTests
{
        public class NormalItemUpdaterTesters
        {
            private readonly NormalItemUpdater _updater;
            private readonly ConfigurationSettings _settings;

            public NormalItemUpdaterTesters()
            {
                _settings = new ConfigurationSettings
                {
                    MaxQuality = 50,
                    MinQuality = 0,
                    NormalItemQualityReductionRateBeforeSellIn = 1,
                    NormalItemQualityReductionRateAfterSellIn = 2,
                    ConjuredItemRateMultiplier = 3
                };

                _updater = new NormalItemUpdater(Options.Create(_settings));
            }

            [Theory]
            [InlineData(10, 9)] // SellIn is reduced by 1
            [InlineData(0, -1)]// SellIn is reduced by 1 even when it goes negative
            public void SellInIsReduced(int sellIn, int expectedSellIn)
            {
                var item = new Item { Name = "Any Item Name", SellIn = sellIn, Quality = 20 };
                _updater.Update(item);
                Assert.Equal(expectedSellIn, item.SellIn);
            }

            [Theory]
            [InlineData(10, 20, 19)]// Before sell date, quality should degrade by 1
            [InlineData(-1, 20, 18)]// After sell date, quality should degrade by 2
            [InlineData(-1, 0, 0)] // Quality should never go below 0
            public void DegradesQualityByConfiguredRate(int sellIn, int quality, int expectedQuality)
            {
                var item = new Item { Name = "Any Item Name", SellIn = sellIn, Quality = quality };
                _updater.Update(item);
                Assert.Equal(expectedQuality, item.Quality);
            }
        }
}
