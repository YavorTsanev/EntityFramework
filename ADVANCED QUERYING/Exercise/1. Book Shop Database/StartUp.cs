using BookShop.Data;
using BookShop.Initializer;
using BookShop.Models.Enums;
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

            //var command = int.Parse(Console.ReadLine());
            var input = Console.ReadLine();
            //var result = GetBooksByAgeRestriction(db, command);
            //var result = GetGoldenBooks(db);
            //var result = GetBooksByPrice(db);
            // var result = GetBooksNotReleasedIn(db, command);
            var result = GetBooksByCategory(db, input);
            Console.WriteLine(result);
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
    }
}
