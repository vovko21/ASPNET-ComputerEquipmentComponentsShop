using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DAL.Entity.StoreProducts
{
    public class TypeDB
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        public virtual ICollection<ComponentDB> Components { get; set; }
    }
}
