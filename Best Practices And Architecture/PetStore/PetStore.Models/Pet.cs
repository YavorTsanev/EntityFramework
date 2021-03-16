using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.Design;

namespace PetStore.Models
{
    public class Pet
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();

        public string Name { get; set; }


    }
}
