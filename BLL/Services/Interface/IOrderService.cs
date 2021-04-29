using BLL.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BLL.Services.Interface
{
    public interface IOrderService
    {
        Task CreateOrderAsync(OrderDTO component);
        OrderDTO GetOrder(int id);
        Task UpdateOrderAsync(OrderDTO component);
        Task DeleteOrderAsync(int id);
        IEnumerable<OrderDTO> GetAllOrders();
        bool IsClientExist(ClientDTO client);
        ClientDTO GetClient(string userId);
    }
}
