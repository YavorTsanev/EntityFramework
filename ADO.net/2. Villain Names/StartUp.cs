using System;
using Microsoft.Data.SqlClient;
namespace _2._Villain_Names
{
    class StartUp
    {
        static void Main(string[] args)
        {
            var connectionString = "Server=.;Database=MinionsDB;Integrated Security=true";
            var query = "select v.Name , count(m.Id) as Count from MinionsVillains as mv join Minions as m on m.Id = mv.MinionId join Villains as v on v.Id = mv.VillainId group by v.Name";

            using var dbConnect = new SqlConnection(connectionString);
            dbConnect.Open();

            var command = new SqlCommand(query, dbConnect);
            using var result = command.ExecuteReader();

            while (result.Read())
            {
                string name = (string)result["Name"];
                int count = (int)result["count"];

                Console.WriteLine($"{name} - {count}");
            }
        }
    }
}
