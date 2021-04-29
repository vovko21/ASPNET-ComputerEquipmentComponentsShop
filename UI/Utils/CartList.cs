using System.Collections.Generic;
using UI.Models;

namespace UI.Utils
{
    public class CartList
    {
        private readonly List<OrderItemViewModel> _list;
        public CartList()
        {
            _list = new List<OrderItemViewModel>();
        }
        public IEnumerable<OrderItemViewModel> Get()
        {
            return _list;
        }
        public void Add(OrderItemViewModel componentViewModel)
        {
            _list.Add(componentViewModel);
        }
        public void Remove(OrderItemViewModel componentViewModel)
        {
            _list.Remove(componentViewModel);
        }
    }
}