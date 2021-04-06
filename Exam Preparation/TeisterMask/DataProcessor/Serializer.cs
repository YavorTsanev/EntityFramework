using System.Globalization;
using System.Linq;
using Microsoft.VisualBasic;
using Newtonsoft.Json;
using ProductShop;
using TeisterMask.DataProcessor.ExportDto;

namespace TeisterMask.DataProcessor
{
    using System;

    using Data;

    using Formatting = Newtonsoft.Json.Formatting;

    public class Serializer
    {
        public static string ExportProjectWithTheirTasks(TeisterMaskContext context)
        {
            var projects = context
                
                .Projects
                .ToList()
                .Where(p => p.Tasks.Count > 0)
                .ToList()
                .Select(p => new ProjectTaskExportDto
            {
                TasksCount = p.Tasks.Count,
                ProjectName = p.Name,
                HasEndDate = p.DueDate.HasValue ? "Yes" : "No",
                Tasks = p.Tasks.ToList().Select(t => new ProjectTaskExportDto.TaskExportDto
                {
                    Name = t.Name,
                    Label = t.LabelType.ToString()
                }).OrderBy(x => x.Name).ToList()
            }).OrderByDescending(x => x.TasksCount).ThenBy(x => x.ProjectName).ToList();

            return XmlConverter.Serialize(projects, "Projects");
        }

        public static string ExportMostBusiestEmployees(TeisterMaskContext context, DateTime date)
        {
            //Select the top 10 employees who have at least one task that its open date is after or equal to the given date with their tasks that meet the same requirement(to have their open date after or equal to the giver date).For each employee, export their username and their tasks.For each task, export its name and open date(must be in format "d"), due date(must be in format "d"), label and execution type. Order the tasks by due date(descending), then by name(ascending).Order the employees by all tasks(meeting above condition) count(descending), then by username(ascending).
            //    NOTE: Do not forget to use CultureInfo.InvariantCulture.You may need to call.ToArray() function before the selection in order to detach entities from the database and avoid runtime errors(EF Core bug).


            var employyes = context.Employees
                .ToList()
                .Where(e => e.EmployeesTasks.Any(t => t.Task.OpenDate >= date))
                .Select(e => new
                {
                    Username = e.Username,
                    Tasks = e.EmployeesTasks
                        .Where(t => t.Task.OpenDate >= date)
                        .OrderByDescending(x => x.Task.DueDate)
                        .ThenBy(x => x.Task.Name)
                        .Select(t => new
                    {
                        TaskName = t.Task.Name,
                        OpenDate = t.Task.OpenDate.ToString("d", CultureInfo.InvariantCulture),
                        DueDate = t.Task.DueDate.ToString("d", CultureInfo.InvariantCulture),
                        LabelType = t.Task.LabelType.ToString(),
                        ExecutionType = t.Task.ExecutionType.ToString()
                    }).ToList()
                }).ToList()
                .OrderByDescending(e => e.Tasks.Count)
                .ThenBy(x => x.Username)
                .Take(10)
                .ToList();
            return JsonConvert.SerializeObject(employyes, Formatting.Indented);
        }
    }
}