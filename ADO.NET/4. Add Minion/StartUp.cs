using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _4._Add_Minion
{
    public class StartUp
    {
        private const string ConectionString = @"Server=.;Database=MinionsDB;Integrated Security=true;";

        static void Main(string[] args)
        {

            var minionInfo = Console.ReadLine().Split(" ", StringSplitOptions.RemoveEmptyEntries).Skip(1).ToList();
            var villainInfo = Console.ReadLine().Split(" ", StringSplitOptions.RemoveEmptyEntries).Skip(1).ToList();

            using var conection = new SqlConnection(ConectionString);
            conection.Open();

            string result = AddMinionsToDatabase(conection, minionInfo,villainInfo);
            Console.WriteLine(result);
        }

        private static string AddMinionsToDatabase(SqlConnection conection, List<string> minionInfo, List<string> villainInfo)
        {
            var sb = new StringBuilder();

            var minionName = minionInfo[0];
            var minionAge = int.Parse(minionInfo[1]);
            var minionTown = minionInfo[2];

            var villainName = villainInfo[0];

            int? townId = GetTownId(conection,minionTown);
            if(townId == null)
            {
                sb.AppendLine(AddTown(conection,minionTown));
            }

            int? villainId = GetVillainId(conection, villainName);
            if (villainId == null)
            {
                sb.AppendLine(AddVillain(conection,villainName));
            }
            int? curtownId = GetTownId(conection,minionTown);
            AddMinion(conection, minionName,minionAge, curtownId);

            int? minionId = GetMinionId(minionName,minionAge, conection);
            int? curvillainId = GetVillainId(conection,villainName);

            sb.AppendLine(AddMinionToMVTable(conection,minionId, curvillainId, minionName, villainName));

            return sb.ToString().TrimEnd();

        }

        private static string AddMinionToMVTable(SqlConnection conection, int? minionId, int? villainId,string minionName, string villainName)
        {
            using var command = new SqlCommand(Queries.AddMinionToMVTable,conection);
            command.Parameters.AddWithValue("@MinionId", minionId);
            command.Parameters.AddWithValue("@VillainId", villainId);
            command.ExecuteNonQuery();

            return $"Successfully added {minionName} to be minion of {villainName}.";
        }

        private static int? GetMinionId(string minionName, int minionAge, SqlConnection conection)
        {
            using var command = new SqlCommand(Queries.GetMinionId,conection);
            command.Parameters.AddWithValue("@MinionName", minionName);
            command.Parameters.AddWithValue("@MinionAge", minionAge);

            return (int?)command.ExecuteScalar();
        }

        private static void AddMinion(SqlConnection conection, string minionName, int minionAge, int? townId)
        {
            using var command = new SqlCommand(Queries.AddMinion,conection);
            command.Parameters.AddWithValue("@MinionName",minionName);
            command.Parameters.AddWithValue("@MinionAge", minionAge);
            command.Parameters.AddWithValue("@TownId", townId);

            command.ExecuteScalar();
        }

        private static string AddVillain(SqlConnection conection, string villainName)
        {
            using var command = new SqlCommand(Queries.AddVillain, conection);
            command.Parameters.AddWithValue("@VillainName", villainName);
            command.ExecuteNonQuery();
            return $"Villain {villainName} was added to the database.";
        }

        private static int? GetVillainId(SqlConnection conection, string villainName)
        {
            using var command = new SqlCommand(Queries.GetVillainId, conection);
            command.Parameters.AddWithValue("@VillainName", villainName);

            return (int?)command.ExecuteScalar();
        }

        private static int? GetTownId(SqlConnection conection,string minionTown)
        {
           using var command = new SqlCommand(Queries.GetTownId, conection);
            command.Parameters.AddWithValue("@TownName", minionTown);

            return (int?)command.ExecuteScalar();
        }


        private static string AddTown(SqlConnection conection, string minionTown)
        {
            using var command = new SqlCommand(Queries.AddTown, conection);
            command.Parameters.AddWithValue("@TownName", minionTown);
            command.ExecuteNonQuery();

            return $"Town {minionTown} was added to the database.";
        }

    }
}
