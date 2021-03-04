using System;
using System.Collections.Generic;
using System.Text;

namespace CarDealer.Dto.Import
{
    public class CarDto
    {
        public string Make { get; set; }

        public string Model { get; set; }

        public int TravelledDistance { get; set; }

        public List<int> PartIds { get; set; }
    }
}
