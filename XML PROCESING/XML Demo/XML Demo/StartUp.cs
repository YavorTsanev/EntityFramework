using System;
using System.Xml;
using System.Xml.Linq;
using System.Linq;
using System.Xml.Serialization;
using System.IO;
using XML_Demo.DTO;

namespace XML_Demo
{
    class StartUp
    {
        static void Main(string[] args)
        {
            var xmlSerializer = new XmlSerializer(typeof(Car[]),new XmlRootAttribute("cars"));

            var cars = (Car[])xmlSerializer.Deserialize(File.OpenRead("Datasets/cars.xml"));

            xmlSerializer.Serialize(File.OpenWrite("../../../Datasets/carsXML.xml"),cars);
        }
    }


}
