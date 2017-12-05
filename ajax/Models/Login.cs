using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ajax.Models
{
    public class Login
    {


          [Required(ErrorMessage = "Enter Id")]
            public int Id { get; set; }
            [Required(ErrorMessage = "Enter Password")]
            [Display(Name = "Pssd")]
            [DataType(DataType.Password)]
            public string Pssd { get; set; }
            [Required(ErrorMessage = "Select Login Type")]
            public string LoginType { get; set; }
            [Required(ErrorMessage = "Enter New Password")]
            [DataType(DataType.Password)]
            [Display(Name = "NewPssd")]
            [RegularExpression(@"((?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[!@#$%^&*_]).{6,20})", ErrorMessage = "Password should be 6-20 characters with at least one upper case letter, one lower case letter, one special character and a number.")]
            public string NewPssd { get; set; }
            [Required(ErrorMessage = "Enter Confirm Password")]
            [DataType(DataType.Password)]
            [Display(Name = "ConfirmPssd")]
            [Compare("NewPssd", ErrorMessage = "The new password and confirm password should match")]
            public string ConfirmPssd { get; set; }
       

    }
}