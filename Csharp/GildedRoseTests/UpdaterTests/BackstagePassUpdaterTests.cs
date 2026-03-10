using GildedRoseKata;
using GildedRoseKata.Configuration;
using GildedRoseKata.ItemUpdaters;
using Microsoft.Extensions.Options;
using Xunit;

namespace GildedRoseTests.UpdaterTests
{
    public class BackstagePassUpdaterTests
    {
        private readonly BackstagePassUpdater _updater;
        private readonly ConfigurationSettings _settings;

        public BackstagePassUpdaterTests()
        {
            _settings = new ConfigurationSettings
            {
                MaxQuality = 50,
                MinQuality = 0,
                NormalItemQualityReductionRateAfterSellIn = 2,
                NormalItemQualityReductionRateBeforeSellIn = 1,
                ConjuredItemRateMultiplier = 2,
            };

            _updater = new BackstagePassUpdater(Options.Create(_settings));
        }

        [Theory]
        [InlineData(10, 9)]
        [InlineData(-1, -2)]
        public void SellInIsReducedByOne(int sellIn, int expectedSellIn)
        {
            var item = new Item { Name = ItemNameConstants.BackstagePassesToConcert, SellIn = sellIn, Quality = 20 };

            _updater.Update(item);

            Assert.Equal(expectedSellIn, item.SellIn);
        }

        // Quality Increase Tests
        [Theory]
        [InlineData(8, 49, 50)] // Below max quality, should increase by 1
        [InlineData(1, 49, 50)] // At sell date, should increase by 1
        [InlineData(-1, 45, 0)] // After sell date, quality should drop to 0
        public void QualityIsIncreasedUntilMaxValueOrSellInIsReached(int sellIn, int quality, int expectedQuality)
        {
            var item = new Item { Name = ItemNameConstants.BackstagePassesToConcert, SellIn = sellIn, Quality = quality };

            _updater.Update(item);

            Assert.Equal(expectedQuality, item.Quality);
        }
    }
}
