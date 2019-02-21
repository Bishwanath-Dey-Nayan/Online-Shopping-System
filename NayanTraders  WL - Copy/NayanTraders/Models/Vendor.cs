using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NayanTraders.Models
{
    public class Vendor
    {
        [Key]
        public int Id { get; set; }

        [Display(Name ="Vendor Name")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Vendor Name Reqired.")]
        public string Name { get; set; }

        
        public string Address { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Branch Reqired.")]
        public string Branch { get; set; }
        
        public string Contact { get; set; }

        public ICollection<Purchase> Purchases { get; set; }
    }
}