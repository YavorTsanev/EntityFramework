using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using PetStore.Models;
using PetStore.Models.Enums;
using PetStore.ServiceModels.Products.InputModels;
using PetStore.ServiceModels.Products.OutputModels;

namespace PetStore.Mapping
{
    public class PetStoreProfile : Profile
    {
        public PetStoreProfile()
        {
            CreateMap<AddProductServiceViewModel, Product>().ForMember(x => x.ProductType,
                o => o.MapFrom(x => Enum.Parse<ProductType>(x.ProductType)));

            CreateMap<Product, ListAllProductsByProductTypeServiceModel>();

            CreateMap<Product, ListAllProductsServiceModel>()
                .ForMember(x => x.ProductType, o => o.MapFrom(x => x.ProductType.ToString()));

            CreateMap<Product, ListAllProductsByNameServiceModel>()
                .ForMember(x => x.ProductType, o => o.MapFrom(x => x.ProductType.ToString()));

            CreateMap<EditProductInputServiceModel, Product>().ForMember(x => x.ProductType,
                o => o.MapFrom(x => Enum.Parse<ProductType>(x.ProductType)));
        }
    }
}
