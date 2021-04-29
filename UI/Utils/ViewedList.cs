using System.Collections.Generic;
using UI.Models;

namespace UI.Utils
{
    public class ViewedList
    {
        private readonly List<ComponentViewModel> _list;
        public ViewedList()
        {
            _list = new List<ComponentViewModel>();
        }
        public IEnumerable<ComponentViewModel> Get()
        {
            return _list;
        }
        public void Append(ComponentViewModel item)
        {
            if (_list.Contains(item))
            {
                _list.Remove(item);
            }
            _list.Add(item);
            _list.Reverse();
        }
        public void Remove(ComponentViewModel item)
        {
            _list.Remove(item);
        }
    }
}