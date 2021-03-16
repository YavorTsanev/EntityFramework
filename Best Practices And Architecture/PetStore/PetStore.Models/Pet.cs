using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.Design;
using PetStore.Models.Enums;

namespace PetStore.Models
{
    public class Pet
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();

        [Required, MinLength(3)]
        public string Name { get; set; }

        public Gender Gender { get; set; }

        public byte Age { get; set; }

        public bool IsSold { get; set; }

        public decimal Price { get; set; }

        public int BreedId { get; set; }
        public virtual Breed Breed { get; set; }

        public string ClientId { get; set; }

        public virtual Client Client { get; set; }


    }
}
