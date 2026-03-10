using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GildedRoseKata.Configuration
{
    public class ConfigurationSettings
    {
        // These settings will be loaded from appsettings.json.
        // we are setting some default values here so that if the configuration is missing, the application can still run with reasonable defaults.
        public int MaxQuality { get; set; } = 40;
        public int MinQuality { get; set; } = 0;
        public int NormalItemQualityReductionRateAfterSellIn { get; set; } = 2;
        public int NormalItemQualityReductionRateBeforeSellIn { get; set; } = 1;
        public int ConjuredItemRateMultiplier {  get; set; } = 2;


    }
}
