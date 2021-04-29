using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Type = DAL.Entity.Type;
using Producer = DAL.Entity.Producer;
using Component = DAL.Entity.Component;
using BLL.Services.Interface;
using DAL.Repository;
using DAL.Repository.Interface;

namespace BLL.Services
{
    public class ComponentService : IComponentService
    {
        private IGenericRepository<Component> componentRepository;
        private IGenericRepository<Type> typeRepository;
        private IGenericRepository<Producer> producerRepository;

        public ComponentService(IGenericRepository<Component> _componentRepository,
             IGenericRepository<Type> _typeRepository,
             IGenericRepository<Producer> _producerRepository)
        {
            componentRepository = _componentRepository;
            typeRepository = _typeRepository;
            producerRepository = _producerRepository;
        }

        public async Task CreateComponentAsync(Component component)
        {
            await componentRepository.AddAsync(component);
        }

        public async Task DeleteComponentAsync(int id)
        {
            await componentRepository.DeleteAsync(id);
        }

        public IEnumerable<Component> GetAllComponents()
        {
            return componentRepository.GetAll();
        }

        public IEnumerable<Producer> GetAllProducers()
        {
            return producerRepository.GetAll();
        }

        public IEnumerable<Type> GetAllTypes()
        {
            return typeRepository.GetAll();
        }

        public Component GetComponent(int id)
        {
            return componentRepository.Get(id);
        }

        public async Task UpdateComponentAsync(Component component)
        {
            await componentRepository.UpdateAsync(component);
        }
    }
}
