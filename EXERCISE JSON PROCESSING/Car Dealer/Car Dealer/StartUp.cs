using AutoMapper;
using CarDealer.Data;
using CarDealer.Dto.Import;
using CarDealer.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace CarDealer
{
    public class StartUp
    {
        static void Main(string[] args)
        {
            InitializeMapper();

            var db = new CarDealerContext();

            string json = File.ReadAllText("../../../Datasets/cars.json");

            var result = ImportCars(db,json);

            Console.WriteLine(result);
        }

        public static void InitializeMapper()
        {
            Mapper.Initialize(cfg => cfg.AddProfile<CarDealerProfile>());
        }

        public static string ImportSuppliers(CarDealerContext context, string inputJson)
        {
            

            var suppliresDtos =  JsonConvert.DeserializeObject<SupplierDto[]>(inputJson);

            var suppliers = Mapper.Map<Supplier[]>(suppliresDtos);

            context.Suppliers.AddRange(suppliers);

            context.SaveChanges();

            return $"Successfully imported {suppliers.Length}.";
        }

        public static string ImportParts(CarDealerContext context, string inputJson)
        {

            var partDtos = JsonConvert.DeserializeObject<List<PartDto>>(inputJson);

            var parts = Mapper.Map<List<Part>>(partDtos).Where(p => context.Suppliers.Any(s => s.Id == p.SupplierId));

            context.Parts.AddRange(parts);

            context.SaveChanges();

            return $"Successfully imported {context.Parts.ToList().Count}.";

        }

        public static string ImportCars(CarDealerContext context, string inputJson)
        {
            var
        }

    }
}
