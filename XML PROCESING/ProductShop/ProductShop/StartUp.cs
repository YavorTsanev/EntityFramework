using AutoMapper;
using ProductShop.Data;
using ProductShop.DTO.Import;
using ProductShop.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using System.Xml.Linq;
using System.Linq;
using AutoMapper.QueryableExtensions;

namespace ProductShop
{
    public class StartUp
    {
        private static readonly MapperConfiguration config = new MapperConfiguration(cfg => cfg.AddProfile<ProductShopProfile>());

        private static readonly IMapper mapper = InitializeMapper();

        public static void Main(string[] args)
        {
            var db = new ProductShopContext();

            //var xmlString = File.ReadAllText("../../../Datasets/categories-products.xml");
            Directory.CreateDirectory("../../../Results");

            var result = GetProductsInRange(db);

            File.WriteAllText("../../../Results" + "products-in-range.xml", result);

        }


        private static IMapper InitializeMapper()
        {
            var config = new MapperConfiguration(cfg => cfg.AddProfile<ProductShopProfile>());

            return config.CreateMapper();
        }

        public static string ImportUsers(ProductShopContext context, string inputXml)
        {
            var xmlSerializer = new XmlSerializer(typeof(List<ImportUserDto>), new XmlRootAttribute("Users"));

            var usersDto =  (List<ImportUserDto>)xmlSerializer.Deserialize(new StringReader(inputXml));
            var mapper = InitializeMapper();

            var users =  mapper.Map<List<User>>(usersDto);

            context.Users.AddRange(users);

            context.SaveChanges();

            return $"Successfully imported {users.Count}";

        }

        public static string ImportProducts(ProductShopContext context, string inputXml)
        {
            var xmlSerializer = new XmlSerializer(typeof(List<ImportProductDto>), new XmlRootAttribute("Products"));

            var productDtos = (List<ImportProductDto>)xmlSerializer.Deserialize(new StringReader(inputXml));
            var mapper = InitializeMapper();

            var products = mapper.Map<List<Product>>(productDtos);

            context.Products.AddRange(products);
            context.SaveChanges();

            return $"Successfully imported {products.Count}";
        }

        public static string ImportCategories(ProductShopContext context, string inputXml)
        {
            var xmlSerializer = new XmlSerializer(typeof(List<ImportCategoryDto>), new XmlRootAttribute("Categories"));

            var categoryDtos = (List<ImportCategoryDto>)xmlSerializer.Deserialize(new StringReader(inputXml));

            //var mapper = InitializeMapper();
            var categories = mapper.Map<List<Category>>(categoryDtos).Where(c => c.Name != null).ToList();

            context.Categories.AddRange(categories);
            context.SaveChanges();

            return $"Successfully imported {categories.Count}";
        }

        public static string ImportCategoryProducts(ProductShopContext context, string inputXml)
        {
            var xmlSerializer = new XmlSerializer(typeof(List<ImportCategoryProductDto>), new XmlRootAttribute("CategoryProducts"));

            var categoryProductDtos = (List<ImportCategoryProductDto>)xmlSerializer.Deserialize(new StringReader(inputXml));

            var categoryProducts = mapper.Map<List<CategoryProduct>>(categoryProductDtos).Where(x => x.ProductId != 0 && x.CategoryId != 0).ToList();

            context.CategoryProducts.AddRange(categoryProducts);
            context.SaveChanges();

            return $"Successfully imported {categoryProducts.Count}";
        }

        public static string GetProductsInRange(ProductShopContext context)
        {
            var products = context.Products.Where(x => x.Price >= 500 && x.Price <= 1000).ProjectTo<ExportProductDto>(config).OrderBy(x => x.Price).Take(10).ToList();

            var result = XmlConverter.Serialize(products, "Products");

            return result;
        }
    }
}



