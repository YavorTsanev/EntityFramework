using System;
using System.ComponentModel.DataAnnotations;
using PetStore.Models.Enums;

namespace PetStore.Models
{
    public class Product
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();

        [Required, MinLength(3),MaxLength(50)]
        public string Name { get; set; }

        public ProductType ProductType { get; set; }

        public decimal Price { get; set; }

        

    }
}
