using AutoMapper;
using AutoMapper.QueryableExtensions;
using Newtonsoft.Json;
using ProductShop.Data;
using ProductShop.Dto;
using ProductShop.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ProductShop
{
    public class StartUp
    {
        private static string directoryPath = "../../../Datasets/Result";

        static void Main(string[] args)
        {
            var db = new ProductShopContext();

            InitializeMapper();

            //var usersJson = File.ReadAllText("../../../Datasets/categories-products.json");

            var result = GetSoldProducts(db);

            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }

            

            File.WriteAllText(directoryPath + "/users-sold-products.json", result);

            //Console.WriteLine(result);
        }

        private static void InitializeMapper()
        {
            Mapper.Initialize(cfg => { cfg.AddProfile<ProductShopProfile>(); });
        }

        public static string ImportUsers(ProductShopContext context, string inputJson)
        {

            var users = JsonConvert.DeserializeObject<User[]>(inputJson);

            context.AddRange(users);
            context.SaveChanges();

            return $"Successfully imported {users.Length}";

        }

        public static string ImportProducts(ProductShopContext context, string inputJson)
        {
            var products = JsonConvert.DeserializeObject<List<Product>>(inputJson);

            context.Products.AddRange(products);
            context.SaveChanges();

            return $"Successfully imported {products.Count}";
        }

        public static string ImportCategories(ProductShopContext context, string inputJson)
        {

            var categories = JsonConvert.DeserializeObject<List<Category>>(inputJson).Where(x => x.Name != null).ToList();

            context.Categories.AddRange(categories);
            context.SaveChanges();


            return $"Successfully imported {categories.Count}";


        }

        public static string ImportCategoryProducts(ProductShopContext context, string inputJson)
        {
            var categoryProducts = JsonConvert.DeserializeObject<List<CategoryProducts>>(inputJson);

            context.AddRange(categoryProducts);
            context.SaveChanges();

            return $"Successfully imported {categoryProducts.Count}";
        }

        public static string GetProductsInRange(ProductShopContext context)
        {
            var products = context.Products.Where(p => p.Price >= 500 && p.Price <= 1000).Select(p => new { name = p.Name, price = p.Price, seller = p.Seller.FirstName + " " + p.Seller.LastName }).OrderBy(p => p.price).ToList();

            return JsonConvert.SerializeObject(products, Formatting.Indented);
        }

        public static string GetSoldProducts(ProductShopContext context)
        {
            var users = context.Users.Where(u => u.ProductsSold.Any(b => b.Buyer != null)).OrderBy(x => x.LastName).ThenBy(x => x.FirstName).ProjectTo<UserSoldProducts>().ToList();

            return JsonConvert.SerializeObject(users,Formatting.Indented);
        }
    }
}
