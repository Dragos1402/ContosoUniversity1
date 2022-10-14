using ContosoUniversityAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContosoUniversityAPI.Services
{
    public interface ICourseServ
    {
         List<Course> Courses { get; set; }

         List<Course> GetCourses();

        List<CourseOnly> GetCourseID(int id);
        string AddCourse(AddCourse course);
        string UpdateCourse(AddCourse course, int id);

    }
}
