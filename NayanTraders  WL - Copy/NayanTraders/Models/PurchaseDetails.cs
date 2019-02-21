using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NayanTraders.Models
{
    public class PurchaseDetails
    {
        [Key]
        public int Id { get; set; }


        [Display(Name ="Purchse Id")]
        public int PurchaseId { get; set; }
        public virtual Purchase Purchase { get; set; }

        [Display(Name ="Product")]
        public int ProductId { get; set; }
        public virtual Product Product { get; set; }

        [Display(Name ="Brand")]
        public int BrandId { get; set; }
        public virtual Brand Brand { get; set; }

        [Display(Name ="Quantity")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Quantity Reqired.")]
        public int Qty { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Rate Reqired.")]
        public float Rate { get; set; }

        public string Amount { get; set; }
    }
}

