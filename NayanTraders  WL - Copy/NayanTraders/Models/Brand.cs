using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NayanTraders.Models
{
    public class Brand
    {
        [Key]
        public int Id { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Brand Name Reqired.")]
        [Display(Name ="Brand Name")]
        public string Name { get; set; }

        public ICollection<PurchaseDetails> PurchaseDetail { get; set; }
        public ICollection<Product> Products { get; set; }
    }
}