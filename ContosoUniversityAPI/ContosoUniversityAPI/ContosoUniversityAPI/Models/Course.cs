
namespace ContosoUniversityAPI.Models
{
    using System;
    using System.Collections.Generic;
    
    public class Course
    {
    
        public int CourseID { get; set; }
        public string Title { get; set; }
        public int Credits { get; set; }
        public int DepartmentID { get; set; }
    
        public  Department Department { get; set; }

        public  ICollection<Enrollment> Enrollments { get; set; }
       
        public  ICollection<Instructor> Instructors { get; set; }

        public Course()
        {
            Instructors= new List<Instructor>();
        }
    }
}
