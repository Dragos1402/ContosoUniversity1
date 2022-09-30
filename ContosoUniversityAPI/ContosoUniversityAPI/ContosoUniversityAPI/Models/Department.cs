
namespace ContosoUniversityAPI.Models
{
    using System;
    using System.Collections.Generic;
    
    public class Department
    {

        public int DepartmentID { get; set; }
        public string Name { get; set; }
        public decimal Budget { get; set; }
        public DateTime StartDate { get; set; }
        public int InstructorID { get; set; }
        public  ICollection<Course> Courses { get; set; }
        public  Instructor Instructor { get; set; }

        public Department()
        {
            Courses = new List<Course>();
        }
    }
    public class DepartmentCount
    {
        public int DepartmentID { get; set; }
        public string Name { get; set; }
        public decimal Budget { get; set; }
        public DateTime StartDate { get; set; }
        public int InstructorID { get; set; }
        public int TotalCourses { get; set; }
    }
}
