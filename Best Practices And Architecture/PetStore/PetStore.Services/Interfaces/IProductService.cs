using System;
using System.Collections.Generic;
using System.Text;
using PetStore.ServiceModels.Products.InputModels;
using PetStore.ServiceModels.Products.OutputModels;

namespace PetStore.Services.Interfaces
{
    public interface IProductService
    {
        void AddProduct(AddProductServiceViewModel model);

        ICollection<ListAllProductsServiceModel> GetAll();

        ICollection<ListAllProductsByProductTypeServiceModel> ListAllByProductType(string type);

        bool RemoveById(string id);

        bool RemoveByName(string name);

        ICollection<ListAllProductsByNameServiceModel> SearchByName(string searchString , bool caseSensitive);

        void EditProduct(string id, EditProductInputServiceModel model);

    }
}
