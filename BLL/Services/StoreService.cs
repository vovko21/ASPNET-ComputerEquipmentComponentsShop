using AutoMapper;
using Binbin.Linq;
using BLL.Models;
using BLL.Services.Interface;
using BLL.Utils;
using DAL.Entity.StoreProducts;
using DAL.Repository.Interface;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using ComponentDB = DAL.Entity.StoreProducts.ComponentDB;
using TypeDB = DAL.Entity.StoreProducts.TypeDB;

namespace BLL.Services
{
    public class StoreService : IStoreService
    {
        private IGenericRepository<ComponentDB> componentRepository;
        private IGenericRepository<TypeDB> typeRepository;
        private IGenericRepository<ProducerDB> producerRepository;
        private readonly IMapper mapper;

        public StoreService(IGenericRepository<ComponentDB> _componentRepository,
             IGenericRepository<TypeDB> _typeRepository,
             IGenericRepository<ProducerDB> _producerRepository,
             IMapper _mapper)
        {
            componentRepository = _componentRepository;
            typeRepository = _typeRepository;
            producerRepository = _producerRepository;
            mapper = _mapper;
        }

        public async Task CreateComponentAsync(ComponentDTO component)
        {
            await componentRepository.AddAsync(mapper.Map<ComponentDTO, ComponentDB>(component));
        }

        public async Task DeleteComponentAsync(int id)
        {
            await componentRepository.DeleteAsync(id);
        }

        public IEnumerable<ComponentDTO> GetAllComponents()
        {
            return mapper.Map<IEnumerable<ComponentDB>, IEnumerable<ComponentDTO>>(componentRepository.GetAll());
        }

        public IEnumerable<ComponentDTO> GetAllComponents(List<ComponentFilter> filters)
        {
            if (filters.Count == 1)
                return mapper.Map<IEnumerable<ComponentDTO>>(componentRepository.GetAll()).Where(filters[0].Predicate.Compile());

            var predicateGroups = new List<Expression<Func<ComponentDTO, bool>>>();
            var groups = filters.GroupBy(x => x.Type);

            foreach (var g in groups)
            {
                var builder = PredicateBuilder.Create(g.FirstOrDefault().Predicate);
                for (int i = 0; i < g.Count(); i++)
                {
                    builder = builder.Or(g.ToArray()[i].Predicate);
                }
                predicateGroups.Add(builder);
            }

            if (predicateGroups.Count == 1)
            {
                return mapper.Map<IEnumerable<ComponentDTO>>(componentRepository.GetAll()).Where(predicateGroups.FirstOrDefault().Compile());
            }

            var builder1 = PredicateBuilder.Create(filters.FirstOrDefault().Predicate);
            foreach (var p in predicateGroups)
            {
                builder1 = builder1.And(p);
            }
            return mapper.Map<IEnumerable<ComponentDTO>>(componentRepository.GetAll()).Where(builder1.Compile());
        }

        public IEnumerable<ProducerDB> GetAllProducers()
        {
            return producerRepository.GetAll();
        }

        public IEnumerable<TypeDB> GetAllTypes()
        {
            return typeRepository.GetAll();
        }

        public ComponentDTO GetComponent(int id)
        {
            return mapper.Map<ComponentDB, ComponentDTO>(componentRepository.Get(id));
        }

        public IEnumerable<string> GetAllTypeNames()
        {
            return typeRepository.GetAll().Select(t => t.Name);
        }

        public IEnumerable<string> GetAllProducerNames()
        {
            return producerRepository.GetAll().Select(t => t.Name);
        }

        public async Task UpdateComponentAsync(ComponentDTO component)
        {
            await componentRepository.UpdateAsync(mapper.Map<ComponentDTO, ComponentDB>(component));
        }

        public void Detach(ComponentDTO entity)
        {
            componentRepository.Detach(mapper.Map<ComponentDTO, ComponentDB>(entity));
        }
        public void Attach(ComponentDTO entity)
        {
            componentRepository.Detach(mapper.Map<ComponentDTO, ComponentDB>(entity));
        }
    }
}
