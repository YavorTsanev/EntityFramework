using AutoMapper;
using AutoMapper.QueryableExtensions;
using CarDealer.Data;
using CarDealer.Dtos.Export;
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
            Directory.CreateDirectory("../../../Results");
            //var xmlStr = File.ReadAllText("../../../Datasets/sales.xml");
            //var result = ImportSales(db, xmlStr);
            //Console.WriteLine(result);

            var result = GetCarsWithDistance(db);

            File.WriteAllText("../../../Results/" + "cars.xml", result);
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

        public static string ImportCars(CarDealerContext context, string inputXml)
        {
            var carsDtos = XmlConverter.Deserializer<ImportCarDto>(inputXml, "Cars");
            var cars = new List<Car>();
            foreach (var carDto in carsDtos)
            {

                var uniquePartSIds = carDto.Parts.Select(x => x.Id).Distinct().ToList();
                var availibleParts = uniquePartSIds.Where(id => context.Parts.Select(x => x.Id).Contains(id)).ToList();

                var car = new Car
                {
                    Make = carDto.Make,
                    Model = carDto.Model,
                    TravelledDistance = carDto.TravelledDistance,
                    PartCars = availibleParts.Select(id => new PartCar 
                    { 
                        PartId = id
                    })
                    .ToList()
                };

                cars.Add(car);
            }
            context.Cars.AddRange(cars);
            context.SaveChanges();

            return $"Successfully imported {cars.Count}";
        }

        public static string ImportCustomers(CarDealerContext context, string inputXml)
        {
            var customersDtos = XmlConverter.Deserializer<ImportCustomer>(inputXml, "Customers");

            var customers = mapper.Map<List<Customer>>(customersDtos);

            context.Customers.AddRange(customers);

            context.SaveChanges();

            return $"Successfully imported {customers.Count}";
        }

        public static string ImportSales(CarDealerContext context, string inputXml)
        {

            var salesDtos = XmlConverter.Deserializer<ImportSaleDto>(inputXml, "Sales").Where(s => context.Cars.Select(x => x.Id).Contains(s.CarId));

             var sales = mapper.Map<List<Sale>>(salesDtos);

            context.Sales.AddRange(sales);

            context.SaveChanges();

            return $"Successfully imported {sales.Count}";
        }

        public static string GetCarsWithDistance(CarDealerContext context)
        {
            //Get all cars with distance more than 2,000,000.Order them by make, then by model alphabetically. Take top 10 records.

            var cars = context.Cars.Where(c => c.TravelledDistance > 2000000).OrderBy(c => c.Make).ThenBy(x => x.Model).Take(10).ProjectTo<ExportCarDto>(config).ToList();

            return XmlConverter.Serialize(cars, "cars");
        }
    }
}