using ContosoUniversityAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContosoUniversityAPI.Services
{
   public interface IDepartmentServ
    {
        List<Department> Departments { get; set; }
        List<Department> GetDepartments();
        DepartmentCount GetDepartmentID(int id);
        string AddDepartment(AddDepartment department);
        string UpdateDepartment(AddDepartment updateDepartment, int id);
    }
}
