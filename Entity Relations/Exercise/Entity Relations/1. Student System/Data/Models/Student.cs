using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace P01_StudentSystem.Data.Models
{
    public class Student
    {
        
        public int Id { get; set; }

        [Required]
        [Column(TypeName =("nvarchar(100)"))]
        public string Name { get; set; }

        [Column(TypeName =("char(10)"))]
        public string PhoneNumber  { get; set; }

        public DateTime RegisteredOn { get; set; }

        public DateTime? Birthday  { get; set; }


    }
}
