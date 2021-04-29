using DAL.Entity;
using DAL.Entity.StoreProducts;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class Initializer : DropCreateDatabaseAlways<ApplicationContext>
    {
        protected override void Seed(ApplicationContext context)
        {
            var types = new List<Entity.StoreProducts.TypeDB> {
                new Entity.StoreProducts.TypeDB() { Name = "Videocard"},
                new Entity.StoreProducts.TypeDB() { Name = "Processor"},
                new Entity.StoreProducts.TypeDB() { Name = "Motherboard"}
            };

            var producers = new List<ProducerDB> {
                new ProducerDB() {Name = "Nvidia"},
                new ProducerDB() {Name = "AMD"},
                new ProducerDB() {Name = "Intel"},
                new ProducerDB() {Name = "Gigabyte"},
            };

            var components = new List<ComponentDB>
            {
                new ComponentDB
                {
                    Name = "RTX 2060",
                    Type = types.FirstOrDefault(x => x.Name == "Videocard"),
                    Producer = producers.FirstOrDefault(x => x.Name == "Nvidia"),
                    Price = 35345602,
                    Description = "Bruh",
                    Image = "https://www.ixbt.com/img/r30/00/02/16/18/front.jpg"
                },
                new ComponentDB
                {
                    Name = "B450M S2H",
                    Type = types.FirstOrDefault(x => x.Name == "Motherboard"),
                    Producer = producers.FirstOrDefault(x => x.Name == "Gigabyte"),
                    Price = 120,
                    Description = "Lorem Ipsum",
                    Image = "https://www.powerplanetonline.com/cdnassets/gigabyte_b450m_s2h_rev_1_0_am4_01_l.jpg"
                },
                new ComponentDB
                {
                    Name = "Intel Core I5",
                    Type = types.FirstOrDefault(x => x.Name == "Processor"),
                    Producer = producers.FirstOrDefault(x => x.Name == "Intel"),
                    Price = 200,
                    Description = "Lorem Ipsum",
                    Image = "https://hotline.ua/img/tx/228/2283825645.jpg"
                },
                new ComponentDB
                {
                    Name = "AMD Ryzer 5 3600",
                    Type = types.FirstOrDefault(x => x.Name == "Processor"),
                    Producer = producers.FirstOrDefault(x => x.Name == "AMD"),
                    Price = 200,
                    Description = "Lorem Ipsum",
                    Image = "https://luckylink.kiev.ua/content/uploads/images/s-l1600%281%29.jpg"
                }
            };

            context.Producers.AddRange(producers);
            context.Types.AddRange(types);
            context.Components.AddRange(components);

            context.SaveChanges();

            base.Seed(context);
        }
    }
}
