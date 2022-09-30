namespace ContosoUniversity.Models.SchoolViewModels
{
    public class AssignedCourseData
    {
        public int CourseID { get; set; }
        public string Title { get; set; }
        public bool Assigned { get; set; }
    }
}
// Este Modelul care ne ajuta sa facem CheckBox-urile. Ne preia Course ID si Titlul Cursului si Assigned verifica daca Checkbox-ul este bifat sau nu