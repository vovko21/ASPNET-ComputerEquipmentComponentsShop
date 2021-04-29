using AutoMapper;
using BLL.Models;
using BLL.Services.Interface;
using DAL.Entity.Orders;
using DAL.Repository.Interface;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class OrderService : IOrderService
    {
        private IGenericRepository<Order> orderRepository;
        private IGenericRepository<Client> clientRepository;
        private readonly IMapper mapper;

        public OrderService(IGenericRepository<Order> _orderRepository,
             IGenericRepository<Client> _clientRepository,
             IMapper _mapper)
        {
            orderRepository = _orderRepository;
            clientRepository = _clientRepository;
            mapper = _mapper;
        }
        public async Task CreateOrderAsync(OrderDTO component)
        {
            await orderRepository.AddAsync(mapper.Map<OrderDTO, Order>(component));
        }

        public async Task DeleteOrderAsync(int id)
        {
            await orderRepository.DeleteAsync(id);
        }

        public IEnumerable<OrderDTO> GetAllOrders()
        {
            return mapper.Map<IEnumerable<Order>, IEnumerable<OrderDTO>>(orderRepository.GetAll());
        }

        public OrderDTO GetOrder(int id)
        {
            return mapper.Map<Order, OrderDTO>(orderRepository.Get(id));
        }

        public ClientDTO GetClient(string userId)
        {
            return mapper.Map<Client, ClientDTO>(clientRepository.GetAll().Where(c => c.User.Id == userId).FirstOrDefault());
        }

        public bool IsClientExist(ClientDTO client)
        {
            return clientRepository.Exists(mapper.Map<ClientDTO, Client>(client));
        }

        public async Task UpdateOrderAsync(OrderDTO component)
        {
            await orderRepository.UpdateAsync(mapper.Map<OrderDTO, Order>(component));
        }
    }
}
