using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EwidencjaTonerow.Models
{
    public class AddRoomViewModel
    {
        [Display(Name = "Nazwa pomieszczenia")]
        public string Name { get; set; }
    }

    public class IndexRoomViewModel
    {
        public List<Room> Rooms { get; set; }
    }
}