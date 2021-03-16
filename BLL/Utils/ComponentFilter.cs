using BLL.Models;
using DAL.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Utils
{
    public class ComponentFilter
    {
        public string Type { get; set; }
        public string Name { get; set; }
        public Expression<Func<ComponentDTO, bool>> Predicate { get; set; }
    }
}
