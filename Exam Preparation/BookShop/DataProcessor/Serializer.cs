using BookShop.Data.Models;
using BookShop.DataProcessor.ExportDto;
using Newtonsoft.Json.Converters;
using ProductShop;

namespace BookShop.DataProcessor
{
    using System;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Xml;
    using System.Xml.Serialization;
    using Data;
    using Newtonsoft.Json;
    using Formatting = Newtonsoft.Json.Formatting;

    public class Serializer
    {
        public static string ExportMostCraziestAuthors(BookShopContext context)
        {
            var authors = context.Authors.Select(a => new AuthorExportDto
            {
                AuthorName = a.FirstName + " " + a.LastName,
                Books = a.AuthorsBooks.OrderByDescending(x => x.Book.Price).Select(b => new BookExportXmlDto
                {
                    BookName = b.Book.Name,
                    BookPrice = b.Book.Price.ToString("f2")
                }).ToList()
            }).ToList().OrderByDescending(x => x.Books.Count).ThenBy(x => x.AuthorName);

            return JsonConvert.SerializeObject(authors,Formatting.Indented);
        }

        public static string ExportOldestBooks(BookShopContext context, DateTime date)
        {
            var books = context.Books.Where(b => b.PublishedOn < date && b.Genre.ToString().ToLower() == "science").OrderByDescending(x => x.Pages).ThenByDescending(x => x.PublishedOn)
                .Take(10).Select(b => new BookExportDto
                {
                    Pages = b.Pages,
                    Name = b.Name,
                    Date = b.PublishedOn.ToString("d",CultureInfo.InvariantCulture)
                }).ToList();

            return XmlConverter.Serialize(books, "Books");
        }
    }
}