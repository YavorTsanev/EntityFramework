using PetStore.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PetStore.ServiceModels.Products.InputModels
{
    public class AddProductServiceViewModel
    {
        [Required, MinLength(3), MaxLength(50)]
        public string Name { get; set; }

        public ProductType ProductType { get; set; }

        [Range(3, 50)]
        public decimal Price { get; set; }
    }
}
