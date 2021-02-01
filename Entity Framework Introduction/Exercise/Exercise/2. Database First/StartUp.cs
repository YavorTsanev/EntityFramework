using SoftUni.Data;
using System;
using System.Linq;

namespace SoftUni
{
    public class StartUp
    {
        static void Main()
        {
            var db = new SoftUniContext();
           // EmployeesFullInformation(db);
        }

        private static void EmployeesFullInformation(SoftUniContext db)
        {
            var employees = db.Employees.Select(e => new { e.FirstName, e.LastName, e.MiddleName, e.JobTitle, e.Salary, e.EmployeeId }).OrderBy(x => x.EmployeeId);

            foreach (var employee in employees)
            {
                Console.WriteLine($"{employee.FirstName} {employee.LastName} {employee.MiddleName} {employee.JobTitle} {employee.Salary:f2}");
            }
        }
    }
}
