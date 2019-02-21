using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NayanTraders.Models
{
    public class UserType
    {
        [Key]
        public int Id { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Type Name Reqired.")]
        [Display(Name ="User type Name")]
        public string Name { get; set; }

        public ICollection<Accounts> Accounts { get; set; }
        public ICollection<User> Users { get; set; }
    }
}