using DAL.Entity.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace UI.Models
{
    public class OrderViewModel
    {
        public int Id { get; set; }
        private OrderStatus orderStatus;
        public OrderStatus OrderStatus
        {
            get => orderStatus;
            set
            {
                if (value == OrderStatus.Completed || value == OrderStatus.Failed || value == OrderStatus.Refunded)
                {
                    OrderClosed = DateTime.Now;
                }
                orderStatus = value;
            }
        }
        public DateTime OrderCreated { get; set; }
        public DateTime? OrderClosed { get; set; }
        public string Adress { get; set; }
        [Required]
        public string ClientId { get; set; }
        public List<OrderItemViewModel> OrderItems { get; set; }
        //public int TotalPrice 
        //{ 
        //    get
        //    {
        //        return OrderItems.Sum(x => x.Component.Price);
        //    }
        //}
    }
}