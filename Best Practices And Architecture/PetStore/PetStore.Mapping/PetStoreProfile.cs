using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using PetStore.Models;
using PetStore.ServiceModels.Products.InputModels;
using PetStore.ServiceModels.Products.OutputModels;

namespace PetStore.Mapping
{
    public class PetStoreProfile : Profile
    {
        public PetStoreProfile()
        {
            CreateMap<AddProductServiceViewModel, Product>();

            CreateMap<Product, ListAllProductsByProductTypeServiceModel>();
        }
    }
}
