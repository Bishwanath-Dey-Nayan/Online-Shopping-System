using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NayanTraders.Models
{
    public class Unit
    {
        [Key]
        public int Id { get; set; }

        [Display(Name="Unit Name")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Unit Name Reqired.")]
        public string Name { get; set; }

        [DataType(DataType.MultilineText)]
        public string Description { get; set; }
        
        public ICollection<Product> Products { get; set; }
    }
}