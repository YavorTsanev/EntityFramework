
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using PetStore.Common;

namespace PetStore.Models
{
    public class Client
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        [Required, MinLength(GlobalConstants.ClientUserNameMinLen),MaxLength(30)]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
        [Required,MinLength(GlobalConstants.ClientPasswordMinLen)]
        public string Email { get; set; }
        [Required, MinLength(3),MaxLength(50)]
        public string FirstName { get; set; }
        [Required, MinLength(3),MaxLength(50)]
        public string LastName { get; set; }
        public virtual ICollection<Pet> PetsBuyed { get; set; } = new HashSet<Pet>();
    }
}
