using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NayanTraders.Models
{
    public class SalesDetail
    {
        [Key]
        public int Id { get; set; }

        [Display(Name ="Sale Id")]
        public int SaleId { get; set; }
        public virtual Sale Sale { get; set; }

        [Display(Name ="Product")]
        public int ProductId { get; set; }
        public virtual Product Product { get; set; }

       
        public int Quantity { get; set; }
        public int rate { get; set; }
    }
}

