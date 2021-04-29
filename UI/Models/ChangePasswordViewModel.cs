using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace UI.Models
{
    public class ChangePasswordViewModel
    {
        public string Id { get; set; }
        [Required(ErrorMessage = "Current password field Required")]
        [MinLength(6, ErrorMessage = "Min 6 characters")]
        public string CurrentPassword { get; set; }
        [Required(ErrorMessage = "New password field Required")]
        [MinLength(6, ErrorMessage = "Min 6 characters")]
        public string NewPassword { get; set; }
    }
}