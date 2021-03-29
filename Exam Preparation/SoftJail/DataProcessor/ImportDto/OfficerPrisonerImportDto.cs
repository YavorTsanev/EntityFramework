using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Xml.Serialization;
using SoftJail.Data.Models.Enums;

namespace SoftJail.DataProcessor.ImportDto
{
    [XmlType("Officer")]
    public class OfficerPrisonerImportDto
    {
        [XmlElement("Name")]
        [Required, MinLength(3), MaxLength(30)]
        public string FullName { get; set; }

        [XmlElement("Money")]
        [Range(typeof(decimal), "0", "79228162514264337593543950335")]
        public decimal Salary { get; set; }

        [EnumDataType(typeof(Position))]
        [XmlElement("Position")]
        public string Position { get; set; }

        [EnumDataType(typeof(Weapon))]
        [XmlElement("Weapon")]
        public string Weapon { get; set; }

        public int DepartmentId { get; set; }

        [XmlArray("Prisoners")]
        public List<PrisonerImportDto> Prisoners { get; set; }

        [XmlType("Prisoner")]
        public class PrisonerImportDto
        {
            [XmlAttribute("id")]
            public int Id { get; set; }
        }
    }

    //<Officers>
    //<Officer>
    //<Name>Minerva Kitchingman</Name>
    //<Money>2582</Money>
    //<Position>Invalid</Position>
    //<Weapon>ChainRifle</Weapon>
    //<DepartmentId>2</DepartmentId>
    //<Prisoners>
    //<Prisoner id = "15" />
    //</ Prisoners >

}
