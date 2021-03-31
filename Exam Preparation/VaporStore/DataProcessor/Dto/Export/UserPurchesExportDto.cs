using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using VaporStore.Data.Models;

namespace VaporStore.DataProcessor.Dto.Export
{
    [XmlType("User")]
    public class UserPurchesExportDto
    {
        [XmlAttribute("username")]
        public string Username { get; set; }

        [XmlArray("Purchases")]
        public PurchaseExportDto[] Purchases { get; set; }

        [XmlType("Purchase")]
        public class PurchaseExportDto
        {
            public string Card { get; set; }

            public string Cvc { get; set; }

            public string Date { get; set; }

            public GameXmlExportDto Game { get; set; }

            public class GameXmlExportDto
            {
                [XmlAttribute("title")]
                public string Name { get; set; }

                public string Genre { get; set; }

                public decimal Price { get; set; }
            }
        }

        public decimal TotalSpent { get; set; }
    }

    //<User username = "mgraveson" >
    //< Purchases >
    //< Purchase >
    //< Card > 7991 7779 5123 9211</Card>
    //<Cvc>340</Cvc>
    //<Date>2017-08-31 17:09</Date>
    //<Game title = "Counter-Strike: Global Offensive" >
    //< Genre > Action </ Genre >
    //< Price > 12.49 </ Price >
    //</ Game >
    //</ Purchase >
}
