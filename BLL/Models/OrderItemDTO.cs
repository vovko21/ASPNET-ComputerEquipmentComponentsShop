namespace BLL.Models
{
    public class OrderItemDTO
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public int ComponentDBId { get; set; }
    }
}
