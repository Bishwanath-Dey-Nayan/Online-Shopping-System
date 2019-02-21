using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NayanTraders.Models
{
    public class Purchase
    {
        [Key]
        public int Id { get; set; }

        [Display(Name ="Purchase Code")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Code Reqired.")]
        public string PurchaseCode { get; set; }

        [Display(Name ="Date")]
        [DataType(DataType.DateTime)]
        public DateTime PurchaseDate { get; set; }

        [Display(Name ="Vendor")]
        public int VendorId { get; set; }
        public virtual Vendor Vendor { get; set; }


        public int total { get; set; }


        public ICollection<PurchaseDetails> PurchaseDetail { get; set; }
    }
}
