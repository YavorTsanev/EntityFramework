using AutoMapper;
using CarDealer.Data;
using CarDealer.Dtos.Import;
using CarDealer.Models;
using ProductShop;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace CarDealer
{
    public class StartUp
    {
        private static readonly MapperConfiguration config = new MapperConfiguration(cfg => cfg.AddProfile<CarDealerProfile>());
        static IMapper mapper = config.CreateMapper();

        public static void Main(string[] args)
        {
            var db = new CarDealerContext();
            //db.Database.EnsureDeleted();
            //db.Database.EnsureCreated();

            var xmlStr = File.ReadAllText("../../../Datasets/parts.xml");
            var result = ImportParts(db, xmlStr);

            Console.WriteLine(result);
        }

        public static string ImportSuppliers(CarDealerContext context, string inputXml)
        {
            var supplierDtos = XmlConverter.Deserializer<ImportSupplierDto>(inputXml, "Suppliers");

            var suppliers = mapper.Map<List<Supplier>>(supplierDtos);

            context.Suppliers.AddRange(suppliers);

            context.SaveChanges();

            return $"Successfully imported {suppliers.Count}";
        }

        public static string ImportParts(CarDealerContext context, string inputXml)
        {

            var partDtos = XmlConverter.Deserializer<ImportPartDto>(inputXml, "Parts").Where( p => context.Suppliers.Select(x => x.Id).Contains(p.SupplierId)).ToList();

            var parts = mapper.Map<List<Part>>(partDtos);

            context.Parts.AddRange(parts);

            context.SaveChanges();

            return $"Successfully imported {parts.Count}";
        }
    }
}