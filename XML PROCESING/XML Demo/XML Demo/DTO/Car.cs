using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace XML_Demo.DTO
{
    [XmlType("car")]
    public partial class Car
    {

        private string makeField;

        private string modelField;

        private ulong travelleddistanceField;

        public string make
        {
            get
            {
                return this.makeField;
            }
            set
            {
                this.makeField = value;
            }
        }

        public string model
        {
            get
            {
                return this.modelField;
            }
            set
            {
                this.modelField = value;
            }
        }

        [System.Xml.Serialization.XmlElementAttribute("travelled-distance")]
        public ulong travelleddistance
        {
            get
            {
                return this.travelleddistanceField;
            }
            set
            {
                this.travelleddistanceField = value;
            }
        }
    }
}
