﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PetStore.Models
{
    public class ClientProduct
    {
        [Required]
        public string ClientId { get; set; }
        public virtual Client Client { get; set; }

        [Required]
        public string ProductId { get; set; }
        public virtual Product Product { get; set; }

        [Range(1, int.MaxValue)]
        public int Quantity { get; set; }


        public string OrderId { get; set; }
        public Order Order { get; set; }
    }
}
