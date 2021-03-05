using System;
using System.Collections.Generic;
using System.Text;

namespace CarDealer.Dto.Import
{
    public class ImportSaleDto
    {
        public decimal Discount { get; set; }

        public int CarId { get; set; }

        public int CustomerId { get; set; }
    }
}
