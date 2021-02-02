using SoftUni.Data;
//Problem 2
using System;
using System.Linq;
using System.Text;

namespace SoftUni
{
    public class StartUp
    {
        static void Main()
        {
            var context = new SoftUniContext();
            //var result = EmployeesFullInformation(db);

            //var result = GetEmployeesWithSalaryOver50000(db);

            var result = GetEmployeesFromResearchAndDevelopment(context);

            Console.WriteLine(result);
        }
        //Problem 3
        private static string GetEmployeesWithSalaryOver50000(SoftUniContext context)
        {
            var sb = new StringBuilder();

            var employees = context.Employees.Where(e => e.Salary > 50000).Select(e => new { e.FirstName, e.Salary }).OrderBy(x => x.FirstName).ToList();

            foreach (var employee in employees)
            {
                sb.AppendLine($"{employee.FirstName} {employee.Salary:f2}");
            }

            return sb.ToString().TrimEnd();
        }
        //Problem 4
        public static string EmployeesFullInformation(SoftUniContext context)
        {
            var sb = new StringBuilder();

            var employees = context.Employees.Select(e => new { e.FirstName, e.LastName, e.MiddleName, e.JobTitle, e.Salary, e.EmployeeId }).OrderBy(x => x.EmployeeId).ToList();

            foreach (var employee in employees)
            {
                sb.AppendLine($"{employee.FirstName} {employee.LastName} {employee.MiddleName} {employee.JobTitle} {employee.Salary:f2}");
            }

            return sb.ToString().TrimEnd();
        }
        //Problem 5
        public static string GetEmployeesFromResearchAndDevelopment(SoftUniContext context)
        {
            var sb = new StringBuilder();

            var employees = context.Employees.Where(e => e.Department.Name == "Research and Development").Select(e => new { e.FirstName, e.LastName, Depart = e.Department.Name, e.Salary}).OrderBy(e => e.Salary).ThenByDescending(e => e.FirstName).ToList();

            foreach (var employee in employees)
            {
                sb.AppendLine($"{employee.FirstName} {employee.LastName} from {employee.Depart} - ${employee.Salary:f2}");
            }

            return sb.ToString().TrimEnd();
        }
    }
}
