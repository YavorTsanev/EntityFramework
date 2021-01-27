using Microsoft.Data.SqlClient;
using System;

namespace Demo
{
    class StartUp
    {
        static void Main(string[] args)
        {
            using var dbConect = new SqlConnection("Server=.;Database=SoftUni;Integrated Security=true");
            dbConect.Open();

            var sqlCommand = new SqlCommand("select employeeid,firstname, lastname, salary from employees where firstname like 'n%'", dbConect);
            Reader(sqlCommand);

            var newSqlCommnad = new SqlCommand("update employees set salary -= 22 where firstname like 'n%'", dbConect);
            int newResult = newSqlCommnad.ExecuteNonQuery();
            Console.WriteLine($"Salary updated for {newResult} employees");

            Reader(sqlCommand);
        }

        private static void Reader(SqlCommand sqlCommand)
        {
            using var result = sqlCommand.ExecuteReader();
            while (result.Read())
            {
                var firstName = (string)result[1];
                var lastName = (string)result["lastname"];
                var salary = (decimal)result["salary"];
                Console.WriteLine($"{firstName} {lastName} => {salary}");
            }
        }
    }
}
