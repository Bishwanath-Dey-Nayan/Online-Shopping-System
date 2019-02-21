using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NayanTraders.Models
{
    public class Gender
    {
        [Key]
        public int Id { get; set; }


        [Display(Name ="Gender")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Gender Reqired.")]
        public string Name { get; set; }

        [DataType(DataType.MultilineText)]
        public string Description { get; set; }


        public ICollection <Accounts> Accounts { get; set; }
    }
}