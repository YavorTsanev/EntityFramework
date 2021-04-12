using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Xml.Serialization;
using Cinema.Data.Models;

namespace Cinema.DataProcessor.ImportDto
{
    [XmlType("Projection")]
    public class ProjectionImportDto
    {
        public int MovieId { get; set; }

        public string DateTime { get; set; }

    }

    //<Projection>
    //<MovieId>38</MovieId>
    //<DateTime>2019-04-27 13:33:20</DateTime>
    //</Projection>

}
