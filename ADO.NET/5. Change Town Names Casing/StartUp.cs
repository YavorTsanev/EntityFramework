using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Text;

namespace _5._Change_Town_Names_Casing
{
    public class StartUp
    {
        private const string connectionString = @"Server=.;Database=MinionsDB;Integrated Security=true;";

        static void Main(string[] args)
        {

            var countryName = Console.ReadLine();
            var connection = OpenConnection();

            string result = GetResult(connection, countryName);

            Console.WriteLine(result);
        }


        private static string GetResult(SqlConnection connection, string countryName)
        {
            var sb = new StringBuilder();

            using var command = new SqlCommand(Queries.GetCountryId, connection);
            command.Parameters.AddWithValue("@CountryName", countryName);
            var countryId = command.ExecuteScalar()?.ToString();

            if (countryId == null)
            {
                return sb.AppendLine($"No town names were affected.").ToString();
            }

            var towns = GetTowns(connection, countryId);


            List<string> cities = new List<string>();
            int count = 0;
            while (towns.Read())
            {
                string town = towns[0].ToString().ToUpper();
                cities.Add(town);
                count++;
                
            }

            sb.AppendLine($"{count} town names were affected.").Append($"[{string.Join(", ", cities)}]");
            return sb.ToString().TrimEnd();
        }

        private static SqlDataReader GetTowns(SqlConnection connection, string countryId)
        {
            using var command = new SqlCommand(Queries.GetTowns, connection);
            command.Parameters.AddWithValue("@CountryId", int.Parse(countryId));

            return command.ExecuteReader();
        }

        private static SqlConnection OpenConnection()
        {
            var connection = new SqlConnection(connectionString);
            connection.Open();
            return connection;
        }

    }
}
