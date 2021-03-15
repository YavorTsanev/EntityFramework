using Microsoft.Data.SqlClient.Server;
using RealEstates.Data;
using RealEstates.Services;
using System;
using System.IO;
using System.Text.Json;
using System.Text.Unicode;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace RealEstates.ConsoleApplication
{
    public class StartUp
    {
        static void Main(string[] args)
        {
            var db = new RealEstateDbContext();

            //var district = new DistrictService(db);
            //Directory.CreateDirectory("../../../Result");


            //var districts = district.GetTopDistrictsByAveragePrice();

            //var options = new JsonSerializerOptions { WriteIndented = true, Encoder = System.Text.Encodings.Web.JavaScriptEncoder.Create(UnicodeRanges.All) };
            //var json = JsonSerializer.Serialize(districts, options);

            

            //File.WriteAllText("../../../Result/" + "Top10Districts.json", json);

            //Console.WriteLine(json);




        }
    }
}
