using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NayanTraders.Models
{
    public class ResetPasswordModel
    {
        [Required(ErrorMessage ="New Password Required",AllowEmptyStrings =false)]
        [DataType(DataType.Password)]
        public string NewPassword { get; set;}

        [Required(ErrorMessage = "New Password and the confirm password does not match", AllowEmptyStrings = false)]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }

        public string ResetCode { get; set; }
    }
}