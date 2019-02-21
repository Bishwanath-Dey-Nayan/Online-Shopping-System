using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NayanTraders.Models
{
    public class Size
    {
        [Key]
        public int id { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Size Name Reqired.")]
        public string Name { get; set; }

        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        public ICollection<Product> Prodcuts { get; set; }
    }
}