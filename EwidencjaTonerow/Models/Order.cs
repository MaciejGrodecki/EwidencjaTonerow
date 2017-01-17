using System;
using System.ComponentModel.DataAnnotations;

namespace EwidencjaTonerow.Models
{
    public class Order
    {
        [Key]
        public int OrderID { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString ="{0:yyyy-MM-dd}", ApplyFormatInEditMode =true)]
        public DateTime SendDate { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? DeliveryDate { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? ChangeDate { get; set; }

        [Required]
        public int StorehouseID { get; set; }

        public virtual Storehouse Storehouse { get; set; }
    }
}