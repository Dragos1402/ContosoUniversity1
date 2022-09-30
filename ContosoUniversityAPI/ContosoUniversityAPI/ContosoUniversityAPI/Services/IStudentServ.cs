using ContosoUniversityAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContosoUniversityAPI.Services
{
    public interface IStudentServ
    {
        Student StudentID { get; set; }
        List<StudentList> Students { get; set; }
        List<StudentList> GetStudents();
        Student GetStudentByID(int id);
    }
}
