using GildedRoseKata.Configuration;
using GildedRoseKata.Interfaces;
using Microsoft.Extensions.Options;

namespace GildedRoseKata.ItemUpdaters
{
    public class AgedBrieUpdater: IItemUpdater
    {
        private readonly ConfigurationSettings _settings;
        
        public AgedBrieUpdater(IOptions<ConfigurationSettings> options) 
        {
            _settings = options.Value;
        }

        public void Update(Item item)
        {
            item.SellIn--;
            if (item.Quality < _settings.MaxQuality)
            {
                item.Quality++;
            }
        }
    }
}
