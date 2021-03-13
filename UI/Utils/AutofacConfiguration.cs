using Autofac;
using Autofac.Integration.Mvc;
using AutoMapper;
using BLL.Services;
using BLL.Services.Interface;
using DAL;
using DAL.Repository;
using DAL.Repository.Interface;
using System.Data.Entity;
using System.Web.Mvc;

namespace UI.Utils
{
    public static class AutofacConfiguration
    {
        public static void Configurate()
        {
            var builder = new ContainerBuilder();

            builder.RegisterControllers(typeof(MvcApplication).Assembly);

            builder.RegisterType<ApplicationContext>().As<DbContext>();
            builder.RegisterGeneric(typeof(GenericRepository<>)).As(typeof(IGenericRepository<>));
            builder.RegisterType<StoreService>().As<IStoreService>();

            var configurationManager = new MapperConfiguration(cfg => cfg.AddProfile(new AutomapperConfiguration()));
            builder.RegisterInstance(configurationManager.CreateMapper());

            DependencyResolver.SetResolver(new AutofacDependencyResolver(builder.Build()));
        }
    }
}