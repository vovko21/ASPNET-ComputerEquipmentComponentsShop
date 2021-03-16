using DAL.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Type = DAL.Entity.Type;
using Producer = DAL.Entity.Producer;
using Component = DAL.Entity.Component;
using BLL.Models;
using BLL.Utils;

namespace BLL.Services.Interface
{
    public interface IStoreService
    {
        Task CreateComponentAsync(ComponentDTO component);
        ComponentDTO GetComponent(int id);
        Task UpdateComponentAsync(ComponentDTO component);
        Task DeleteComponentAsync(int id);
        IEnumerable<ComponentDTO> GetAllComponents();
        IEnumerable<ComponentDTO> GetAllComponents(List<ComponentFilter> filters);
        IEnumerable<Type> GetAllTypes();
        IEnumerable<Producer> GetAllProducers();
        public IEnumerable<string> GetAllTypeNames();
        public IEnumerable<string> GetAllProducerNames();
    }
}
