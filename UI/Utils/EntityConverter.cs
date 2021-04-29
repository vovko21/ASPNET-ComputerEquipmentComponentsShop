using AutoMapper;
using BLL.Models;
using BLL.Services;
using BLL.Services.Interface;
using DAL.Entity.StoreProducts;
using DAL.Repository;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;
using System.ServiceModel.Channels;
using UI.Models;

namespace UI.Utils
{
    public class EntityConverter : IValueConverter<int, ComponentViewModel>
    {
        //private readonly IStoreService _service;

        public EntityConverter()
        {
            
        }

        public ComponentViewModel Convert(int sourceMember, ResolutionContext context)
        {
            //IStoreService _service = new StoreService(new GenericRepository<ComponentDB>(new DbContext("name=AplicationContext")),
            //    new GenericRepository<TypeDB>(new DbContext("name=AplicationContext")),
            //    new GenericRepository<ProducerDB>(new DbContext("name=AplicationContext")), );
            var repos = new GenericRepository<ComponentDB>(new IdentityDbContext("name=AplicationContext"));
            return context.Mapper.Map<ComponentDTO, ComponentViewModel>(context.Mapper.Map<ComponentDB, ComponentDTO>(repos.Get(sourceMember)));
        }
    }
}