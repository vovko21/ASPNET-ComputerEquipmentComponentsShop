using DAL.Entity;
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
            var types = new List<Entity.Type> {
                new Entity.Type() { Name = "Videocard"},
                new Entity.Type() { Name = "Processor"},
                new Entity.Type() { Name = "Motherboard"}
            };

            var producers = new List<Producer> {
                new Producer() {Name = "Nvidia"},
                new Producer() {Name = "AMD"},
                new Producer() {Name = "Intel"},
                new Producer() {Name = "Gigabyte"},
            };

            var components = new List<Component>
            {
                new Component
                {
                    Name = "RTX 2060",
                    Type = types.FirstOrDefault(x => x.Name == "Videocard"),
                    Producer = producers.FirstOrDefault(x => x.Name == "Nvidia"),
                    Price = 35345602,
                    Description = "Bruh",
                    Image = "https://www.ixbt.com/img/r30/00/02/16/18/front.jpg"
                },
                new Component
                {
                    Name = "B450M S2H",
                    Type = types.FirstOrDefault(x => x.Name == "Motherboard"),
                    Producer = producers.FirstOrDefault(x => x.Name == "Gigabyte"),
                    Price = 120,
                    Description = "Lorem Ipsum",
                    Image = "https://www.powerplanetonline.com/cdnassets/gigabyte_b450m_s2h_rev_1_0_am4_01_l.jpg"
                },
                new Component
                {
                    Name = "Intel Core I5",
                    Type = types.FirstOrDefault(x => x.Name == "Processor"),
                    Producer = producers.FirstOrDefault(x => x.Name == "Intel"),
                    Price = 200,
                    Description = "Lorem Ipsum",
                    Image = "https://hotline.ua/img/tx/228/2283825645.jpg"
                },
                new Component
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
