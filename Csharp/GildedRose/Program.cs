using GildedRoseKata.Configuration;
using GildedRoseKata.Factory;
using GildedRoseKata.Interfaces;
using GildedRoseKata.ItemUpdaters;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace GildedRoseKata;

public class Program
{
    public static void Main(string[] args)
    {
        var host = Host.CreateDefaultBuilder(args)
            .UseContentRoot(AppContext.BaseDirectory)
            .ConfigureServices((context, services) =>
            {
                services.Configure<ConfigurationSettings>(context.Configuration.GetSection("ConfigurationSettings"));

                services.AddTransient<IItemUpdater, AgedBrieUpdater>();
                services.AddTransient<IItemUpdater, BackstagePassUpdater>();
                services.AddTransient<IItemUpdater, ConjuredItemUpdater>();
                services.AddTransient<IItemUpdater, NormalItemUpdater>();
                services.AddTransient<IItemUpdater, SulfurasUpdater>();
                services.AddTransient<IItemUpdaterFactory, ItemUpdaterFactory>(); 
                services.AddSingleton<ICollection<Item>>(sp => InitializeItems());
                services.AddTransient<GildedRose>();

            })
            .Build();

        var items = host.Services.GetRequiredService<ICollection<Item>>();
        Console.WriteLine("OMGHAI!");
        var app = host.Services.GetRequiredService<GildedRose>();

        int days = 10;
        if (args.Length > 0)
        {
            days = int.Parse(args[0]) + 1;
        }

        for (var i = 0; i < days; i++)
        {
            Console.WriteLine("-------- day " + i + " --------");
            Console.WriteLine("name, sellIn, quality");
            foreach (var item in items)
            {
                Console.WriteLine(item.Name + ", " + item.SellIn + ", " + item.Quality);
            }

            Console.WriteLine("");
            app.UpdateQuality();
        }
    }

    
    private static ICollection<Item> InitializeItems()
    {
        return new Collection<Item>
        {
            new Item {Name = "+5 Dexterity Vest", SellIn = 10, Quality = 20},
            new Item {Name = "Aged Brie", SellIn = 2, Quality = 0},
            new Item {Name = "Elixir of the Mongoose", SellIn = 5, Quality = 7},
            new Item {Name = "Sulfuras, Hand of Ragnaros", SellIn = 0, Quality = 80},
            new Item {Name = "Sulfuras, Hand of Ragnaros", SellIn = -1, Quality = 80},
            new Item
            {
                Name = "Backstage passes to a TAFKAL80ETC concert",
                SellIn = 15,
                Quality = 20
            },
            new Item
            {
                Name = "Backstage passes to a TAFKAL80ETC concert",
                SellIn = 10,
                Quality = 49
            },
            new Item
            {
                Name = "Backstage passes to a TAFKAL80ETC concert",
                SellIn = 5,
                Quality = 49
            },
            
            new Item {Name = "Conjured Mana Cake", SellIn = 3, Quality = 6}
        };
    }
}