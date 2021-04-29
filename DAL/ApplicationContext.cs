using DAL.Entity.Orders;
using DAL.Entity.StoreProducts;
using DAL.Entity.Users;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;

namespace DAL
{
    public class ApplicationContext : IdentityDbContext<User>
    {
        public ApplicationContext()
            : base("name=AplicationContext")
        {
            this.Configuration.LazyLoadingEnabled = true;
            //Database.SetInitializer(new Initializer());
        }

        public DbSet<ProducerDB> Producers { get; set; }
        public DbSet<ComponentDB> Components { get; set; }
        public DbSet<TypeDB> Types { get; set; }

        public DbSet<Order> Orders { get; set; }
    }
}