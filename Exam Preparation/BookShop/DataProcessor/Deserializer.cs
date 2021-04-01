using System.Xml;
using AutoMapper;
using BookShop.Data.Models;
using BookShop.Data.Models.Enums;
using BookShop.DataProcessor.ImportDto;
using ProductShop;
using Book = BookShop.Data.Models.Book;

namespace BookShop.DataProcessor
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Xml.Serialization;
    using Data;
    using Newtonsoft.Json;
    using ValidationContext = System.ComponentModel.DataAnnotations.ValidationContext;

    public class Deserializer
    {
        private const string ErrorMessage = "Invalid data!";

        private const string SuccessfullyImportedBook
            = "Successfully imported book {0} for {1:F2}.";

        private const string SuccessfullyImportedAuthor
            = "Successfully imported author - {0} with {1} books.";

        public static string ImportBooks(BookShopContext context, string xmlString)
        {
            var books = XmlConverter.Deserializer<BookImportDto>(xmlString, "Books");
            var sb = new StringBuilder();

            foreach (var item in books)
            {
                if (!IsValid(item))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                var iSValidDate = DateTime.TryParseExact(item.PublishedOn, "MM/dd/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out var parseDate
                    );


                if (item.Genre < 1 || item.Genre > 3)
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }


                var book = new Book
                {
                    Name = item.Name,
                    Genre = (Genre) item.Genre,
                    Price = item.Price,
                    Pages = item.Pages,
                    PublishedOn = parseDate
                };
                context.Books.Add(book);

                sb.AppendLine(string.Format(SuccessfullyImportedBook, item.Name, item.Price));
            }

            context.SaveChanges();
            return sb.ToString().TrimEnd();
        }

        public static string ImportAuthors(BookShopContext context, string jsonString)
        {
            var authors = JsonConvert.DeserializeObject<List<AuthorImportDto>>(jsonString);
            var sb = new StringBuilder();

            foreach (var item in authors)
            {
                if (!IsValid(item))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                if (context.Authors.FirstOrDefault(a => a.Email == item.Email) != null)
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }


                var author = new Author
                {
                    FirstName = item.FirstName,
                    LastName = item.LastName,
                    Phone = item.Phone,
                    Email = item.Email,
                    
                };

                foreach (var itemBook in item.Books)
                {
                    var book = context.Books.FirstOrDefault(b => b.Id == itemBook.Id);

                    if (book == null)
                    {
                        continue;
                    }

                    author.AuthorsBooks.Add(new AuthorBook
                    {
                        Book = book
                    });
                    context.SaveChanges();


                }

                if (!author.AuthorsBooks.Any())
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                context.Authors.Add(author);
                context.SaveChanges();
                sb.AppendLine(string.Format(SuccessfullyImportedAuthor, author.FirstName + " " +author.LastName, author.AuthorsBooks.Count));
            }

            return sb.ToString().TrimEnd();
        }

        private static bool IsValid(object dto)
        {
            var validationContext = new ValidationContext(dto);
            var validationResult = new List<ValidationResult>();

            return Validator.TryValidateObject(dto, validationContext, validationResult, true);
        }
    }
}