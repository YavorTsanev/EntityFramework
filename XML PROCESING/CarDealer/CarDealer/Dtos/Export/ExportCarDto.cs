using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace CarDealer.Dtos.Export
{
    [XmlType("car")]
    public class ExportCarDto
    {
        [XmlAttribute("make")]
        public string Make { get; set; }

        [XmlAttribute("model")]
        public string Model { get; set; }

        [XmlAttribute("travelled-distance")]
        public long TravelledDistance { get; set; }

    }

    //  <cars>
    //<car>
    //  <make>BMW</make>
    //  <model>1M Coupe</model>
    //  <travelled-distance>39826890</travelled-distance>
    //</car>

}
