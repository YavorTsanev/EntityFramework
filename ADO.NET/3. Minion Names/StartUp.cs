using System;
using System.Numerics;
using Microsoft.Data.SqlClient;

namespace _3._Minion_Names
{
    class StartUp
    {
        static void Main(string[] args)
        {
            int id = int.Parse(Console.ReadLine());

            var connectionString = "Server=.;Database=MinionsDB;Integrated Security=true";
            var queryNameVillain = "select Name from Villains as v where v.Id = @Id";
            var queryNamesAndAgeMinions = "select ROW_NUMBER() over(order by m.Name) as RowNumber, m.Name,m.Age from MinionsVillains as mv join Minions as m on m.Id = mv.MinionId where mv.VillainId = @Id";

            using var connection = new SqlConnection(connectionString);
            connection.Open();
            using var commandVillainName = new SqlCommand(queryNameVillain, connection);
            commandVillainName.Parameters.AddWithValue("@Id", id);
            var villainName = (string)commandVillainName.ExecuteScalar();

            if (villainName == null)
            {
                Console.WriteLine($"No villain with ID {id} exists in the database.");
                return;
            }

            Console.WriteLine($"Villain: {villainName}");

            using var minionsNameCommand = new SqlCommand(queryNamesAndAgeMinions, connection);
            minionsNameCommand.Parameters.AddWithValue("@Id", id);

            var result = minionsNameCommand.ExecuteReader();
            if (result.HasRows == false)
            {
                Console.WriteLine("(no minions)");
            }

            while (result.Read())
            {
                var rowNumber = (long)result["RowNumber"];
                string name = (string)result["Name"];
                int age = (int)result["Age"];

                Console.WriteLine($"{rowNumber}. {name} {age}");
            }
        }
    }
}
