using AutoMapper;
using AutoMapper.QueryableExtensions;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
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

            var result = GetUsersWithProducts(db);

            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }



            File.WriteAllText(directoryPath + "/users-and-products.json", result);

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
            var categoryProducts = JsonConvert.DeserializeObject<List<CategoryProduct>>(inputJson);

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

            return JsonConvert.SerializeObject(users, Formatting.Indented);
        }

        public static string GetCategoriesByProductsCount(ProductShopContext context)
        {
            var categories = context.Categories.ProjectTo<CategoiesByProducts>().OrderByDescending(x => x.ProductsCount).ToList();

            //var categories = context.Categories.Select(x => new { category = x.Name, productsCount = x.CategoryProducts.Count, averagePrice =string.Format("{0:f2}",x.CategoryProducts.Average(x => x.Product.Price)) , totalRevenue = $"{x.CategoryProducts.Sum(x => x.Product.Price):f2}"}).OrderByDescending(x => x.productsCount).ToList();

            return JsonConvert.SerializeObject(categories, Formatting.Indented);
        }

        public static string GetUsersWithProducts(ProductShopContext context)
        {
            //var usersWithProducts = new
            //{
            //    usersCount = context.Users.Count(u => u.ProductsSold.Any(p => p.Buyer != null)),
            //    users = context.Users.Where(u => u.ProductsSold.Any(p => p.Buyer != null)).OrderByDescending(x => x.ProductsBought.Count(p => p.Buyer != null))
            //    .Select(u => new
            //    {
            //        firstName = u.FirstName,
            //        lastName = u.LastName,
            //        age = u.Age,
            //        soldProducts = new
            //        {
            //            count = u.ProductsSold.Count(p => p.Buyer != null),
            //            products = u.ProductsSold.Where(p => p.Buyer != null)
            //          .Select(p => new
            //          {
            //              name = p.Name,
            //              price = p.Price
            //          })
            //        }
            //    })
            //};

            var users = context
                .Users
                .Where(u => u.ProductsSold.Any(ps => ps.Buyer != null))
                .OrderByDescending(u => u.ProductsSold.Count(ps => ps.Buyer != null))
                .Select(u => new
                {
                    u.FirstName,
                    u.LastName,
                    u.Age,
                    SoldProducts = new
                    {
                        Count = u.ProductsSold.Count(ps => ps.Buyer != null),
                        Products = u.ProductsSold
                            .Where(p => p.Buyer != null)
                            .Select(p => new
                            {
                                p.Name,
                                p.Price
                            })
                    }
                })
                .ToList();

            var result = new
            {
                UsersCount = users.Count,
                Users = users
            };

            var settings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };

            var usersJson = JsonConvert.SerializeObject(result, Formatting.Indented, settings);

            return usersJson;
        }
    }
}
