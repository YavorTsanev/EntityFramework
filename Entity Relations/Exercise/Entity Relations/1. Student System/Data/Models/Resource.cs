using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace P01_StudentSystem.Data.Models
{
    public class Resource
    {
        public int ResourceId { get; set; }


        [Column(TypeName =("nvarcahr(50)"))]
        public string  Name { get; set; }


    }
}
