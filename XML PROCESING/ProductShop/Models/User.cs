﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ProductShop.Models
{
    public class User
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        public int? Age { get; set; }

        [InverseProperty("Seller")]
        public virtual ICollection<Product> ProductsSold { get; set; } = new HashSet<Product>();

        [InverseProperty("Buyer")]
        public virtual ICollection<Product> ProductsBought { get; set; } = new HashSet<Product>();
    }
}
