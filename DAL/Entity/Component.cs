using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Entity
{
    public class Component
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        [Required]
        public virtual Type Type { get; set; }
        [Required]
        public virtual Producer Producer { get; set; }
        [Required]
        public int Price { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
    }
}
