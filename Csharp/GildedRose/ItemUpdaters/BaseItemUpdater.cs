using GildedRoseKata.Configuration;
using GildedRoseKata.Interfaces;
using Microsoft.Extensions.Options;

namespace GildedRoseKata.ItemUpdaters
{
    public abstract class BaseItemUpdater : IItemUpdater
    {
        protected readonly ConfigurationSettings _settings;

        protected BaseItemUpdater(IOptions<ConfigurationSettings> options)
        {
            _settings = options.Value;
        }

        public abstract void Update(Item item);
    }
}
