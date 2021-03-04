using System;
using System.Collections.Generic;
using System.Text;

namespace CarDealer.Models
{
    public class Car
    {
        public int Id { get; set; }

        public string Make { get; set; }

        public string Model { get; set; }

        public int TravelledDistance { get; set; }

        public virtual ICollection<PartCar> PartCars { get; set; } = new HashSet<PartCar>();

        public virtual ICollection<Sale> Sales { get; set; } = new HashSet<Sale>();
    }
}
