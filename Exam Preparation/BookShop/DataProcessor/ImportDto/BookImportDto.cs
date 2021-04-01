using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Xml.Serialization;
using BookShop.Data.Models.Enums;

namespace BookShop.DataProcessor.ImportDto
{
    [XmlType("Book")]
    public class BookImportDto
    {
        [Required]
        [MinLength(3)]
        [MaxLength(30)]
        public string Name { get; set; }

        public int Genre { get; set; }

        [Range(typeof(decimal), "0.01", "79228162514264337593543950335")]
        public decimal Price { get; set; }

        [Range(50, 5000)]
        public int Pages { get; set; }

        public DateTime PublishedOn { get; set; }

    }

    //<Book>
    //<Name>Hairy Torchwood</Name>
    //<Genre>3</Genre>
    //<Price>41.99</Price>
    //<Pages>3013</Pages>
    //<PublishedOn>01/13/2013</PublishedOn>
    //</Book>
}
