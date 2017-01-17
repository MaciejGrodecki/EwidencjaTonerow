using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EwidencjaTonerow.Models
{
    public class AddOrderViewModel
    {
        public int StorehouseID { get; set; }
        public IEnumerable<Storehouse> StorehouseItems { get; set; }

        public int PrinterID { get; set; }

        public IEnumerable<Printer> Printers { get; set; }
        public int RoomID { get; set; }

        public IEnumerable<Room> Rooms { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Data wysłania")]
        public DateTime SendDate { get; set; }

        public int TonerID { get; set; }
        public IEnumerable<Toner> Toners { get; set; }
    }

    public class IndexOrderViewModel
    {
        public ICollection<Order> Orders { get; set; }
    }

    public class DeliveredOrderViewModel
    {
        public ICollection<Order> Orders { get; set; }
    }
}