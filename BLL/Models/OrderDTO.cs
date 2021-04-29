using DAL.Entity.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BLL.Models
{
    public class OrderDTO
    {
        public int Id { get; set; }
        public OrderStatus OrderStatus { get; set; }
        public DateTime OrderCreated { get; set; }
        public DateTime? OrderClosed { get; set; }
        public string Adress { get; set; }
        [Required]
        public string ClientId { get; set; }
        public ICollection<OrderItemDTO> OrderItems { get; set; }
    }
}
