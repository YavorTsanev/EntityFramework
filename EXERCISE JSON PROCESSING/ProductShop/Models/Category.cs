using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ProductShop.Models
{
    public class Category
    {
        public int Id { get; set; }

        [Required, MinLength(3), StringLength(15)]
        public string Name { get; set; }

        public virtual ICollection<CategoryProducts> CategoryProducts { get; set; } = new HashSet<CategoryProducts>();
    }
}
