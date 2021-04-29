using DAL.Entity.StoreProducts;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DAL.Entity.Orders
{
    public class OrderItems
    {
        [Key]
        public int Id { get; set; }
        public int Quantity { get; set; }
        public int ComponentDBId { get; set; }
    }
}
