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
using PetStore.Services.Interfaces;

namespace PetStore.Services
{
    public class ProductServiece : IProductService
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
            try
            {
                var product = mapper.Map<Product>(model);

                db.Products.Add(product);

                db.SaveChanges();
            }
            catch (Exception)
            {
                throw new ArgumentException(ExceptionMessages.InvalidProductType);
            }

        }

        public ICollection<ListAllProductsServiceModel> GetAll()
        {
            return db.Products.ProjectTo<ListAllProductsServiceModel>(mapper.ConfigurationProvider).ToList();
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

        public bool RemoveById(string id)
        {
            var productToRemove = db.Products.Find(id);

            if (productToRemove == null)
            {
                throw new ArgumentNullException(ExceptionMessages.NullProduct);
            }

            db.Remove(productToRemove);

            return db.SaveChanges() == 1;
        }

        public bool RemoveByName(string name)
        {
            var productToRemove = db.Products.FirstOrDefault(x => x.Name == name);

            if (productToRemove == null)
            {
                throw new ArgumentNullException(ExceptionMessages.NullProduct);
            }

            db.Remove(productToRemove);

            return db.SaveChanges() == 1;

        }

        public ICollection<ListAllProductsByNameServiceModel> SearchByName(string searchString, bool caseSensitive)
        {
            return caseSensitive
                ? db.Products.Where(x => x.Name.Contains(searchString))
                    .ProjectTo<ListAllProductsByNameServiceModel>(mapper.ConfigurationProvider).ToList()
                : db.Products.Where(x => x.Name.ToLower().Contains(searchString.ToLower()))
                    .ProjectTo<ListAllProductsByNameServiceModel>(mapper.ConfigurationProvider).ToList();
        }

        public void EditProduct(string id, EditProductInputServiceModel model)
        {
            try
            {
                var product = mapper.Map<Product>(model);

                var productToUpdate = db.Products.Find(id);

                if (productToUpdate == null)
                {
                    throw new ArgumentException(ExceptionMessages.NullProduct);
                }

                productToUpdate.Name = product.Name;
                productToUpdate.ProductType = product.ProductType;
                productToUpdate.Price = product.Price;

                db.SaveChanges();
            }
            catch (ArgumentException ae)
            {
                throw ae;
            }
            catch (Exception)
            {
                throw new ArgumentException(ExceptionMessages.InvalidProductType);
            }

        }
    }
}
