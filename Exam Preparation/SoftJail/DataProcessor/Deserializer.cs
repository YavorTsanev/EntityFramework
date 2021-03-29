using System.Globalization;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using ProductShop;
using SoftJail.Data.Models;
using SoftJail.Data.Models.Enums;
using SoftJail.DataProcessor.ImportDto;

namespace SoftJail.DataProcessor
{

    using Data;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Deserializer
    {
        public static string ImportDepartmentsCells(SoftJailDbContext context, string jsonString)
        {
            //•	If any validation errors occur(such as if a department name is too long/ short or a cell number is out of range) proceed as described above
            //    •	If a department is invalid, do not import its cells.
            //    •	If a Department doesn’t have any Cells, he is invalid.
            //    •	If one Cell has invalid CellNumber, don’t import the Department.

            var sb = new StringBuilder();
            var departmentsCellsDtos = JsonConvert.DeserializeObject<List<DepartmentCellImportDto>>(jsonString);

            foreach (var item in departmentsCellsDtos)
            {
                if (!IsValid(item) || item.Cells.Count == 0 || !item.Cells.All(IsValid))
                {
                    sb.AppendLine("Invalid Data");
                    continue;
                }

                var department = new Department
                {
                    Name = item.Name,
                    Cells = item.Cells.Select(c => new Cell
                    {
                        CellNumber = c.CellNumber,
                        HasWindow = c.HasWindow
                    }).ToList()

                };

                context.Departments.Add(department);
                sb.AppendLine($"Imported {item.Name} with {item.Cells.Count} cells");
            }
            context.SaveChanges();
            return sb.ToString().TrimEnd();
        }

        public static string ImportPrisonersMails(SoftJailDbContext context, string jsonString)
        {
            var prisonersMailsDtos = JsonConvert.DeserializeObject<ICollection<PrisonerMailImportDto>>(jsonString);

            //•	The release and incarceration dates will be in the format “dd / MM / yyyy”. Make sure you use CultureInfo.InvariantCulture.
            //•	If any validation errors occur(such as invalid prisoner name or invalid nickname), ignore the entity and print an error message.
            //•	If a mail has incorrect address print error message and do not import the prisoner and his mails
            var sb2 = new StringBuilder();

            foreach (var item in prisonersMailsDtos)
            {
                if (!IsValid(item) || !item.Mails.All(IsValid))
                {
                    sb2.AppendLine("Invalid Data");
                    continue;
                }

                var incarcerationDateFormat = DateTime.ParseExact(item.IncarcerationDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);

                  var yoo  = DateTime.TryParseExact(item.IncarcerationDate, "dd/MM/yyyy", CultureInfo.InvariantCulture,DateTimeStyles.None, out DateTime releaseDateDateFormat);

                var prisoner = new Prisoner
                {
                    FullName = item.FullName,
                    Nickname = item.Nickname,
                    Age = item.Age,
                    IncarcerationDate = incarcerationDateFormat,
                    ReleaseDate = yoo ? releaseDateDateFormat : (DateTime?) null,
                    Bail = item.Bail,
                    CellId = item.CellId,
                    Mails = item.Mails.Select(x => new Mail
                    {
                        Description = x.Description,
                        Sender = x.Sender,
                        Address = x.Address
                    }).ToList()
                };
                sb2.AppendLine($"Imported {prisoner.FullName} {prisoner.Age} years old");

                context.Prisoners.Add(prisoner);
            }

            context.SaveChanges();
            return sb2.ToString().TrimEnd();
        }

        public static string ImportOfficersPrisoners(SoftJailDbContext context, string xmlString)
        {
            var officersPrisonerDtos = XmlConverter.Deserializer<OfficerPrisonerImportDto>(xmlString, "Officers");

                //•	If there are any validation errors(such as negative salary or invalid position/ weapon), proceed as described above.
                //•	The prisoner Id will always be valid

                var sb = new StringBuilder();

                foreach (var item in officersPrisonerDtos)
                {
                    if (!IsValid(item))
                    {
                        sb.AppendLine("Invalid Data");
                        continue;
                    }

                    var officer = new Officer
                    {
                        FullName = item.FullName,
                        Salary = item.Salary,
                        Position = (Position)Enum.Parse(typeof(Position),item.Position),
                        Weapon = Enum.Parse<Weapon>(item.Weapon),
                        DepartmentId = item.DepartmentId,
                        OfficerPrisoners = item.Prisoners.Select(p => new OfficerPrisoner
                        {
                            PrisonerId = p.Id

                        }).ToList()

                    };

                    sb.AppendLine($"Imported {item.FullName} ({item.Prisoners.Count} prisoners)");
                    context.Officers.Add(officer);
                }

                context.SaveChanges();
                return sb.ToString().TrimEnd();
        }

        private static bool IsValid(object obj)
        {
            var validationContext = new System.ComponentModel.DataAnnotations.ValidationContext(obj);
            var validationResult = new List<ValidationResult>();

            bool isValid = Validator.TryValidateObject(obj, validationContext, validationResult, true);
            return isValid;
        }
    }
}