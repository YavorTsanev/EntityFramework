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

            var result = ImportCars(db, json);

            Console.WriteLine(result);
        }

        public static void InitializeMapper()
        {
            Mapper.Initialize(cfg => cfg.AddProfile<CarDealerProfile>());
        }

        public static string ImportSuppliers(CarDealerContext context, string inputJson)
        {


            var suppliresDtos = JsonConvert.DeserializeObject<SupplierDto[]>(inputJson);

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
            var cars = JsonConvert.DeserializeObject<CarDto[]>(inputJson);

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
            var customersDto = JsonConvert.DeserializeObject<List<CustomerDto>>(inputJson);

            var customers = Mapper.Map<List<Customer>>(customersDto);

            context.Customers.AddRange(customers);

            context.SaveChanges();


            return $"Successfully imported {customers.Count}.";
        }

    }
}
