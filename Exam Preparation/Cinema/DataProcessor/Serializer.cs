using System.Linq;
using Cinema.DataProcessor.ExportDto;
using Newtonsoft.Json;
using ProductShop;

namespace Cinema.DataProcessor
{
    using System;

    using Data;

    public class Serializer
    {
        public static string ExportTopMovies(CinemaContext context, int rating)
        {
            //Export top 10 movies which have rating more or equal to the given and have at least one projection with sold tickets.For each movie, export its name, rating formatted to the second digit, total incomes formatted same way and customers. For each customer, export its first name, last name and balance formatted to the second digit. Order the customers by balance(descending by the formatted string, not the balance itselft), then by first name(ascending) and last name(ascending).Take first 10 records ordered by rating(descending), then by total incomes(descending).

            var movies = context
                .Movies
                .ToList()
                .Where(m => m.Rating >= rating &&
                            m.Projections.Any(p => p.Tickets.Count >= 1))
                .OrderByDescending(m => m.Rating)
                .ThenByDescending(m => m.Projections.Sum(p => p.Tickets.Sum(t => t.Price)))
                .Select(m => new
                {
                    MovieName = m.Title,
                    Rating = m.Rating.ToString("f2"),
                    TotalIncomes = m.Projections
                        .Sum(p => p.Tickets.Sum(t => t.Price))
                        .ToString("f2"),
                    Customers = m.Projections
                        .SelectMany(p => p.Tickets)
                        .Select(t => new
                        {
                            FirstName = t.Customer.FirstName,
                            LastName = t.Customer.LastName,
                            Balance = t.Customer.Balance.ToString("f2")
                        })
                        .ToList()
                        .OrderByDescending(c => c.Balance)
                        .ThenBy(c => c.FirstName)
                        .ThenBy(c => c.LastName)
                        .ToList()
                })
                .Take(10)
                .ToList();

            return JsonConvert.SerializeObject(movies, Formatting.Indented);
        }

        public static string ExportTopCustomers(CinemaContext context, int age)
        {
            //Export customers with age above or equal to the given. For each customer, export their first name, last name, spent money for tickets(formatted to the second digit) and spent time(in format: "hh\:mm\:ss").Take first 10 records and order the result by spent money in descending order.

            var customers = context
                .Customers
                .ToList()
                .Where(c => c.Age >= age)
                .OrderByDescending(c => c.Tickets.Sum(t => t.Price))
                .Select(c => new CustomerExportDto()
                {
                    FirstName = c.FirstName,
                    LastName = c.LastName,
                    SpentMoney = c.Tickets.Sum(t => t.Price).ToString("f2"),
                    SpentTime = TimeSpan.FromMilliseconds(c.Tickets.Sum(t => t.Projection.Movie.Duration.TotalMilliseconds))
                        .ToString(@"hh\:mm\:ss")
                })
                .Take(10)
                .ToList();

            return XmlConverter.Serialize(customers, "Customers");
        }
    }
}