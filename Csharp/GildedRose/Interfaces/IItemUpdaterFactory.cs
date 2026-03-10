using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GildedRoseKata.Interfaces
{
    public interface IItemUpdaterFactory
    {
        IItemUpdater ItemUpdaterFor(string itemName);
    }
}
