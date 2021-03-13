using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entity
{
    public class Component
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        [Required]
        public Type Type { get; set; }
        [Required]
        public Producer Producer { get; set; }
        [Required]
        public int Price { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
    }
}
