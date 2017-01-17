using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EwidencjaTonerow.Models
{
    public class IndexStorehouseViewModel
    {
        public IEnumerable<Printer> Printers { get; set; }
        public IEnumerable<Order> Orders { get; set; }
        public IEnumerable<Storehouse> Storehouse { get; set; }
    }

    public class ExchangedStorehouseViewModel
    {
        public IEnumerable<Printer> Printers { get; set; }
        public IEnumerable<Order> Orders { get; set; }
        public IEnumerable<Storehouse> Storehouse { get; set; }
    }
}