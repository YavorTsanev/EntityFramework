﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace P01_HospitalDatabase.Data.Models
{
    public class Doctor
    {
        public int DoctorId { get; set; }

        [Required, Column(TypeName ="nvarchar(100)")]
        public string Name { get; set; }

        [Required, Column(TypeName = "nvarchar(100)")]
        public string Specialty { get; set; }

        public ICollection<Visitation> Visitations { get; set; } = new HashSet<Visitation>();
    }
}
