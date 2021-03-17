using Microsoft.Data.SqlClient.Server;
using RealEstates.Data;
using RealEstates.Services;
using System;
using System.IO;
using System.Text.Json;
using System.Text.Unicode;
using RealEstates.Models;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace RealEstates.ConsoleApplication
{
    public class StartUp
    {
        static void Main(string[] args)
        {
            Directory.CreateDirectory("../../../Result");
            var db = new RealEstateDbContext();

            //Console.Write("MinPrice: ");
            //var minPrice = int.Parse(Console.ReadLine());
            //Console.Write("MaxPrice: ");
            //var maxPrice = int.Parse(Console.ReadLine());
            //var property = new PropetiesService(db);
            //var properties = property.SearchByPrice(minPrice, maxPrice);



            var district = new DistrictService(db);
            var districts = district.GetTopDistrictsByAveragePrice();

            var options = new JsonSerializerOptions { WriteIndented = true, Encoder = System.Text.Encodings.Web.JavaScriptEncoder.Create(UnicodeRanges.All) };
            var json = JsonSerializer.Serialize(districts, options);



            File.WriteAllText("../../../Result/" + "Top10Districts.json", json);

            //Console.WriteLine(json);

            var tag = new Tag { }
        }
    }
}
