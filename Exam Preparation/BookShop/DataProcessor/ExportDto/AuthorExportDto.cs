using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BookShop.DataProcessor.ExportDto
{
    public class AuthorExportDto
    {
        public string AuthorName { get; set; }
        public List<BookExportXmlDto> Books { get; set; }
    }

    public class BookExportXmlDto
    {
        public string BookName { get; set; }
        public string BookPrice { get; set; }
    }


}
