using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EwidencjaTonerow.Models
{
    public class Toner
    {
        [Key]
        public int TonerID { get; set; }

        [Display(Name = "Nazwa tonera")]
        [Required]
        public string Name { get; set; }

        public virtual ICollection<Printer> Printers { get; set; }

        public Toner()
        {
            Printers = new List<Printer>();
        }

        public Toner(string Name, int PrinterID)
        {
            this.Name = Name;
        }
    }

    public class TonerCount
    {
        public int PrinterID;
        public int RoomID;
        public int Count;

        public TonerCount(int printerID, int? roomID, int count)
        {
            PrinterID = printerID;
            RoomID = roomID.Value;
            Count = count;
        }
    }
}