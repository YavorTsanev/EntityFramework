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

        }
    }
}
