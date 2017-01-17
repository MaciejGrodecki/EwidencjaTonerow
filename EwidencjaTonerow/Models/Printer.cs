using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EwidencjaTonerow.Models
{
    public class Printer
    {
        [Key]
        public int PrinterID { get; set; }

        [Display(Name = "Nazwa drukarki")]
        [Required]
        public string Name { get; set; }

        public virtual ICollection<Room> Rooms { get; set; }
        public virtual ICollection<Toner> Toners { get; set; }
        public virtual ICollection<Storehouse> Storehouse { get; set; }

        public Printer()
        {
            Rooms = new List<Room>();
            Toners = new List<Toner>();
        }
    }
}