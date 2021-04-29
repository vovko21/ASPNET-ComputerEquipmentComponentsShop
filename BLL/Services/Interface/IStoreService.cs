using BLL.Models;
using BLL.Utils;
using DAL.Entity.StoreProducts;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TypeDB = DAL.Entity.StoreProducts.TypeDB;

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
        IEnumerable<TypeDB> GetAllTypes();
        IEnumerable<ProducerDB> GetAllProducers();
        IEnumerable<string> GetAllTypeNames();
        IEnumerable<string> GetAllProducerNames();
        void Detach(ComponentDTO entity);
        void Attach(ComponentDTO entity);
    }
}
