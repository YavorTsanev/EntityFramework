using Code_First.Models;
using System;

namespace Code_First
{
    class StartUp
    {
        static void Main(string[] args)
        {
            var db = new RecipiesDbContext();
            db.Database.EnsureCreated();
        }
    }
}
