using GildedRoseKata;
using GildedRoseKata.Configuration;
using GildedRoseKata.ItemUpdaters;
using Microsoft.Extensions.Options;
using Xunit;

namespace GildedRoseTests.UpdaterTests
{
    public class AgedBrieUpdaterTests
    {
        private readonly AgedBrieUpdater _updater;
        private readonly ConfigurationSettings _settings;

        public AgedBrieUpdaterTests()
        {
            _settings = new ConfigurationSettings
            {
                MaxQuality = 50,
                MinQuality = 0,
                NormalItemQualityReductionRateAfterSellIn = 2,
                NormalItemQualityReductionRateBeforeSellIn = 1,
                ConjuredItemRateMultiplier = 2,
            };

            _updater = new AgedBrieUpdater(Options.Create(_settings));
        }

        [Theory]
        [InlineData(10, 9)]
        [InlineData(-1, -2)]
        public void SellInIsReducedByOne(int sellIn, int expectedSellIn)
        {
            var item = new Item { Name = "Aged Brie", SellIn = sellIn, Quality = 20 };

            _updater.Update(item);

            Assert.Equal(expectedSellIn, item.SellIn);
        }

        // Quality Increase Tests
        [Theory]
        [InlineData(2, 20, 21)] // Below max quality, should increase by 1
        [InlineData(2, 50, 50)] // At max quality, should not increase
        [InlineData(-2, 45, 46)] // Below max quality, should increase by 1 even after sell date has passed
        [InlineData(0, 45, 46)] // At sell date, should increase by 1
        public void QualityIsIncreasedUntilMaxValue(int sellIn, int quality, int expectedQuality)
        {
            var item = new Item { Name = ItemNameConstants.AgedBrie, SellIn = sellIn, Quality = quality };

            _updater.Update(item);

            Assert.Equal(expectedQuality, item.Quality);
        }
    }

}