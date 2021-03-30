using System.Globalization;
using System.Linq;
using Microsoft.VisualBasic;
using Newtonsoft.Json;
using ProductShop;
using SoftJail.DataProcessor.ExportDto;

namespace SoftJail.DataProcessor
{

    using Data;
    using System;
    using static SoftJail.DataProcessor.ExportDto.PrisonerExportDto;
    using static SoftJail.DataProcessor.ExportDto.PrisonerOfficerExportDto;

    public class Serializer
    {
        public static string ExportPrisonersByCells(SoftJailDbContext context, int[] ids)
        {
            //Export all prisoners that were processed which have these ids. For each prisoner, get their id, name, cell number they are placed in, their officers with each officer name, and the department name they are responsible for. At the end export the total officer salary with exactly two digits after the decimal place.Sort the officers by their name(ascending), sort the prisoners by their name(ascending), then by the prisoner id(ascending).

            var prisonersOfficersDtos = context.Prisoners.Where(p => ids.Contains(p.Id)).Select(p =>
                new PrisonerOfficerExportDto
                {
                    Id = p.Id,
                    Name = p.FullName,
                    CellNumber = p.Cell.CellNumber,
                    Officers = p.PrisonerOfficers.Select(o => new OfficerExportDto
                    {
                        Department = o.Officer.Department.Name,
                        OfficerName = o.Officer.FullName
                    }).OrderBy(x => x.OfficerName).ToList(),
                    TotalOfficerSalary = decimal.Round(p.PrisonerOfficers.Select(o => o.Officer.Salary).Sum(),2)
                }).OrderBy(x => x.Name).ThenBy(x => x.Id).ToList();

            var result = JsonConvert.SerializeObject(prisonersOfficersDtos,Formatting.Indented);

            return result;
        }

        public static string ExportPrisonersInbox(SoftJailDbContext context, string prisonersNames)
        {
            //Use the method provided in the project skeleton, which receives a string of comma - separated prisoner names. Export the prisoners: for each prisoner, export its id, name, incarcerationDate in the format “yyyy - MM - dd” and their encrypted mails.The encrypted algorithm you have to use is just to take each prisoner mail description and reverse it.Sort the prisoners by their name(ascending), then by their id(ascending).

            var prisonersNameArr = prisonersNames.Split(",");

            var prisoners = context.Prisoners.Where(p => prisonersNameArr.Contains(p.FullName)).Select(p =>
                new PrisonerExportDto
                {
                    Id = p.Id,
                    Name = p.FullName,
                    IncarcerationDate = p.IncarcerationDate.ToString("yyyy-MM-dd",CultureInfo.InvariantCulture),
                    Mails = p.Mails.Select(m => new MailExportDto
                    {
                        Description = string.Join("", m.Description.ToArray().Reverse())
                    }).ToList()
                }).OrderBy(x => x.Name).ThenBy(x => x.Id).ToList();

             var result = XmlConverter.Serialize(prisoners, "Prisoners");

             return result;
        }
    }
}