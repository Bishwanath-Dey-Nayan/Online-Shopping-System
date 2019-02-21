using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NayanTraders.Models
{
    public class Country
    {
        [Key]
        public int Id { get; set; }
        [Display(Name ="Country")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Country Name Reqired.")]
        public string Name { get; set; }

        public ICollection<Accounts> Accounts { get; set; }
        public ICollection<City> Cities { get; set; }

    }
}