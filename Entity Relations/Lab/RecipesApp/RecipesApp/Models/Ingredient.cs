using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace RecipesApp.Models
{
    public class Ingredient
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public decimal Quantity { get; set; }

        public Recipe Recipe { get; set; }


    }
}
