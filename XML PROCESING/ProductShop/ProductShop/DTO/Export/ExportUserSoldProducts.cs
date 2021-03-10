using ProductShop.DTO.Import;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace ProductShop.DTO.Export
{
    [XmlType("User")]
    public class ExportUserSoldProducts
    {
        [XmlElement("firstName")]
        public string FirstName { get; set; }

        [XmlElement("lastName")]
        public string LastName { get; set; }

        [XmlArray("soldProducts")]
        public List<ExportProductDto> ProductsSold { get; set; }
    }
}
