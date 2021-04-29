using DAL.Entity.Users;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace UI.Models
{
    public class ClientViewModel
    {
        public int Id { get; set; }
        [Required]
        public virtual User User { get; set; }
        public virtual ICollection<OrderViewModel> Orders { get; set; }
    }
}