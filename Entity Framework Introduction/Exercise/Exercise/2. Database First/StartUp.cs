using SoftUni.Data;
using SoftUni.Models;
//Problem 2
using System;
using System.Globalization;
using System.Linq;
using System.Text;

namespace SoftUni
{
    public class StartUp
    {
        private static StringBuilder sb ;

        static  StartUp()
        {
            sb = new StringBuilder();
        }

        public static void Main()
        {
            var context = new SoftUniContext();

            //var result = GetEmployeesWithSalaryOver50000(context);

            //var result = EmployeesFullInformation(context);

            //var result = GetEmployeesWithSalaryOver50000(context);

            // var result = GetEmployeesFromResearchAndDevelopment(context);

            //var result = AddNewAddressToEmployee(context);

           //var result = GetEmployeesInPeriod(context);

            //var result = GetAddressesByTown(context);

            //var result = GetEmployee147(context);

            //var result = GetDepartmentsWithMoreThan5Employees(context);

            Console.WriteLine(result);
        }
        //Problem 3
        private static string GetEmployeesWithSalaryOver50000(SoftUniContext context)
        {

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

            var employees = context.Employees.Where(e => e.Department.Name == "Research and Development").Select(e => new { e.FirstName, e.LastName, Depart = e.Department.Name, e.Salary}).OrderBy(e => e.Salary).ThenByDescending(e => e.FirstName).ToList();

            foreach (var employee in employees)
            {
                sb.AppendLine($"{employee.FirstName} {employee.LastName} from {employee.Depart} - ${employee.Salary:f2}");
            }

            return sb.ToString().TrimEnd();
        }
        //Problem 6
        public static string AddNewAddressToEmployee(SoftUniContext context)
        {
            Address newAddress = new Address() { AddressText = "Vitoshka 15", TownId = 4 };

            context.Addresses.Add(newAddress);

            var employee = context.Employees.FirstOrDefault(e => e.LastName == "Nakov");

            employee.Address = newAddress;

            context.SaveChanges();

            var employees = context.Employees.OrderByDescending(e => e.AddressId).Take(10).Select(e => e.Address.AddressText ).ToList();

            foreach (var e in employees)
            {
                sb.AppendLine(e);
            }

            return sb.ToString().TrimEnd();
        }
        //Problem 7
        public static string GetEmployeesInPeriod(SoftUniContext context)
        {

            var employyes = context.Employees.Where(e => e.EmployeesProjects.Any(ep => ep.Project.StartDate.Year >= 2001 && ep.Project.StartDate.Year <= 2003)).Take(10).Select(e => new { EmployeeFirst = e.FirstName, EmployeeLast = e.LastName, ManagerFirst = e.Manager.FirstName, ManagerLast = e.Manager.LastName, Projects = e.EmployeesProjects.Select(ep => new {ProjectName = ep.Project.Name, StartDate = ep.Project.StartDate.ToString("M/d/yyyy h:mm:ss tt", CultureInfo.InvariantCulture), EndDate = ep.Project.EndDate != null ? ep.Project.EndDate.Value.ToString("M/d/yyyy h:mm:ss tt", CultureInfo.InvariantCulture) : "not finished" }) });


            foreach (var e in employyes)
            {
                sb.AppendLine($"{e.EmployeeFirst} {e.EmployeeLast} – Manager: {e.ManagerFirst} {e.ManagerLast}");

                foreach (var p in e.Projects)
                {
                    sb.AppendLine($"--{p.ProjectName} - {p.StartDate} - {p.EndDate}");
                }
            }

            return sb.ToString().TrimEnd();
        }
        //Problem 8
        public static string GetAddressesByTown(SoftUniContext context)
        {
            var addresses = context.Addresses.OrderByDescending(a => a.Employees.Count).ThenBy(a => a.Town.Name).ThenBy(a => a.AddressText).Take(10).Select(a => new { AddressText = a.AddressText, TownName =  a.Town.Name, EmplCount = a.Employees.Count}).ToList();

            foreach (var a in addresses)
            {
                sb.AppendLine($"{a.AddressText}, {a.TownName} - {a.EmplCount} employees");
            }

            return sb.ToString().TrimEnd();
        }
        //Problem 9
        public static string GetEmployee147(SoftUniContext context)
        {
            var employee = context.Employees.Where(e => e.EmployeeId == 147).Select(e => new { e.FirstName, e.LastName, e.JobTitle, Projects = e.EmployeesProjects.Select(ep => ep.Project.Name).OrderBy(p => p).ToList() }).FirstOrDefault();

            sb.AppendLine($"{employee.FirstName} {employee.LastName} - {employee.JobTitle}");

            foreach (var p in employee.Projects)
            {
                sb.AppendLine(p);
            }
            

            return sb.ToString().Trim();
        }
        //Problem 10
        public static string GetDepartmentsWithMoreThan5Employees(SoftUniContext context)
        {
            var departments = context.Departments.Where(d => d.Employees.Count > 5).OrderBy(d => d.Employees.Count).ThenBy(d => d.Name).Select(d => new { d.Name, ManagerFirstName = d.Manager.FirstName, ManagerLastName = d.Manager.LastName, Employees = d.Employees.Select(e => new { e.FirstName, e.LastName, e.JobTitle }).OrderBy(e => e.FirstName).ThenBy(e => e.LastName).ToList() }).ToList();

            foreach (var d in departments)
            {
                sb.AppendLine($"{d.Name} - {d.ManagerFirstName} {d.ManagerLastName}");

                foreach (var e in d.Employees)
                {
                    sb.AppendLine($"{e.FirstName} {e.LastName} - {e.JobTitle}");
                }
            }

            return sb.ToString().TrimEnd();
        }

    }
}
