using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Xml.Serialization;
using Cinema.Data.Models;

namespace Cinema.DataProcessor.ImportDto
{
    [XmlType("Customer")]
    public class CustomerImportDto
    {
        [Required]
        [MinLength(3)]
        [MaxLength(20)]
        public string FirstName { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(20)]
        public string LastName { get; set; }

        [Range(12, 110)]
        public int Age { get; set; }

        [Range(typeof(decimal), "0.01", "79228162514264337593543950335")]
        public decimal Balance { get; set; }

        [XmlArray("Tickets")]
        public List<TicketImportDto> Tickets { get; set; }

        [XmlType("Ticket")]
        public class TicketImportDto
        {

            public int ProjectionId { get; set; }

            [Range(typeof(decimal), "0.01", "79228162514264337593543950335")]
            public decimal Price { get; set; }
        }
    }

    //<Customer>
    //<FirstName>Randi</FirstName>
    //<LastName>Ferraraccio</LastName>
    //<Age>20</Age>
    //<Balance>59.44</Balance>
    //<Tickets>
    //<Ticket>
    //<ProjectionId>1</ProjectionId>
    //<Price>7</Price>
    //</Ticket>

}
