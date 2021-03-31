using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Xml.Serialization;
using VaporStore.Data.Models;
using VaporStore.Data.Models.Enums;

namespace VaporStore.DataProcessor.Dto.Import
{
    [XmlType("Purchase")]
    public class PurchaseImportDto
    {
        [Required ,XmlAttribute("title")]
        public string Game { get; set; }

        [Required, XmlElement("Type")]
        public PurchaseType? Type { get; set; }

        [Required, RegularExpression(@"^[0-9A-Z]{4}-[0-9A-Z]{4}-[0-9A-Z]{4}$"),XmlElement("Key")]
        public string ProductKey { get; set; }

        [Required, RegularExpression(@"^[0-9]{4} [0-9]{4} [0-9]{4} [0-9]{4}$"), XmlElement("Card")]
        public string Card { get; set; }

        [Required, XmlElement("Date")]
        public string Date { get; set; } 






    }
    //<Purchase title = "Dungeon Warfare 2" >
    //< Type > Digital </ Type >
    //< Key > ZTZ3 - 0D2S-G4TJ</Key>
    //<Card>1833 5024 0553 6211</Card>
    //<Date>07/12/2016 05:49</Date>
    //</Purchase>



}
