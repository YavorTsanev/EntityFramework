using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using PetStore.Common;
using PetStore.Data;
using PetStore.Models;
using PetStore.Models.Enums;
using PetStore.ServiceModels.Products.InputModels;
using PetStore.ServiceModels.Products.OutputModels;

namespace PetStore.Services
{
    public class ProductServiece
    {
        private readonly PetStoreDbContext db;
        private readonly IMapper mapper;

        public ProductServiece(PetStoreDbContext db, IMapper mapper)
        {
            this.db = db;
            this.mapper = mapper;
        }

        public void AddProduct(AddProductServiceViewModel model)
        {
            var product = mapper.Map<Product>(model);

            db.Products.Add(product);

            db.SaveChanges();
        }

        public ICollection<ListAllProductsByProductTypeServiceModel> ListAllByProductType(string type)
        {

            var hasParsed = Enum.TryParse(type, true, out ProductType productType);

            if (!hasParsed)
            {
                throw new ArgumentException(ExceptionMessages.InvalidProductType);
            }

            return db.Products.Where(x => x.ProductType == productType)
                .ProjectTo<ListAllProductsByProductTypeServiceModel>(mapper.ConfigurationProvider).ToList();
        }
    }
}
