using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using DAL.Entity.Users;

namespace DAL.Entity.Orders
{
    public class Client
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public virtual User User { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
        public override bool Equals(object obj)
        {
            if (obj is Client)
            {
                var client = (Client)obj;
                if (client.Id == this.Id)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
