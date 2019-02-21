using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NayanTraders.Models
{
    public class OrderDetail
    {
        [Key]
        public int Id { get; set; }
        public int OrderId { get; set; }
        public virtual Order order { get; set; }

        public int ProductId { get; set; }
        public virtual Product Product { get; set; }

        public double Price { get; set; }
        public int Quantity { get; set; }
    }
}