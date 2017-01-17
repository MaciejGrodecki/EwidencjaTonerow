using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EwidencjaTonerow.Models
{
    public class AddPrinterViewModel
    {
        [Display(Name = "Nazwa drukarki")]
        public string Name { get; set; }

        public int RoomID { get; set; }
        public ICollection<Room> Rooms { get; set; }
    }

    public class AddRoomToPrinterViewModel
    {
        public int PrinterID { get; set; }
        public Printer Printer { get; set; }
        public int RoomID { get; set; }
        public ICollection<Room> Rooms { get; set; }
    }

    public class AddTonerToPrinterViewModel
    {
        public int PrinterID { get; set; }
        public Printer Printer { get; set; }
        public int TonerID { get; set; }
        public ICollection<Toner> Toners { get; set; }
    }

    public class IndexPrinterViewModel
    {
        public IEnumerable<Printer> Printers { get; set; }
        public IEnumerable<Room> Rooms { get; set; }
        public IEnumerable<Toner> Toners { get; set; }
    }
}