using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NayanTraders.Models
{
    public class Order
    {
        [Key]
        public int OrderId { get; set; }
        public string OrderName { get; set; }
        public DateTime OrderDate { get; set; }
        public string PaymentType { get; set; }
        public string Status { get; set; }
        public string CustomerName { get; set; }
        public string Email { get; set; }
        public string CustomerAddress { get; set; }



        public ICollection<OrderDetail> OrderDetail { get; set; }
    }
}