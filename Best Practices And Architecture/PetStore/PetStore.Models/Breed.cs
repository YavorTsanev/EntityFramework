using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using PetStore.Common;

namespace PetStore.Models
{
    public class Breed
    {
        public int Id { get; set; }
        
        [Required, MinLength(GlobalConstants.BreedNameMinLen),MaxLength(30)]
        public string Name { get; set; }

        public virtual ICollection<Pet> Pets { get; set; } = new HashSet<Pet>();

    }
}