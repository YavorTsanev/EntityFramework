using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using PetStore.Models.Enums;

namespace PetStore.ServiceModels.Products.InputModels
{
    public class EditProductInputServiceModel
    {
        [Required, MinLength(3), MaxLength(50)]
        public string Name { get; set; }

        public string ProductType { get; set; }

        [Range(3, 50)]
        public decimal Price { get; set; }
    }
}
