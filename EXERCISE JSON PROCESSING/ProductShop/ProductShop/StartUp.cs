using AutoMapper;
using Newtonsoft.Json;
using ProductShop.Data;
using ProductShop.Models;
using System;
using System.IO;

namespace ProductShop
{
    public class StartUp
    {

        static void Main(string[] args)
        {
            var db = new ProductShopContext();

            

            var usersJson = File.ReadAllText("../../../Datasets/users.json");

            var result = ImportUsers(db, usersJson);

            Console.WriteLine(result);
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

        }
    }
}
