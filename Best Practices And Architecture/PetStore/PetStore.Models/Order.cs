using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;

namespace PetStore.Models
{
    public class Order
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();

        [Required, MinLength(3)]
        public string Town { get; set; }

        [Required, MinLength(5)]
        public string Address { get; set; }

        public string Notes { get; set; }

        public virtual ICollection<ClientProduct> ClientProducts { get; set; } = new HashSet<ClientProduct>();

        public decimal TotalPrice => ClientProducts.Sum(cp => cp.Product.Price * cp.Quantity);

    }
}
