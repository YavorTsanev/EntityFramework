using AutoMapper;
using ProductShop.DTO.Import;
using ProductShop.Models;

namespace ProductShop
{
    public class ProductShopProfile : Profile
    {
        public ProductShopProfile()
        {
            CreateMap<ImportUserDto, User>();
            CreateMap<ImportProductDto, Product>();
            CreateMap<ImportCategoryDto, Category>();
            CreateMap<ImportCategoryProductDto, CategoryProduct>();

            CreateMap<Product, ExportProductDto>().ForMember(x => x.BuyerName, o => o.MapFrom(x => x.Buyer.FirstName + " " + x.Buyer.LastName));
        }
    }
}
