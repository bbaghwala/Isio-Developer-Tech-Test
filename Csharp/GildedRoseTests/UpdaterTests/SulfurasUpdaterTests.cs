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
    public class SulfurasUpdaterTests
    {
        private readonly SulfurasUpdater _updater;
        private readonly ConfigurationSettings _settings;

        public SulfurasUpdaterTests()
        {
            _settings = new ConfigurationSettings();
            _updater = new SulfurasUpdater(Options.Create(_settings));
        }

        [Theory]
        [InlineData(10, 10)] // SellIn should not change    
        [InlineData(0, 0)] // SellIn should not change even at 0
        [InlineData(-1, -1)] // SellIn should not change even when negative
        public void SellInIsReduced(int sellIn, int expectedSellIn)
        {
            var item = new Item { Name = ItemNameConstants.Sulfuras, SellIn = sellIn, Quality = 20 };
            _updater.Update(item);
            Assert.Equal(expectedSellIn, item.SellIn);
        }

        [Theory]
        [InlineData(10, 20, 20)]// Quality should not change before sell date
        [InlineData(-1, 18, 18)]// Quality should not change after sell date
        [InlineData(-1, 50, 50)]// Quality should not change even at max quality
        public void DegradesQualityByConfiguredRate(int sellIn, int quality, int expectedQuality)
        {
            var item = new Item { Name = ItemNameConstants.Sulfuras, SellIn = sellIn, Quality = quality };
            _updater.Update(item);
            Assert.Equal(expectedQuality, item.Quality);
        }
    }
}
