using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace ProductShop.DTO.Import
{
    [XmlType("CategoryProduct")]
    public class ImportCategoryProductDto
    {
        private byte categoryIdField;

        private byte productIdField;

        /// <remarks/>
        public byte CategoryId
        {
            get
            {
                return this.categoryIdField;
            }
            set
            {
                this.categoryIdField = value;
            }
        }

        /// <remarks/>
        public byte ProductId
        {
            get
            {
                return this.productIdField;
            }
            set
            {
                this.productIdField = value;
            }
        }
    }
}