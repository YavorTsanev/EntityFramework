using System.Globalization;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using ProductShop;
using TeisterMask.Data.Models;
using TeisterMask.Data.Models.Enums;
using TeisterMask.DataProcessor.ImportDto;

namespace TeisterMask.DataProcessor
{
    using System;
    using System.Collections.Generic;

    using System.ComponentModel.DataAnnotations;

    using Data;

    using ValidationContext = System.ComponentModel.DataAnnotations.ValidationContext;

    public class Deserializer
    {
        private const string ErrorMessage = "Invalid data!";

        private const string SuccessfullyImportedProject
            = "Successfully imported project - {0} with {1} tasks.";

        private const string SuccessfullyImportedEmployee
            = "Successfully imported employee - {0} with {1} tasks.";

        public static string ImportProjects(TeisterMaskContext context, string xmlString)
        {
            var projectsDtos = XmlConverter.Deserializer<ProjectImportDto>(xmlString, "Projects");
            //•	If there are any validation errors for the project entity(such as invalid name or open date), do not import any part of the entity and append an error message to the method output. 
            //•	If there are any validation errors for the task entity(such as invalid name, open or due date are missing, task open date is before project open date or task due date is after project due date), do not import it(only the task itself, not the whole project) and append an error message to the method output.
            var sb = new StringBuilder();

            foreach (var projectDto in projectsDtos)
            {
                var isValidDueDate = DateTime.TryParseExact(projectDto.DueDate, "dd/MM/yyyy",
                    CultureInfo.InvariantCulture, DateTimeStyles.None, out var parseDueDate);

                var isValidOpenDate = DateTime.TryParseExact(projectDto.OpenDate, "dd/MM/yyyy",
                    CultureInfo.InvariantCulture, DateTimeStyles.None, out var parseOpenDate);
                

                if (!IsValid(projectDto) || !isValidOpenDate)
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                var project = new Project
                {
                    Name = projectDto.Name,
                    OpenDate = parseOpenDate,
                    DueDate = isValidDueDate ? parseDueDate : (DateTime?)null,


                };

                foreach (var taskDto in projectDto.Tasks)
                {
                    var isValidTaskOpenDate = DateTime.TryParseExact(taskDto.TaskOpenDate, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out var parsedTaskOpenDate);

                    var isValidTaskDueDate = DateTime.TryParseExact(taskDto.TaskDueDate, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out var parsedTaskDueDate);

                    if (!IsValid(taskDto) || !isValidTaskOpenDate || !isValidTaskDueDate)
                    {
                        sb.AppendLine(ErrorMessage);
                        continue;
                    }

                    if (parsedTaskOpenDate < project.OpenDate || parsedTaskDueDate > project.DueDate)
                    {
                        sb.AppendLine(ErrorMessage);
                        continue;
                    }

                    var task = new Task
                    {
                        Name = taskDto.TaskName,
                        OpenDate = parsedTaskOpenDate,
                        DueDate = parsedTaskDueDate,
                        ExecutionType = (ExecutionType)taskDto.ExecutionType,
                        LabelType = (LabelType)taskDto.LabelType

                    };

                    project.Tasks.Add(task);

                }

                context.Projects.Add(project);
                context.SaveChanges();
                sb.AppendLine(string.Format(SuccessfullyImportedProject, project.Name, project.Tasks.Count));
            }


            return sb.ToString().TrimEnd();
        }

        public static string ImportEmployees(TeisterMaskContext context, string jsonString)
        {

            var emplooyesDtos = JsonConvert.DeserializeObject<List<EmployeesImportDto>>(jsonString);

            //•	If any validation errors occur(such as invalid username, email or phone), do not import any part of the entity and append an error message to the method output.
            //•	Take only the unique tasks.
            //•	If a task does not exist in the database, append an error message to the method output and continue with the next task.
            var sb = new StringBuilder();

            foreach (var empDto in emplooyesDtos)
            {
                if (!IsValid(empDto))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                var emplooyee = new Employee
                {
                    Username = empDto.Username,
                    Email = empDto.Email,
                    Phone = empDto.Phone
                };

                foreach (var taskDto in empDto.Tasks.Distinct())
                {
                    var task = context.Tasks.FirstOrDefault(x => x.Id == taskDto);

                    if (task == null)
                    {
                        sb.AppendLine(ErrorMessage);
                        continue;
                    }

                    emplooyee.EmployeesTasks.Add(new EmployeeTask
                    {
                        Task = task
                    });

                    context.SaveChanges();
                }

                context.Employees.Add(emplooyee);
                context.SaveChanges();
                sb.AppendLine(string.Format(SuccessfullyImportedEmployee, emplooyee.Username,
                    emplooyee.EmployeesTasks.Count));

            }

            return sb.ToString().TrimEnd();
        }

        private static bool IsValid(object dto)
        {
            var validationContext = new ValidationContext(dto);
            var validationResult = new List<ValidationResult>();

            return Validator.TryValidateObject(dto, validationContext, validationResult, true);
        }
    }
}