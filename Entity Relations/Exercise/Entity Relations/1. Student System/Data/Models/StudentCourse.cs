﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace P01_StudentSystem.Data.Models
{
    public class StudentCourse
    {
        [ForeignKey("Student")]
        public int StudentId { get; set; }

        public Student Student { get; set; }

        public int CourseId { get; set; }

        [ForeignKey("Course")]
        public Course Course { get; set; }

    }
}
