using GildedRoseKata.Configuration;
using GildedRoseKata.Interfaces;
using GildedRoseKata.ItemUpdaters;
using Microsoft.Extensions.Options;

namespace GildedRoseKata.Factory
{
    public class ItemUpdaterFactory : IItemUpdaterFactory
    {
        private readonly IOptions<ConfigurationSettings> _options;

        public ItemUpdaterFactory(IOptions<ConfigurationSettings> options)
        {
            _options = options;
        }

        public IItemUpdater ItemUpdaterFor(string itemName)
        {
            switch (itemName)
            {
                case ItemNameConstants.AgedBrie:
                    return new AgedBrieUpdater(_options);
                case ItemNameConstants.Sulfuras:
                    return new SulfurasUpdater(_options);
                case ItemNameConstants.BackstagePassesToConcert:
                    return new BackstagePassUpdater(_options);
                case ItemNameConstants.ConjuredItem:
                    return new ConjuredItemUpdater(_options);
                default:
                    return new NormalItemUpdater(_options);
            }
        }
    }
}
