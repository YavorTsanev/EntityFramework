using AutoMapper;
using CarDealer.Dtos.Export;
using CarDealer.Dtos.Import;
using CarDealer.Models;
using System.Collections.Generic;
using System.Linq;

namespace CarDealer
{
    public class CarDealerProfile : Profile
    {
        public CarDealerProfile()
        {
            CreateMap<ImportSupplierDto, Supplier>();
            CreateMap<ImportPartDto, Part>();
            CreateMap<ImportCarDto, Car>();
            CreateMap<ImportCustomer, Customer>();
            CreateMap<ImportSaleDto, Sale>();

            CreateMap<Car, ExportCarDto>();
            CreateMap<Car, ExportBMVCarDto>();
            CreateMap<Supplier, ExportSupplierDto>();
            CreateMap<Car, ExportCarAndPartsDto>().ForMember(x => x.Parts, o => o.MapFrom(x => x.PartCars.Select( p => new ExportPartDto { Name = p.Part.Name, Price = p.Part.Price}).OrderByDescending(p => p.Price)));

            CreateMap<Customer, ExportCustomerAndSale>().ForMember(x => x.SpentMoney, o => o.MapFrom(x => x.Sales.SelectMany(s => s.Car.PartCars.Select(p => p.Part.Price)).Sum()));

            CreateMap<Sale, ExportSaleWithInfoDto>().ForMember(x => x.Car, o=>o.MapFrom(x => x.Car)).ForMember(x => x.Price, o => o.MapFrom(x => x.Car.PartCars.Select(p => p.Part.Price).Sum())).ForMember(x => x.PriceWithDiscount, o=> o.MapFrom(x => x.Car.PartCars.Select(p => p.Part.Price).Sum() - (x.Car.PartCars.Select(p => p.Part.Price).Sum() * x.Discount)/100));
        }
    }
}
