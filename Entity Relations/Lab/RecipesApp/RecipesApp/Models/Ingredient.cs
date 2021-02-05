using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace RecipesApp.Models
{
    public class Ingredient
    {
        public Ingredient()
        {
            Recipes = new HashSet<RecipeIngredient>();
        }

        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public decimal Quantity { get; set; }

        public ICollection<RecipeIngredient> Recipes { get; set; }


    }
}
