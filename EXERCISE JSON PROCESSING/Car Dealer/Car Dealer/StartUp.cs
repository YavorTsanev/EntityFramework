using AutoMapper;
using AutoMapper.QueryableExtensions;
using CarDealer.Data;
using CarDealer.Dto.Export;
using CarDealer.Dto.Import;
using CarDealer.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
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

            //string json = File.ReadAllText("../../../Datasets/sales.json");

            var result = GetCarsWithTheirListOfParts(db);
            var directoryPath = @"../../../Results";

            Directory.CreateDirectory(directoryPath);

            File.WriteAllText(directoryPath + "/cars-and-parts.json", result);

            //Console.WriteLine(result);
        }

        public static void InitializeMapper()
        {
            Mapper.Initialize(cfg => cfg.AddProfile<CarDealerProfile>());
        }

        public static string ImportSuppliers(CarDealerContext context, string inputJson)
        {


            var suppliresDtos = JsonConvert.DeserializeObject<ImportSupplierDto[]>(inputJson);

            var suppliers = Mapper.Map<Supplier[]>(suppliresDtos);

            context.Suppliers.AddRange(suppliers);

            context.SaveChanges();

            return $"Successfully imported {suppliers.Length}.";
        }

        public static string ImportParts(CarDealerContext context, string inputJson)
        {

            var partDtos = JsonConvert.DeserializeObject<List<ImportPartDto>>(inputJson);

            var parts = Mapper.Map<List<Part>>(partDtos).Where(p => context.Suppliers.Any(s => s.Id == p.SupplierId));

            context.Parts.AddRange(parts);

            context.SaveChanges();

            return $"Successfully imported {context.Parts.ToList().Count}.";

        }

        public static string ImportCars(CarDealerContext context, string inputJson)
        {
            var cars = JsonConvert.DeserializeObject<ImportCarDto[]>(inputJson);

            foreach (var carDto in cars)
            {
                Car car = new Car
                {
                    Make = carDto.Make,
                    Model = carDto.Model,
                    TravelledDistance = carDto.TravelledDistance
                };

                context.Cars.Add(car);

                foreach (var partId in carDto.PartsId)
                {
                    PartCar partCar = new PartCar
                    {
                        CarId = car.Id,
                        PartId = partId
                    };

                    if (car.PartCars.FirstOrDefault(p => p.PartId == partId) == null)
                    {
                        context.PartCars.Add(partCar);
                    }
                }
            }

            context.SaveChanges();

            return $"Successfully imported {cars.Count()}.";
        }

        public static string ImportCustomers(CarDealerContext context, string inputJson)
        {
            var customersDto = JsonConvert.DeserializeObject<List<Dto.Export.ExportCustomerDto>>(inputJson);

            var customers = Mapper.Map<List<Customer>>(customersDto);

            context.Customers.AddRange(customers);

            context.SaveChanges();


            return $"Successfully imported {customers.Count}.";
        }

        public static string ImportSales(CarDealerContext context, string inputJson)
        {
            var salesDto = JsonConvert.DeserializeObject<List<ImportSaleDto>>(inputJson);

            var sales = Mapper.Map<List<Sale>>(salesDto);

            context.Sales.AddRange(sales);

            context.SaveChanges();

            return $"Successfully imported {sales.Count}.";
        }

        public static string GetOrderedCustomers(CarDealerContext context)
        {
            //var customers = context.Customers.OrderBy(c => c.BirthDate).ThenBy(c => c.IsYoungDriver).Select(c => new { c.Name, BirthDate = c.BirthDate.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture), c.IsYoungDriver });

            var costomersDto = context.Customers.OrderBy(c => c.BirthDate).ThenBy(c => c.IsYoungDriver).ProjectTo<ExportCustomerDto>();

            var settings = new JsonSerializerSettings
            {
                Formatting = Formatting.Indented,
            };

            return JsonConvert.SerializeObject(costomersDto, settings);
        }

        public static string GetCarsFromMakeToyota(CarDealerContext context)
        {
            var cars = context.Cars.Where(c => c.Make == "Toyota").OrderBy(c => c.Model).ThenByDescending(c => c.TravelledDistance).ProjectTo<ExportCarDtoExport>();

            var settings =new JsonSerializerSettings{Formatting = Formatting.Indented};

            return JsonConvert.SerializeObject(cars,settings);
        }

        public static string GetLocalSuppliers(CarDealerContext context)
        {
            var suppliers = context.Suppliers.Where(s => s.IsImporter == false).ProjectTo<ExportSupplierDto>();

            return JsonConvert.SerializeObject(suppliers, Formatting.Indented);
        }

        public static string GetCarsWithTheirListOfParts(CarDealerContext context)
        {
            var cars = context.Cars.Select(c => new { car = new { c.Make, c.Model, c.TravelledDistance }, parts = c.PartCars.Select(pc => new { Name = pc.Part.Name, Price = $"{pc.Part.Price:f2}"})} );



            return JsonConvert.SerializeObject(cars, Formatting.Indented);

        }

    }
}
