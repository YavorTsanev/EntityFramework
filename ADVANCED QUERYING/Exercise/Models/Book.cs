using BookShop.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BookShop.Models
{
    public class Book
    {
        public int BookId { get; set; }

        [Required, Column(TypeName = "nvarchar(50)")]
        public string Title  { get; set; }

        [Required, Column(TypeName = "nvarchar(1000)")]
        public string Description  { get; set; }

        public DateTime? ReleaseDate  { get; set; }

        public int Copies  { get; set; }

        public decimal Price { get; set; }

        public EditionType EditionType  { get; set; }

        public AgeRestriction AgeRestriction  { get; set; }

        public Author Author { get; set; }

        public virtual ICollection<BookCategory> BookCategories { get; set; } = new HashSet<BookCategory>();
    }
}
