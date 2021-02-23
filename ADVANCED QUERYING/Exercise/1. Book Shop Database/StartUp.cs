using BookShop.Data;
using BookShop.Initializer;
using BookShop.Models.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Text;

namespace BookShop
{
    public class StartUp
    {

        public static void Main(string[] args)
        {
            using var db = new BookShopContext();
            //DbInitializer.ResetDatabase(db);

            //var input = int.Parse(Console.ReadLine());

            //var input = Console.ReadLine();

            //var result = GetBooksByAgeRestriction(db, command);

            //var result = GetGoldenBooks(db);

            //var result = GetBooksByPrice(db);

            // var result = GetBooksNotReleasedIn(db, command);

            //var result = GetBooksByCategory(db, input);

            //var result = GetBooksReleasedBefore(db,input);

            //var result = GetAuthorNamesEndingIn(db,input);

            //var result = GetBookTitlesContaining(db, input);

            //var result = GetBooksByAuthor(db, input);

            //var result = CountBooks(db, input);

            //var result = CountCopiesByAuthor(db);

            //var result = GetTotalProfitByCategory(db);

            //var result = GetMostRecentBooks(db);

            //var result = RemoveBooks(db);

            //Console.WriteLine(result);

            //IncreasePrices(db);
        }

        public static string GetBooksByAgeRestriction(BookShopContext context, string command)
        {
            StringBuilder sb = new StringBuilder();
            var age = Enum.Parse<AgeRestriction>(command, true);
            var books = context
                .Books
                .Where(b => b.AgeRestriction == age)
                .Select(b => b.Title)
                .OrderBy(t => t)
                .ToList();

            foreach (var book in books)
            {
                sb.AppendLine(book);
            }

            return sb.ToString().TrimEnd();
        }

        public static string GetGoldenBooks(BookShopContext context)
        {
            StringBuilder sb = new StringBuilder();


            var books = context.Books.AsEnumerable().Where(b => b.EditionType.ToString() == "Gold" && b.Copies < 5000).OrderBy(b => b.BookId).Select(b => b.Title).ToList();

            foreach (var item in books)
            {
                sb.AppendLine(item);
            }

            return sb.ToString().TrimEnd();
        }

        public static string GetBooksByPrice(BookShopContext context)
        {
            StringBuilder sb = new StringBuilder();

            var books = context.Books.Where(b => b.Price > 40).Select(b => new { b.Title, b.Price }).OrderByDescending(b => b.Price).ToList();

            foreach (var item in books)
            {
                sb.AppendLine($"{item.Title} - ${item.Price:f2}");
            }

            return sb.ToString().TrimEnd();
        }

        public static string GetBooksNotReleasedIn(BookShopContext context, int year)
        {
            StringBuilder sb = new StringBuilder();

            var books = context.Books.Where(b => b.ReleaseDate.Value.Year != year).OrderBy(b => b.BookId).Select(b => b.Title).ToList();

            foreach (var item in books)
            {
                sb.AppendLine(item);
            }

            return sb.ToString().TrimEnd();
        }

        public static string GetBooksByCategory(BookShopContext context, string input)
        {
            StringBuilder sb = new StringBuilder();

            var categories = input.Split(" ", StringSplitOptions.RemoveEmptyEntries).Select(c => c.ToUpper()).ToList();

            var books = context.Books.Where(b => b.BookCategories.Select(bc => bc.Category.Name.ToLower()).Intersect(categories).Any())
                .Select(b => b.Title)
                .OrderBy(b => b)
                .ToArray();

            foreach (var item in books)
            {
                sb.AppendLine(item);
            }

            return sb.ToString().TrimEnd();
        }

        public static string GetBooksReleasedBefore(BookShopContext context, string date)
        {
            StringBuilder sb = new StringBuilder();

            var books = context.Books.Where(b => b.ReleaseDate < DateTime.ParseExact(date, "dd-MM-yyyy", System.Globalization.CultureInfo.InvariantCulture)).OrderByDescending(b => b.ReleaseDate).Select(b => new { b.Title, EditionType = b.EditionType.ToString(), b.Price }).ToList();

            foreach (var book in books)
            {
                sb.AppendLine($"{book.Title} - {book.EditionType} - ${book.Price:f2}");
            }

            return sb.ToString().TrimEnd();
        }

