using AutoMapper;
using Binbin.Linq;
using BLL.Models;
using BLL.Services.Interface;
using BLL.Utils;
using DAL.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Component = DAL.Entity.Component;
using Producer = DAL.Entity.Producer;
using Type = DAL.Entity.Type;

namespace BLL.Services
{
    public class StoreService : IStoreService
    {
        private IGenericRepository<Component> componentRepository;
        private IGenericRepository<Type> typeRepository;
        private IGenericRepository<Producer> producerRepository;
        private readonly IMapper mapper;

        public StoreService(IGenericRepository<Component> _componentRepository,
             IGenericRepository<Type> _typeRepository,
             IGenericRepository<Producer> _producerRepository,
             IMapper _mapper)
        {
            componentRepository = _componentRepository;
            typeRepository = _typeRepository;
            producerRepository = _producerRepository;
            mapper = _mapper;
        }

        public async Task CreateComponentAsync(ComponentDTO component)
        {
            await componentRepository.AddAsync(mapper.Map<Component>(component));
        }

        public async Task DeleteComponentAsync(int id)
        {
            await componentRepository.DeleteAsync(id);
        }

        public IEnumerable<ComponentDTO> GetAllComponents()
        {
            return mapper.Map<IEnumerable<ComponentDTO>>(componentRepository.GetAll());
        }

        public IEnumerable<ComponentDTO> GetAllComponents(List<ComponentFilter> filters)
        {
            var predicates = new List<Expression<Func<ComponentDTO, bool>>>();

            var groups = filters.GroupBy(x => x.Type);

            foreach (var g in groups)
            {
                var builder = PredicateBuilder.Create(g.FirstOrDefault().Predicate);
                for (int i = 0; i < g.Count(); i++)
                {
                    builder = builder.Or(g.ToArray()[i].Predicate);
                }
                predicates.Add(builder);
            }
            var builder1 = PredicateBuilder.Create(filters.FirstOrDefault().Predicate);
            if (predicates.Count > 1)
            {
                foreach (var p in predicates)
                {
                    builder1 = builder1.And(p);
                }
            }
            return mapper.Map<IEnumerable<ComponentDTO>>(componentRepository.GetAll()).Where(builder1.Compile());
            //return mapper.Map<IEnumerable<ComponentDTO>>(componentRepository.GetAll().Where(mapper.Map<Func<Component, bool>>(builder1.Compile())));
        }

        public IEnumerable<Producer> GetAllProducers()
        {
            return producerRepository.GetAll();
        }

        public IEnumerable<Type> GetAllTypes()
        {
            return typeRepository.GetAll();
        }

        public ComponentDTO GetComponent(int id)
        {
            return mapper.Map<ComponentDTO>(componentRepository.Get(id));
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
            await componentRepository.UpdateAsync(mapper.Map<Component>(component));
        }
    }
}
