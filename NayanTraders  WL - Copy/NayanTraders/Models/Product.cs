using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NayanTraders.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        [Display(Name ="Product Name")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Product Name Reqired.")]
        public string Name { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Code Reqired.")]
        public string Code { get; set; }

        [Display(Name ="Category")]
        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }

        [Display(Name ="Size")]
        public int SizeId { get; set; }
        public virtual Size Size { get; set; }


        [Display(Name ="Unit")]
        public int UnitId { get; set; }
        public virtual Unit Unit { get; set; }

        [Display(Name ="Brand")]
        public int BrandId { get; set; }
        public virtual Brand Brands { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Price Reqired.")]
        public float  Price { get; set; }

        public string Discount { get; set; }
        public string Image { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Date Reqired.")]
        [DataType(DataType.DateTime)]
        public DateTime Date { get; set; }

        public ICollection<SalesDetail> SalesDetails { get; set; }
        public ICollection<PurchaseDetails> PurchaseDetail { get; set; }
        public ICollection<OrderDetail> OrderDetail { get; set; }
    }
}

