using GildedRoseKata;
using GildedRoseKata.Configuration;
using GildedRoseKata.ItemUpdaters;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace GildedRoseTests.UpdaterTests
{
    public class ConjuredItemUpdaterTests
    {
        private readonly ConjuredItemUpdater _updater;
        private readonly ConfigurationSettings _settings;

        public ConjuredItemUpdaterTests()
        {
            _settings = new ConfigurationSettings
            {
                MaxQuality = 50,
                MinQuality = 0,
                NormalItemQualityReductionRateBeforeSellIn = 1,
                NormalItemQualityReductionRateAfterSellIn = 2,
                ConjuredItemRateMultiplier = 3   
            };

            _updater = new ConjuredItemUpdater(Options.Create(_settings));
        }

        [Theory]
        [InlineData(10, 9)] // SellIn is reduced by 1
        [InlineData(0, -1)]// SellIn is reduced by 1 even when it goes negative
        public void SellInIsReduced(int sellIn, int expectedSellIn)
        {
            var item = new Item { Name = "Conjured Mana Cake", SellIn = sellIn, Quality = 20 };
            _updater.Update(item);
            Assert.Equal(expectedSellIn, item.SellIn);
        }

        [Theory]
        [InlineData(10, 20, 17)]// Before sell date, quality should degrade by 3 (1 * 3)
        [InlineData(-1, 20, 14)]// After sell date, quality should degrade by 6 (2 * 3)
        [InlineData(-1, 0, 0)] // Quality should never go below 0
        public void DegradesQualityByConfiguredRate(int sellIn, int quality, int expectedQuality)
        {
            var item = new Item { Name = "Conjured Mana Cake", SellIn = sellIn, Quality = quality };

            _updater.Update(item);

            Assert.Equal(expectedQuality, item.Quality); 
        }
    }
}
