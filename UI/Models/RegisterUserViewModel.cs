using DAL.Validator;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace UI.Models
{
    public class RegisterUserViewModel
    {
        [Required]
        public string Username { get; set; }
        [Column("Email")]
        [Required]
        [EmailAddress(ErrorMessage = "Enter a proper email address")]
        [StringLength(25, ErrorMessage = "Maximum length allowed for an email is 30 characters")]
        public string EmailViewModel { get; set; }
        [Required]
        [MinLength(6)]
        public string Password { get; set; }
        [Required]
        [MinLength(6)]
        [Compare("Password", ErrorMessage = "Passwords must be equal")]
        public string ConfirmPassword { get; set; }
    }
}