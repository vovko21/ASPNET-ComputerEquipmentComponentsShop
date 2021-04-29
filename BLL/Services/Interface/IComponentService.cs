using DAL.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Type = DAL.Entity.Type;
using Producer = DAL.Entity.Producer;
using Component = DAL.Entity.Component;


namespace BLL.Services.Interface
{
    public interface IComponentService
    {
        Task CreateComponentAsync(Component component);
        Component GetComponent(int id);
        Task UpdateComponentAsync(Component component);
        Task DeleteComponentAsync(int id);
        IEnumerable<Component> GetAllComponents();
        IEnumerable<Type> GetAllTypes();
        IEnumerable<Producer> GetAllProducers();
    }
}
