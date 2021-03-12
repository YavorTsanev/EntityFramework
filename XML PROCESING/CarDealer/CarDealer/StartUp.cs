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

            var result = GetSalesWithAppliedDiscount(db);

            File.WriteAllText("../../../Results/" + "sales-discounts.xml", result);
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

        public static string GetCarsFromMakeBmw(CarDealerContext context)
        {
            //Get all cars from make BMW and order them by model alphabetically and by travelled distance descending.

            var bmvCars = context.Cars.Where(c => c.Make == "BMW").OrderBy(x => x.Model).ThenByDescending(x => x.TravelledDistance).ProjectTo<ExportBMVCarDto>(config).ToList();

            return XmlConverter.Serialize(bmvCars, "cars");

        }

        public static string GetLocalSuppliers(CarDealerContext context)
        {
            //Get all suppliers that do not import parts from abroad.Get their id, name and the number of parts they can offer to supply.

            var suppliers = context.Suppliers.Where(x => !x.IsImporter).ProjectTo<ExportSupplierDto>(config).ToList();

            return XmlConverter.Serialize(suppliers, "suppliers");
        }

        public static string GetCarsWithTheirListOfParts(CarDealerContext context)
        {
            //Get all cars along with their list of parts.For the car get only make, model and travelled distance and for the parts get only name and price and sort all parts by price(descending).Sort all cars by travelled distance(descending) then by model(ascending).Select top 5 records.

            var carsAndParts = context.Cars.ProjectTo<ExportCarAndPartsDto>(config).OrderByDescending(x => x.TravelledDistance).ThenBy(x => x.Model).Take(5).ToList();

            return XmlConverter.Serialize(carsAndParts, "cars");
        }

        public static string GetTotalSalesByCustomer(CarDealerContext context)
        {

            //Get all customers that have bought at least 1 car and get their names, bought cars count and total spent money on cars.Order the result list by total spent money descending.

            var customersAndSales = context.Customers.Where(c => c.Sales.Any()).ProjectTo<ExportCustomerAndSale>(config).OrderByDescending(x => x.SpentMoney).ToList();



            return XmlConverter.Serialize(customersAndSales, "customers");
        }

        public static string GetSalesWithAppliedDiscount(CarDealerContext context)
        {
            //Get all sales with information about the car, customer and price of the sale with and without discount.

            var salesWithInfo = context.Sales.ProjectTo<ExportSaleWithInfoDto>(config).ToList();

            return XmlConverter.Serialize(salesWithInfo, "sales");
        }
    }
}