using DAL.Entity.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DAL.Entity.Orders
{
    public class Order
    {
        [Key]
        public int Id { get; set; }
        public OrderStatus OrderStatus { get; set; }
        public DateTime OrderCreated { get; set; }
        public DateTime? OrderClosed { get; set; }
        public string Adress { get; set; }
        [Required]
        public string ClientId { get; set; }
        public virtual ICollection<OrderItems> OrderItems { get; set; }
    }
}
