using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace ProductShop.DTO.Import
{
    [XmlType("Product")]
    public class ImportProductDto
    {
        private string nameField;

        private decimal priceField;

        private byte sellerIdField;

        private int? buyerIdField;


        /// <remarks/>
        public string name
        {
            get
            {
                return this.nameField;
            }
            set
            {
                this.nameField = value;
            }
        }

        /// <remarks/>
        public decimal price
        {
            get
            {
                return this.priceField;
            }
            set
            {
                this.priceField = value;
            }
        }

        /// <remarks/>
        public byte sellerId
        {
            get
            {
                return this.sellerIdField;
            }
            set
            {
                this.sellerIdField = value;
            }
        }

        /// <remarks/>
        public int? buyerId
        {
            get
            {
                return this.buyerIdField;
            }
            set
            {
                this.buyerIdField = value;
            }
        }

    }
}


