using RecipesApp.Models;
using System;

namespace RecipesApp
{
    class StartUp
    {
        static void Main(string[] args)
        {
            var db = new RecipesDbContext();

            db.Database.EnsureDeleted();
            db.Database.EnsureCreated();

            db.Recipes.Add(new Recipe() { Name = "Musaka", Description ="Pukam Musaka", CookingTime = new TimeSpan(2,3,4)});
            db.SaveChanges();
        }
    }
}
