using Microsoft.AspNet.Identity.EntityFramework;
using System.ComponentModel.DataAnnotations;

namespace DAL.Entity.Users
{
    public class User : IdentityUser
    {
        [Required]
        [EmailAddress]
        public override string Email { get => base.Email; set => base.Email = value; }
        public string Image { get; set; }
    }
}
