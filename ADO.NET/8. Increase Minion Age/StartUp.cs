using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _8._Increase_Minion_Age
{
    public class StartUp
    {
        private const string ConnectionString = @"Server=.;Database=MinionsDB;Integrated Security=true;";

        static void Main(string[] args)
        {
            var connection = OpenConnection();

            var ids = Console.ReadLine().Split(" ").Select(int.Parse).ToList();

            var result = TakeResult(connection,ids);

            Console.WriteLine(result);
        }

        private static string TakeResult(SqlConnection connection, List<int> ids)
        {
            var sb = new StringBuilder();

            var toLower = new SqlCommand(Queries.UpdateToLower,connection);
            toLower.ExecuteNonQuery();

            foreach (var item in ids)
            {
                var update = new SqlCommand(Queries.UpdateMinions, connection);
                update.Parameters.AddWithValue("@Id", item);
                update.ExecuteNonQuery();
            }

            using var select = new SqlCommand(Queries.PrintResult, connection);

            var reader = select.ExecuteReader();

            while (reader.Read())
            {
                sb.AppendLine($"{reader["Name"]} {reader["Age"]}");
            }


            return sb.ToString().TrimEnd();

        }
        
        private static SqlConnection OpenConnection()
        {
            var connection = new SqlConnection(ConnectionString);
            connection.Open();

            return connection;
        }
    }
}
