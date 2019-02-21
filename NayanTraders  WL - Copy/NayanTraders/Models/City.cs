using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NayanTraders.Models
{
    public class City
    {
        [Key]
        public int Id { get; set; }

        [Display(Name ="Country")]
        public int CountryId { get; set; }
        public virtual Country Country { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Country Name Reqired.")]
        [Display(Name ="City")]
        public string Name { get; set; }

        public ICollection<Accounts> Accounts { get; set; }
    }
}