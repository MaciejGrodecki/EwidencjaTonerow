using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EwidencjaTonerow.Models
{
    public class Room
    {
        [Key]
        public int RoomID { get; set; }

        [Display(Name = "Nazwa pomieszczenia")]
        [Required]
        public string Name { get; set; }

        public virtual ICollection<Printer> Printers { get; set; }

        public Room()
        {
            Printers = new List<Printer>();
        }

        public Room(string Name)
        {
            this.Name = Name;
        }
    }
}