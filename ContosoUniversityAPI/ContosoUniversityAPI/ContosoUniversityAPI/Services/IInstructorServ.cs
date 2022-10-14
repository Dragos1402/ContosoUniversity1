using ContosoUniversityAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContosoUniversityAPI.Services
{
    public interface IInstructorServ
    {
       List<InstructorCount> Instructors { get; set; }
       List<InstructorCount> GetInstructors();
        InstructorCount GetInstructorID(int id);
        string AddInstructor(InstructorSimplu instructorSimplu);
        string UpdateInstructor(InstructorSimplu instructorSimplu, int id);
        string DeleteInstructor( int id);

    }
}
