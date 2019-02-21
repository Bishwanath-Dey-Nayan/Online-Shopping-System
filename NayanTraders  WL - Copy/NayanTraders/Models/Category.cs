using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NayanTraders.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Category Name Required.")]
        [Display(Name ="Category Name")]
        public string Name { get; set; }

        public ICollection<Product> Products { get; set; }

    }
}