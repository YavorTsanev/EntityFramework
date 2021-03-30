using System;
using System.Collections.Generic;
using System.Text;

namespace SoftJail.DataProcessor.ExportDto
{
    public class PrisonerOfficerExportDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int CellNumber { get; set; }

        public ICollection<OfficerExportDto> Officers { get; set; }

        public decimal TotalOfficerSalary { get; set; }

        public class OfficerExportDto
        {
            public string OfficerName { get; set; }

            public string Department { get; set; }
        }
    }
    //{
    //"Id": 3,
    //"Name": "Binni Cornhill",
    //"CellNumber": 503,
    //"Officers": [
    //{
    //    "OfficerName": "Hailee Kennon",
    //    "Department": "ArtificialIntelligence"
    //},

}
