using AutoMapper;
using ProductShop.DTO.Export;
using ProductShop.DTO.Import;
using ProductShop.Models;
using System.Linq;

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

            CreateMap<User, ExportUserDto>();

            CreateMap<Product, ExportProductDto>();

            CreateMap<Category, ExportCategoryDto>().ForMember(x => x.Count, o => o.MapFrom(x => x.CategoryProducts.Count)).ForMember(x => x.AveragePrice, o => o.MapFrom(x =>x.CategoryProducts.Select(x => x.Product.Price).Average())).ForMember(x => x.TotalRevenue, o => o.MapFrom(x => x.CategoryProducts.Select(x => x.Product.Price).Sum()));
        }
    }
}
