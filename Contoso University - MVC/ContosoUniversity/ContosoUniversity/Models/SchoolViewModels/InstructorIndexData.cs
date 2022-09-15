namespace ContosoUniversity.Models.SchoolViewModels
{
    public class InstructorIndexData
    {
      public  IEnumerable<Instructor> Instructors { get; set; }  // cu IEnumerable stochez datele ce apartin entitatilor dorite intr-o variabila 
        public IEnumerable<Course> Courses { get; set; }
        public IEnumerable<Enrollment> Enrollments { get; set; }
    }
}
