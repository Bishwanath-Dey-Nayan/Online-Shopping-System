using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NayanTraders.Models
{
    public class Sale
    {
        [Key]
        public int Id { get; set; }

        [Display(Name ="Sales Code")]
        public string SaleCode { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime Date { get; set; }

        [Display(Name ="Client Name")]
        public int AccountId { get; set; }
        public virtual Accounts Accounts { get; set; }

        public string Total { get; set; }

        public ICollection<SalesDetail> SalesDetails { get; set; }
    }
}

