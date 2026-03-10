using GildedRoseKata.Configuration;
using Microsoft.Extensions.Options;

namespace GildedRoseKata.ItemUpdaters
{
    public class SulfurasUpdater : BaseItemUpdater
    {
        public SulfurasUpdater(IOptions<ConfigurationSettings> options) : base(options)
        {
        }
        public override void Update(Item item)
        {
            // Sulfuras does not change in quality or sellIn, so we do nothing here.
        }
    }
}
