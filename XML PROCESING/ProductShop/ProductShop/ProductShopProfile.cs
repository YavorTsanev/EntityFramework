using AutoMapper;
using ProductShop.DTO.Export;
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

            CreateMap<User, ExportUserSoldProducts>();

            CreateMap<Product, ExportProductDto>();
            

        }
    }
}
