using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using PetStore.Models.Enums;

namespace PetStore.ServiceModels.Products.OutputModels
{
    public class ListAllProductsServiceModel
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string ProductType { get; set; }

        public decimal Price { get; set; }
    }
}
