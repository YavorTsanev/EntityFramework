using AutoMapper;
using CarDealer.Dtos.Export;
using CarDealer.Dtos.Import;
using CarDealer.Models;

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
        }
    }
}
