using Code_First.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Code_First
{
    class StartUp
    {
        static void Main(string[] args)
        {
            var db = new RecipesDbContext();
            db.Database.EnsureCreated();


            for (int i = 0; i < 10; i++)
            {
                db.Recipes.Add(new Recipe
                {
                    Name = $"Musaka{i}",
                    Ingredients = new List<Ingredient>
                    {
                        new Ingredient{Name = "Meat", Amount = 10},
                         new Ingredient{Name = "Patato", Amount = 15}
                    }
                });
            }

            db.SaveChanges();

        }
    }
}
