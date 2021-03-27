using DAL.Validator;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entity
{
    public class User : IdentityUser
    {
        [Required]
        [EmailValidator]
        public override string Email { get => base.Email; set => base.Email = value; }
        public string Image { get; set; }
    }
}
