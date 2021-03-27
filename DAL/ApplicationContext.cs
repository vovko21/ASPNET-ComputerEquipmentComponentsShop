using DAL.Entity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;

namespace DAL
{
    public class ApplicationContext : IdentityDbContext<IdentityUser>
    {
        public ApplicationContext()
            : base("name=AplicationContext")
        {
            this.Configuration.LazyLoadingEnabled = true;
            Database.SetInitializer(new Initializer());
        }

        public DbSet<Producer> Producers { get; set; }
        public DbSet<Component> Components { get; set; }
        public DbSet<Type> Types { get; set; }
    }
}