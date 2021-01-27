using Microsoft.Data.SqlClient;
using System;
using System.Text;

namespace _6._Remove_Villain
{
    class SratUp
    {
        private const string ConnectionString = @"Server=.;Database=MinionsDB;Integrated Security=true";

        static void Main(string[] args)
        {
            var villainId = int.Parse(Console.ReadLine());

            var connection = OpenConnection();

            var result = GetResult(connection,villainId);

            Console.WriteLine(result);

        }

        private static object GetResult(SqlConnection connection, int villainId)
        {
            var sb = new StringBuilder();

            string villainName = GetVillainName(connection,villainId);

            if (villainName == null)
            {
                return sb.AppendLine("No such villain was found.").ToString().TrimEnd();
            }

            int count = GetCoutOfDeleteMinions(connection, villainId);

            var res =  DeleteVillain(connection,villainId);

            if (res > 0)
            {
                sb.AppendLine($"{villainName} was deleted.")
                .AppendLine($"{count} minions were released.");
            }
            
            return sb.ToString().TrimEnd();
        }

        private static int GetCoutOfDeleteMinions(SqlConnection connection, int villainId)
        {
            var command = new SqlCommand(Queries.GetCountOfDeletedMinions,connection);
            command.Parameters.AddWithValue("@Id", villainId);

            return (int)command.ExecuteScalar();
        }

        private static int DeleteVillain(SqlConnection connection, int villainId)
        {
            var command = new SqlCommand(Queries.Transaction,connection);

            command.Parameters.AddWithValue("@Id", villainId);

            return command.ExecuteNonQuery();
        }

        private static string GetVillainName(SqlConnection connection, int villainId)
        {
            var command = new SqlCommand(Queries.GetVillanName,connection);

            command.Parameters.AddWithValue("@Id",villainId);

            return command.ExecuteScalar()?.ToString();
        }

        private static SqlConnection OpenConnection()
        {
            var connection = new SqlConnection(ConnectionString);
            connection.Open();
            return connection;
        }
    }
}
