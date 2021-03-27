using DAL.Validator;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace UI.Models
{
    public class RegisterUserViewModel
    {
        [Required]
        public string Username { get; set; }
        [Required]
        [EmailValidator(ErrorMessage = "Not valid email!")]
        public string Email { get; set; }
        [Required]
        [MinLength(6)]
        public string Password { get; set; }
        [Required]
        [MinLength(6)]
        [Compare("Password", ErrorMessage = "Muust be equal")]
        public string ConfirmPassword { get; set; }
    }
}