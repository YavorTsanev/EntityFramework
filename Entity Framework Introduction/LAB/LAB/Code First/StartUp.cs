using Code_First.Models;
using Microsoft.EntityFrameworkCore;
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
            db.Database.Migrate();



        }
    }
}
