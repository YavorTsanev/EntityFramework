using Microsoft.Data.SqlClient;
using System;
using System.Data;
using System.Text;

namespace _9._Increase_Age_Stored_Procedure
{
    public class StartUp
    {
        private const string ConnectionString = @"Server=.;Database=MinionsDB;Integrated Security=true;";

        static void Main(string[] args)
        {
            var connection = OpenConnection();

            var id = int.Parse(Console.ReadLine());

            var result = GetResult(connection,id);

            Console.WriteLine(result);
        }

        private static string GetResult(SqlConnection connection, int id)
        {
            var execSP = new SqlCommand("sp_IncreaseMinionAge", connection);
            execSP.CommandType = CommandType.StoredProcedure;
            execSP.Parameters.AddWithValue("@id",id);
            execSP.ExecuteNonQuery();

            var sb = new StringBuilder();

            var result = new SqlCommand(Queries.PrintResult, connection);
            result.Parameters.AddWithValue("@id", id);
            var reader = result.ExecuteReader();

            while (reader.Read())
            {
                sb.AppendLine($"{reader["name"]} – {reader["age"]} years old");
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
