using System;
using PetStore.Data;
using PetStore.Models;

namespace PetStore.ConsoleApp
{
    public class StartUp
    {
        static void Main(string[] args)
        {
            var db = new PetStoreDbContext();

            var product = new Product
            {
                Name = "Y22222222222222222222222225555555555555555555555",
                Price = 550,
                ProductType = (Models.Enums.ProductType)1,
            };

            db.Products.Add(product);

            db.SaveChanges();
        }

    }
}
