using AutoMapper;
using ProductShop.Dto;
using ProductShop.Models;
using System.Linq;

namespace ProductShop
{
    public class ProductShopProfile : Profile
    {
        public ProductShopProfile()
        {
            CreateMap<User, UserSoldProducts>();

            CreateMap<Category, CategoiesByProducts>().ForMember(x => x.ProductsCount, o => o.MapFrom(c => c.CategoryProducts.Count)).ForMember(x => x.AveragePrice, o => o.MapFrom(c => string.Format("{0:f2}",c.CategoryProducts.Average(x => x.Product.Price)))).ForMember(x => x.TotalRevenue, o => o.MapFrom(x => x.CategoryProducts.Sum(x => x.Product.Price).ToString()));

        }
    }
}
