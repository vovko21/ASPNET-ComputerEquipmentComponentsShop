namespace DAL
{
    using DAL.Entity;
    using System;
    using System.ComponentModel;
    using System.Data.Entity;
    using System.Linq;

    public class ApplicationContext : DbContext
    {
        public ApplicationContext()
            : base("name=AplicationContext")
        {
            Database.SetInitializer(new Initializer());
        }

        public virtual DbSet<Producer> Producers { get; set; }
        public virtual DbSet<Entity.Component> Components { get; set; }
        public virtual DbSet<Entity.Type> Types { get; set; }
    }
}