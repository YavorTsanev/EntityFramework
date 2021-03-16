using RealEstates.Data;
using RealEstates.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace RealEstates.Importer
{
    public class Program
    {
        static void Main(string[] args)
        {
            var jsonString = File.ReadAllText("imot.bg-raw-data-2020-07-23.json");

            var jsonProperties = JsonSerializer.Deserialize<List<JsonProperty>>(jsonString);

            var db = new RealEstateDbContext();

            var propertyService = new PropetiesService(db);

            foreach (var property in jsonProperties.Where(x => x.Price > 1000))
            {
                propertyService.Create
                    (
                    property.District,
                    property.Size,
                    property.Year,
                    property.Price,
                    property.Type,
                    property.BuildingType,
                    property.Floor,
                    property.TotalFloors
                    );
            }

            Console.WriteLine("Done!");

        }

        //"Url": "https://www.imot.bg/pcgi/imot.cgi?act=5&adv=1a159436674429522&slink=5p50ib&f1=1",
        //"Size": 12,
        //"Floor": 1,
        //"TotalFloors": 5,
        //"District": "град София, Стрелбище",
        //"Year": 2022,
        //"Type": "1-СТАЕН",
        //"BuildingType": "Тухла",
        //"Price": 17590


    }
}
