using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EwidencjaTonerow.Models
{
    public class Storehouse
    {
        public int StorehouseID { get; set; }
        public int PrinterID { get; set; }
        public int? TonerID { get; set; }
        public int? RoomID { get; set; }
        public int Count { get; set; }

        public virtual Printer Printer { get; set; }
        public virtual Toner Toner { get; set; }
        public virtual Room Room { get; set; }

        public Storehouse()
        {
            Count = 0;
        }
    }
}