//Problem 2
using SoftUni.Data;
using SoftUni.Models;

using System;
using System.Text;
using System.Linq;
using System.Globalization;
using Microsoft.EntityFrameworkCore;

namespace SoftUni
{
    public class StartUp
    {
        private static StringBuilder sb;

        static StartUp()
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

            //var result = GetLatestProjects(context);

            //var result = IncreaseSalaries(context);

            //var result = GetEmployeesByFirstNameStartingWithSa(context);

            //var result = DeleteProjectById(context);

            var result = RemoveTown(context);

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

            var employees = context.Employees.Where(e => e.Department.Name == "Research and Development").Select(e => new { e.FirstName, e.LastName, Depart = e.Department.Name, e.Salary }).OrderBy(e => e.Salary).ThenByDescending(e => e.FirstName).ToList();

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

            var employees = context.Employees.OrderByDescending(e => e.AddressId).Take(10).Select(e => e.Address.AddressText).ToList();

            foreach (var e in employees)
            {
                sb.AppendLine(e);
            }

            return sb.ToString().TrimEnd();
        }
        //Problem 7
        public static string GetEmployeesInPeriod(SoftUniContext context)
        {

            var employyes = context.Employees.Where(e => e.EmployeesProjects.Any(ep => ep.Project.StartDate.Year >= 2001 && ep.Project.StartDate.Year <= 2003)).Take(10).Select(e => new { EmployeeFirst = e.FirstName, EmployeeLast = e.LastName, ManagerFirst = e.Manager.FirstName, ManagerLast = e.Manager.LastName, Projects = e.EmployeesProjects.Select(ep => new { ProjectName = ep.Project.Name, StartDate = ep.Project.StartDate.ToString("M/d/yyyy h:mm:ss tt", CultureInfo.InvariantCulture), EndDate = ep.Project.EndDate != null ? ep.Project.EndDate.Value.ToString("M/d/yyyy h:mm:ss tt", CultureInfo.InvariantCulture) : "not finished" }) });


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
            var addresses = context.Addresses.OrderByDescending(a => a.Employees.Count).ThenBy(a => a.Town.Name).ThenBy(a => a.AddressText).Take(10).Select(a => new { AddressText = a.AddressText, TownName = a.Town.Name, EmplCount = a.Employees.Count }).ToList();

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
        //Problem 11
        public static string GetLatestProjects(SoftUniContext context)
        {
            var projects = context.Projects.OrderByDescending(p => p.StartDate).OrderBy(p => p.Name).Select(p => new { p.Name, p.Description, StartDate = p.StartDate.ToString("M/d/yyyy h:mm:ss tt", CultureInfo.InvariantCulture) }).ToList().Take(10);

            foreach (var p in projects)
            {
                sb.AppendLine(p.Name).AppendLine(p.Description).AppendLine(p.StartDate);
            }

            return sb.ToString().TrimEnd();
        }
        //Problem 12
        public static string IncreaseSalaries(SoftUniContext context)
        {
            var departmets = new string[] { "Engineering", "Tool Design", "Marketing", "Information Services" };

            GetEmployees(context, departmets).ToList().ForEach(e => e.Salary += e.Salary * 0.12m);

            context.SaveChanges();

            GetEmployees(context, departmets).Select(e => new { e.FirstName, e.LastName, e.Salary }).OrderBy(e => e.FirstName).ThenBy(e => e.LastName).ToList().ForEach(e => sb.AppendLine($"{e.FirstName} {e.LastName} (${e.Salary:f2})"));

            return sb.ToString().TrimEnd();

            IQueryable<Employee> GetEmployees(SoftUniContext context, string[] departmets)
            {
                return context.Employees.Where(e => departmets.Contains(e.Department.Name));
            }

        }
        //Problem 13
        public static string GetEmployeesByFirstNameStartingWithSa(SoftUniContext context)
        {
            context.Employees.Where(e => EF.Functions.Like(e.FirstName,"sa%")).Select(e => new {e.FirstName, e.LastName, e.JobTitle, e.Salary }).OrderBy(e => e.FirstName).ThenBy(e => e.LastName).ToList().ForEach(e => sb.AppendLine($"{e.FirstName} {e.LastName} - {e.JobTitle} - (${e.Salary:f2})"));

            return sb.ToString().TrimEnd();
        }
        //Problem 14
        public static string DeleteProjectById(SoftUniContext context)
        {
            //var id = int.Parse(Console.ReadLine());

            context.EmployeesProjects.RemoveRange(context.EmployeesProjects.Where(p => p.ProjectId == 2).ToList());

            context.Projects.Remove(context.Projects.FirstOrDefault(p => p.ProjectId == 2));

            context.SaveChanges();

            context.Projects.Take(10).Select(p => p.Name).ToList().ForEach(p => sb.AppendLine(p));

            return sb.ToString().TrimEnd();

        }
        //Problem 15
        public static string RemoveTown(SoftUniContext context)
        {
            context.Employees.Where(e => e.Address.Town.Name == "Seattle").ToList().ForEach(e => e.AddressId = null);

            var addressesToRemove = context.Addresses.Where(a => a.Town.Name == "Seattle").ToList();

            context.Addresses.RemoveRange(addressesToRemove);

            context.Towns.Remove(context.Towns.FirstOrDefault(t => t.Name == "Seattle"));

            context.SaveChanges();

            sb.Append($"{addressesToRemove.Count} addresses in Seattle were deleted");

            return sb.ToString().TrimEnd();
        }
    }
}
