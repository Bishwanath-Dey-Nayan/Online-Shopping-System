using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NayanTraders.Models
{
    public class Accounts
    {
        [Key]
        public int id { get; set; }
        [Required(AllowEmptyStrings =false, ErrorMessage ="Name Reqired.")]

        public string Name { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Contact Required.")]
        public string Contact { get; set; }

        [Display(Name="Gender")]
        public int GenderId { get; set; }
        public virtual Gender Gender { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Address Reqired.")]
        public string Address { get; set; }

        [Display(Name="Country")]
        public int CountryId { get; set; }
        public virtual Country Country { get; set; }

        [Display(Name ="City")]
        public int CityId { get; set; }
        public virtual City City { get; set; }
        
        [Required(AllowEmptyStrings = false, ErrorMessage = "Email-Address Reqired.")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Display(Name ="User Type")]
        public int UserTypeId { get; set; }
        public virtual UserType UserType { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Password Reqired.")]
        [DataType(DataType.Password)]
        public string PassWord { get; set; }

         [Required(AllowEmptyStrings = false, ErrorMessage = "Date Reqired.")]

         [DataType(DataType.Date)]
        public DateTime CreateDate { get; set; }

        public ICollection<Sale> Sales { get; set; }

    }
}