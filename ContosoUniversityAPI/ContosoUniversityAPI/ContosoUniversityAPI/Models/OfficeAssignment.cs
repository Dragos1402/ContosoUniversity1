
namespace ContosoUniversityAPI.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class OfficeAssignment
    {
        public int InstructorID { get; set; }
        public string Location { get; set; }
    
        public  Instructor Instructor { get; set; }
        public InstructorCount InstructorCount { get; set; }
    }
    public class AddOfficeAssignment
    {
        [Key]
        public int InstructorID { get; set; }
        public string Location { get; set; }
    }
}
