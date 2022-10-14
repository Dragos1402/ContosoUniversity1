
namespace ContosoUniversityAPI.Models
{
    using ContosoUniversity.APIControllers;
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

        public List<StudentList> StudentLists { get; set; }

        public Course()
        {
            Instructors= new List<Instructor>();
            StudentLists= new List<StudentList>();
        }
    }
    public class CourseOnly
    {
        public int CourseID { get; set; }
        public string Title { get; set; }
        public int Credits { get; set; }
        public int DepartmentID { get; set; }
        public List<StudentList> StudentLists { get; set; }

        public CourseOnly()
        {
           StudentLists= new List<StudentList>();
        }
        
    }
    public class AddCourse
    {
        public int CourseID { get; set; }
        public string Title { get; set; }
        public int Credits { get; set; }
        public int DepartmentID { get; set; }

    }
}
