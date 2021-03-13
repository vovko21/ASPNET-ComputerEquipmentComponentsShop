﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entity
{
    public class Type
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public ICollection<Component> Components { get; set; }
    }
}