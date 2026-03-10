using GildedRoseKata;
using GildedRoseKata.Configuration;
using GildedRoseKata.Interfaces;
using Microsoft.Extensions.Options;
using Moq;
using System.Collections.Generic;
using Xunit;

namespace GildedRoseTests
{
    public class GildedRoseTests
    {
        private readonly Mock<IItemUpdaterFactory> _factoryMock;
        private readonly Mock<IItemUpdater> _updaterMock;
        private readonly IOptions<ConfigurationSettings> _options;

        public GildedRoseTests()
        {
            _factoryMock = new Mock<IItemUpdaterFactory>();
            _updaterMock = new Mock<IItemUpdater>();

            _options = Options.Create(new ConfigurationSettings
            {
                MaxQuality = 50,
                MinQuality = 0,
                NormalItemQualityReductionRateAfterSellIn = 2,
                NormalItemQualityReductionRateBeforeSellIn = 1,
                ConjuredItemRateMultiplier = 2,
            });
        }

        [Fact]
        public void CallsUpdaterForEachItem()
        {
            var items = new List<Item>
        {
            new Item { Name = "Normal Item", SellIn = 10, Quality = 20 },
            new Item { Name = ItemNameConstants.AgedBrie,   SellIn = 5,  Quality = 10 }
        };

            _factoryMock
                .Setup(f => f.ItemUpdaterFor(It.IsAny<string>()))
                .Returns(_updaterMock.Object);
            var gildedRose = new GildedRose(_options, items, _factoryMock.Object);
            gildedRose.UpdateQuality();
            _factoryMock.Verify(f => f.ItemUpdaterFor(It.IsAny<string>()), Times.Exactly(2));
        }

        [Fact]
        public void CallsCorrectUpdaterForEachItemName()
        {

            var normalUpdater = new Mock<IItemUpdater>();
            var agedBrieUpdater = new Mock<IItemUpdater>();

            var items = new List<Item>
        {
            new Item { Name = "Normal Item", SellIn = 10, Quality = 20 },
            new Item { Name = ItemNameConstants.AgedBrie,   SellIn = 5,  Quality = 10 }
        };

            _factoryMock.Setup(f => f.ItemUpdaterFor("Normal Item")).Returns(normalUpdater.Object);
            _factoryMock.Setup(f => f.ItemUpdaterFor(ItemNameConstants.AgedBrie)).Returns(agedBrieUpdater.Object);
            var gildedRose = new GildedRose(_options, items, _factoryMock.Object);
            gildedRose.UpdateQuality();
            normalUpdater.Verify(u => u.Update(items[0]), Times.Once);
            agedBrieUpdater.Verify(u => u.Update(items[1]), Times.Once);
        }

        [Fact]
        public void NoExceptionIfItemListIsEmpty()
        {
            var items = new List<Item>();
            var gildedRose = new GildedRose(_options, items, _factoryMock.Object);
            gildedRose.UpdateQuality();
            _factoryMock.Verify(f => f.ItemUpdaterFor(It.IsAny<string>()), Times.Never);
        }
    }

}