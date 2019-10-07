using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TimebookingMVC2.Models
{
    public class AccountModels
    {
        public class LoginModel
        {
            [Required(ErrorMessage = "Username is required")]
            [Display(Name = "Username")]
            public string UserName { get; set; }

            [Required(ErrorMessage = "Password is required")]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }

        }

        public class RegisterModel
        {
            [Required(ErrorMessage = "Username is required")]
            [Display(Name = "Username")]
            public string UserName { get; set; }

            [Required(ErrorMessage = "Email is required")]
            [Display(Name = "Email")]
            public string Email { get; set; }

            [Required(ErrorMessage = "Password is required")]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }
        }
    }
}