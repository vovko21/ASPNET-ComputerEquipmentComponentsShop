using DAL.Entity.Users;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BLL.Models
{
    public class ClientDTO
    {
        public int Id { get; set; }
        [Required]
        public User User { get; set; }
        public ICollection<OrderDTO> Orders { get; set; }
    }
}
