﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Xml.Serialization;
using TeisterMask.Data.Models;
using TeisterMask.Data.Models.Enums;

namespace TeisterMask.DataProcessor.ImportDto
{
    [XmlType("Project")]
    public class ProjectImportDto
    {
        [Required]
        [MinLength(2)]
        [MaxLength(40)]
        public string Name { get; set; }

        [Required]
        public string OpenDate { get; set; }

        public string DueDate { get; set; }

        [XmlArray("Tasks")]
        public List<TaskImportDto> Tasks { get; set; }

        [XmlType("Task")]
        public class TaskImportDto
        {
            [XmlElement("Name")]
            [Required]
            [MinLength(2)]
            [MaxLength(40)]
            public string TaskName { get; set; }

            [Required]
            [XmlElement("OpenDate")]
            public string TaskOpenDate { get; set; }

            [Required]
            [XmlElement("DueDate")]
            public string TaskDueDate { get; set; }

            [Range(0,3)]
            public int ExecutionType { get; set; }

            [Range(0, 4)]
            public int LabelType { get; set; }


        }
    }

    //<Project>
    // <Name>S</Name>
    // <OpenDate>25/01/2018</OpenDate>
    // <DueDate>16/08/2019</DueDate>
    // <Tasks>
    //   <Task>
    //     <Name>Australian</Name>
    //     <OpenDate>19/08/2018</OpenDate>
    //     <DueDate>13/07/2019</DueDate>
    //     <ExecutionType>2</ExecutionType>
    //     <LabelType>0</LabelType>
    //   </Task>

}
