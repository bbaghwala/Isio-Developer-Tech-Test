using GildedRoseKata.Configuration;
using GildedRoseKata.Interfaces;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GildedRoseKata.ItemUpdaters
{
    public class BackstagePassUpdater : BaseItemUpdater
    {
        
        public BackstagePassUpdater(IOptions<ConfigurationSettings> options) : base(options) { }
        

        public override void Update(Item item)
        {
            item.SellIn--;
            if (item.SellIn < 0)
            {
                item.Quality = _settings.MinQuality; 
            }
            else if (item.SellIn <= 2 )
            {
                item.Quality = Math.Min(item.Quality + 4, _settings.MaxQuality); 
            }
            else if (item.SellIn <= 7)
            {
                item.Quality = Math.Min(item.Quality + 3, _settings.MaxQuality); 
            }
            else
            {
                item.Quality = Math.Min(item.Quality + 1, _settings.MaxQuality);
            }
        }
    }
}
