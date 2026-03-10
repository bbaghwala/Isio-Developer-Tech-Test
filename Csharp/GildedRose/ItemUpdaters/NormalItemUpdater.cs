using GildedRoseKata.Configuration;
using GildedRoseKata.Interfaces;
using Microsoft.Extensions.Options;

namespace GildedRoseKata.ItemUpdaters
{
    public class NormalItemUpdater: BaseItemUpdater
    {
        
        public NormalItemUpdater(IOptions<ConfigurationSettings> options) : base(options) { }
        

        public override void Update(Item item)
        {
            item.SellIn--;
            int degradeRate = item.SellIn < 0 ? _settings.NormalItemQualityReductionRateAfterSellIn  : _settings.NormalItemQualityReductionRateBeforeSellIn;
            if(item.Quality > _settings.MinQuality && item.Quality < _settings.MaxQuality)
            {
                item.Quality = item.Quality - degradeRate;
            }
            
        }
    }
}
