
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PetStore.Models
{
    public class Client
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        [Required, MinLength(6)]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
        [Required,MinLength(6)]
        public string Email { get; set; }
        [Required, MinLength(3)]
        public string FirstName { get; set; }
        [Required, MinLength(3)]
        public string LastName { get; set; }
        public virtual ICollection<Pet> PetsBuyed { get; set; } = new HashSet<Pet>();
    }
}
