﻿using SoftJail.Data.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SoftJail.Data.Models
{
    public class Officer
    {
        public int Id { get; set; }

        [Required, MinLength(3),MaxLength(30)]
        public string FullName  { get; set; }

        [Range(0, double.MaxValue)]
        public decimal Salary  { get; set; }

        public Position Position  { get; set; }

        public Weapon Weapon { get; set; }

        public int DepartmentId { get; set; }

        public Department Department { get; set; }

        public ICollection<OfficerPrisoner> OfficerPrisoners { get; set; } = new HashSet<OfficerPrisoner>();


    }
}
