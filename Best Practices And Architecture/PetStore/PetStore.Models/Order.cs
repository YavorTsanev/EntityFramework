using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection.Metadata;
using System.Runtime.Serialization;
using System.Text;

namespace PetStore.Models
{
    public class Order
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();

        [Required, MinLength(3),MaxLength(40)]
        public string Town { get; set; }

        [Required, MinLength(5),MaxLength(70)]
        public string Address { get; set; }

        public string Notes { get; set; }

        public virtual ICollection<ClientProduct> ClientProducts { get; set; } = new HashSet<ClientProduct>();
        [NotMapped]
        public decimal TotalPrice => ClientProducts.Sum(cp => cp.Product.Price * cp.Quantity);

    }
}