        public static string GetAuthorNamesEndingIn(BookShopContext context, string input)
        {
            StringBuilder sb = new StringBuilder();

            var authors = context.Authors.Where(a => a.FirstName.EndsWith(input)).Select(a => new { FullName = a.FirstName + " " + a.LastName }).OrderBy(x => x.FullName).ToList();

            foreach (var author in authors)
            {
                sb.AppendLine(author.FullName);
            }

            return sb.ToString().TrimEnd();
        }

        public static string GetBookTitlesContaining(BookShopContext context, string input)
        {
            StringBuilder sb = new StringBuilder();

            var books = context.Books.Where(b => b.Title.ToUpper().Contains(input.ToUpper())).Select(b => b.Title).OrderBy(b => b).ToList();

            foreach (var book in books)
            {
                sb.AppendLine(book);
            }

            return sb.ToString().TrimEnd();
        }

        public static string GetBooksByAuthor(BookShopContext context, string input)
        {
            StringBuilder sb = new StringBuilder();

            var books = context.Books.Where(b => b.Author.LastName.ToUpper().StartsWith(input.ToUpper())).OrderBy(b => b.BookId).Select(b => new { b.Title, AuthorFullNAme = b.Author.FirstName + " " + b.Author.LastName }).ToList();

            foreach (var book in books)
            {
                sb.AppendLine($"{book.Title} ({book.AuthorFullNAme})");
            }

            return sb.ToString().TrimEnd();
        }

        public static int CountBooks(BookShopContext context, int lengthCheck)
        {
            return context.Books.Where(b => b.Title.Length > lengthCheck).Count();
        }


        public static string CountCopiesByAuthor(BookShopContext context)
        {
            StringBuilder sb = new StringBuilder();

            var authors = context.Authors.Select(a => new { FullName = a.FirstName + " " + a.LastName, CountOfCopies = a.Books.Sum(b => b.Copies) }).OrderByDescending(a => a.CountOfCopies).ToList();

            foreach (var author in authors)
            {
                sb.AppendLine($"{author.FullName} - {author.CountOfCopies}");
            }

            return sb.ToString().TrimEnd();
        }

        public static string GetTotalProfitByCategory(BookShopContext context)
        {
            StringBuilder sb = new StringBuilder();

            var categories = context.Categories.Select(c => new { c.Name, TotalProfit = c.CategoryBooks.Sum(bc => bc.Book.Price * bc.Book.Copies) }).OrderByDescending(c => c.TotalProfit).ToList();

            foreach (var c in categories)
            {
                sb.AppendLine($"{c.Name} ${c.TotalProfit:f2}");
            }


            return sb.ToString().TrimEnd();

        }

        public static string GetMostRecentBooks(BookShopContext context)
        {
            StringBuilder sb = new StringBuilder();

            var categories = context.Categories.Select(c => new { c.Name, Books = c.CategoryBooks.Select(cb => new { BookTitle = cb.Book.Title, BookDate = cb.Book.ReleaseDate }).OrderByDescending(b => b.BookDate).Take(3) }).OrderBy(c => c.Name).ToList();

            foreach (var c in categories)
            {
                sb.AppendLine($"--{c.Name}");

                foreach (var b in c.Books)
                {
                    sb.AppendLine($"{b.BookTitle} ({b.BookDate.Value.Year})");
                }
            }

            return sb.ToString().Trim();
        }

        public static void IncreasePrices(BookShopContext context)
        {
            var books = context.Books.Where(b => b.ReleaseDate.Value.Year < 2010).ToList();

            foreach (var b in books)
            {
                b.Price += 5m;
                
            }
              context.SaveChanges();
        }

        public static int RemoveBooks(BookShopContext context) 
        {
            var books = context.Books.Where(b => b.Copies < 4200).ToList();

            context.Books.RemoveRange(books);

            context.SaveChanges();

            return books.Count();
        }
    }
}
