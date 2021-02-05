using RecipesApp.Models;
using System;
using System.Collections.Generic;

namespace RecipesApp
{
    class StartUp
    {
        static void Main(string[] args)
        {

            var db = new RecipesDbContext();

            db.Database.EnsureDeleted();
            db.Database.EnsureCreated();

            var recipe = new Recipe
            {
                Name = "Musaka",
                CookingTime = new TimeSpan(2, 3, 4),
                Description = "Pukam Musaka",
                Ingredients = {new RecipeIngredient
                {
                    Ingredient = new Ingredient { Name = "Meat", Quantity = 2000 }

                },
                new RecipeIngredient
                {
                    Ingredient = new Ingredient { Name = "Patato", Quantity = 1000 }
                }}

            };

            db.Recipes.Add(recipe);
            

            db.SaveChanges();
        }
    }
}
