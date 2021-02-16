using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace P01_StudentSystem.Data.Models
{
    public class Course
    {
        public int CourseId { get; set; }

        [Required]
        [Column(TypeName =("nvarchar(80)"))]
        public string Name { get; set; }

        [Column(TypeName = ("nvarchar(max)"))]
        public string Description  { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        [Column(TypeName = ("money"))]
        public double Price { get; set; }
    }
}
