using GildedRoseKata.Configuration;
using GildedRoseKata.Interfaces;
using Microsoft.Extensions.Options;
using System.Collections.Generic;

namespace GildedRoseKata;

public class GildedRose
{
    
    private readonly ICollection<Item> _items;
    private readonly ConfigurationSettings _settings;
    private IItemUpdaterFactory _itemUpdaterFactory;
    public GildedRose(
        IOptions<ConfigurationSettings> options, 
        ICollection<Item> items,
        IItemUpdaterFactory itemUpdaterFactory)
    {
        _settings = options.Value;
        _items = items;
        _itemUpdaterFactory = itemUpdaterFactory;
    }

    public void UpdateQuality()
    {
        foreach (var item in _items)
        {
            var updater = _itemUpdaterFactory.ItemUpdaterFor(item.Name);
            updater.Update(item);
        }
    }
}