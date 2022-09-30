
namespace ContosoUniversityAPI.Models
{
    using System;
    using System.Collections.Generic;
    
    public class Instructor
    {
 
        public int ID { get; set; }
        public string LastName { get; set; }
        public string FirstMidName { get; set; }
        public DateTime HireDate { get; set; }
        public  ICollection<DepartmentCount> Departments { get; set; }
        public  OfficeAssignment OfficeAssignment { get; set; }
        public  ICollection<Course> Courses { get; set; }
        public Instructor()
        {
            Departments = new List<DepartmentCount>();
            OfficeAssignment = new OfficeAssignment();
            Courses = new List<Course>();
        }
    }
    public class InstructorCount
    {
        public int ID { get; set; }
        public string LastName { get; set; }
        public string FirstMidName { get; set; }
        public DateTime HireDate { get; set; }
        public int TotalDepartments { get; set; }
        public int TotalCourses { get; set; }
    }
}
