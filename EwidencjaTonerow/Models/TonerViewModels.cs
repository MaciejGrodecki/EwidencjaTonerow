using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EwidencjaTonerow.Models
{
    public class AddTonerViewModel
    {
        [Display(Name = "Nazwa tonera")]
        public string Name { get; set; }
    }

    public class IndexTonerViewModel
    {
        public List<Toner> Toners { get; set; }
    }
}