using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace BookShop.DataProcessor.ExportDto
{
    [XmlType("Book")]
    public class BookExportDto
    {
        [XmlAttribute]
        public int Pages { get; set; }

        public string Name { get; set; }

        public string Date { get; set; }
    }

    //  <Book Pages = "4881" >
    //< Name > Sierra Marsh Fern</Name>
    //<Date>03/18/2016</Date>
    //</Book>

}

