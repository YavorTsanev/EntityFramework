using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PetStore.Models
{
    public class Breed
    {
        public int Id { get; set; }
        
        [Required, MinLength(3)]
        public string Name { get; set; }

        public virtual ICollection<Pet> Pets { get; set; } = new HashSet<Pet>();

    }
}