using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Text;
using Cinema.Data.Models;
using Cinema.DataProcessor.ImportDto;
using Newtonsoft.Json;
using ProductShop;

namespace Cinema.DataProcessor
{
    using System;

    using Data;

    public class Deserializer
    {
        private const string ErrorMessage = "Invalid data!";

        private const string SuccessfulImportMovie
            = "Successfully imported {0} with genre {1} and rating {2}!";

        private const string SuccessfulImportProjection
            = "Successfully imported projection {0} on {1}!";

        private const string SuccessfulImportCustomerTicket
            = "Successfully imported customer {0} {1} with bought tickets: {2}!";

        public static string ImportMovies(CinemaContext context, string jsonString)
        {
            //•	If any validation errors occur(such as if Rating is not between 1 and 10, a Title/ Genre / Duration / Rating / Director is missing, or they exceed required the min and max length), do not import any part of the entity and append an error message to the method output.
            //    •	If a movie with this title already exists, do not import it and append an error message.
            //    •	Durations will always be in the format "c".Do not forget to use CultureInfo.InvariantCulture!
            //    •	When printing the output Ratings should be in format "f2".

            var movies = JsonConvert.DeserializeObject<List<MovieImportDto>>(jsonString);

            var sb = new StringBuilder();

            foreach (var movieDto in movies)
            {
                if (!IsValid(movieDto))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                var movie = context.Movies.FirstOrDefault(m => m.Title == movieDto.Title);

                if (movie != null)
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                var movieToImpot = new Movie
                {
                    Title = movieDto.Title,
                    Genre = movieDto.Genre,
                    Duration = movieDto.Duration,
                    Rating = movieDto.Rating,
                    Director = movieDto.Director
                };

                context.Movies.Add(movieToImpot);
                context.SaveChanges();
                sb.AppendLine(string.Format(SuccessfulImportMovie, movieToImpot.Title, movieToImpot.Genre.ToString(),
                    movieToImpot.Rating.ToString("f2")));
            }


            return sb.ToString().TrimEnd();
        }

        public static string ImportProjections(CinemaContext context, string xmlString)
        {
            //•	If there are any validation errors(such as invalid movie), do not import any part of the entity and append an error message to the method output.
            //    •	Dates will always be in the format: "yyyy-MM-dd HH:mm:ss".Do not forget to use CultureInfo.InvariantCulture!
            //    •	In the output Projection Datetime is in format "MM/dd/yyyy".

            var projections = XmlConverter.Deserializer<ProjectionImportDto>(xmlString, "Projections");

            var sb = new StringBuilder();

            foreach (var projectionDto in projections)
            {
               

                var movie = context.Movies.FirstOrDefault(m => m.Id == projectionDto.MovieId);
                if (movie == null)
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                var isValidDate = DateTime.TryParseExact(projectionDto.DateTime, "yyyy-MM-dd HH:mm:ss",
                    CultureInfo.InvariantCulture, DateTimeStyles.None, out var date);

                if (!isValidDate)
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                var projection = new Projection
                {
                    Movie = movie,
                    MovieId = projectionDto.MovieId,
                    DateTime =date
                };
                context.Projections.Add(projection);
                context.SaveChanges();
                sb.AppendLine(string.Format(SuccessfulImportProjection, projection.Movie.Title, projection.DateTime.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture)));
                
                
            }


            return sb.ToString().TrimEnd();
        }

        public static string ImportCustomerTickets(CinemaContext context, string xmlString)
        {
            //•	If there are any validation errors(such invalid names, age, balance, etc.), do not import any part of the entity and append an error message to the method output.
            //    •	If there are any validation errors in tickets(such invalid price), do not import the ticket entity itself and append an error message to the method output.

            var customers = XmlConverter.Deserializer<CustomerImportDto>(xmlString, "Customers");

            var sb = new StringBuilder();

            foreach (var customerDto in customers)
            {
                if (!IsValid(customerDto))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                var customer = new Customer
                {
                    FirstName = customerDto.FirstName,
                    LastName = customerDto.LastName,
                    Age = customerDto.Age,
                    Balance = customerDto.Balance
                };

                foreach (var ticketDto in customerDto.Tickets)
                {
                    if (!IsValid(ticketDto))
                    {
                        sb.AppendLine(ErrorMessage);
                        continue;
                    }

                    var ticket = new Ticket
                    {
                        ProjectionId = ticketDto.ProjectionId,
                        Price = ticketDto.Price
                    };

                    customer.Tickets.Add(ticket);
                }

                context.Customers.Add(customer);
                context.SaveChanges();
                sb.AppendLine(string.Format(SuccessfulImportCustomerTicket, customer.FirstName, customer.LastName,
                    customer.Tickets.Count));
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