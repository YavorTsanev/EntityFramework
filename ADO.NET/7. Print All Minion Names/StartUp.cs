using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Text;

namespace _7._Print_All_Minion_Names
{
    public class StartUp
    {
        private const string ConectionString = @"Server=.;Database=MinionsDB;Integrated Security =true;";

        static void Main(string[] args)
        {
            var conection = OpenConnection();

            var result = TakeResult(conection);

            Console.WriteLine(result);
        }

        private static string TakeResult(SqlConnection conection)
        {
            var command = new SqlCommand(Queries.TakeVillainNames, conection);

            var reader = command.ExecuteReader();

            var sb = new StringBuilder();

            var list = new List<string>();

            while (reader.Read())
            {
                list.Add(reader[0].ToString());
            }

            for (int i = 0; i < list.Count / 2 ; i++)
            {
                sb.AppendLine(list[i]);
                sb.AppendLine(list[list.Count - 1 - i]);
            }

            if (list.Count%2 == 1)
            {
                sb.AppendLine(list[list.Count / 2]);
            }

            return sb.ToString().TrimEnd();
        }

        private static SqlConnection OpenConnection()
        {
            var connection = new SqlConnection(ConectionString);
            connection.Open();
            return connection;
        }
    }
}
