using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LodhranUniversity.Models
{
    public class Student
    {
        public int StudentId { get; set; }
        public string StudentName { get; set; }

        public List<Course> Courses { get; set; }
    }

    public class Course
    {
        public int CourseId { get; set; }
        public string CourseTitle { get; set; }
        public string CourseCreditHours { get; set; }
        public string Grade { get; set; }
    }
}