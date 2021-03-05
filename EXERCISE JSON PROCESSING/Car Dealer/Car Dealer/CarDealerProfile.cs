using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using AutoMapper;
using CarDealer.Dto.Export;
using CarDealer.Dto.Import;
using CarDealer.Models;

namespace CarDealer
{
    public class CarDealerProfile : Profile
    {
        public CarDealerProfile()
        {
            CreateMap<ImportSupplierDto, Supplier>();

            CreateMap<ImportPartDto, Part>();

            CreateMap<CustomerDtoImport, Customer>();

            CreateMap<ImportSaleDto, Sale>();

           CreateMap<Customer, ExportCustomerDto>().ForMember(x => x.BirthDate, o => o.MapFrom(x => x.BirthDate.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture)));

            CreateMap<Supplier, ExportSupplierDto>();

            CreateMap<Customer, ExportCustomerTotalSlaes>().ForMember(x => x.FullName, o => o.MapFrom(x => x.Name)).ForMember(x => x.BoughtCars, o => o.MapFrom(x => x.Sales.Count)).ForMember(x => x.SpentMoney, o => o.MapFrom(x => x.Sales.SelectMany(s => s.Car.PartCars.Select(p => p.Part.Price)).Sum()));

        }
    }
}
