using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookShop.Models
{
    public class Category
    {
        public int CategoryId { get; set; }

        [Required, Column(TypeName = "nvarchar(50)")]
        public string Name { get; set; }

        public virtual ICollection<BookCategory> GetBookCategories { get; set; } = new HashSet<BookCategory>();


    }
}