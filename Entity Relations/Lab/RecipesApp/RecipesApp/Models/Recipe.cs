
using System;
using System.Collections;
using System.Collections.Generic;

namespace RecipesApp.Models
{
    public class Recipe
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public TimeSpan CookingTime { get; set; }

        public ICollection<Ingredient> Ingredients { get; set; }
    }
}
