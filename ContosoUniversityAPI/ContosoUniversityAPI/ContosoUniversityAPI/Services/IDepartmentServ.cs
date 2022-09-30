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
       List<DepartmentCount> Departments { get; set; }
        List<DepartmentCount> GetDepartments();
        DepartmentCount GetDepartmentID(int id);
    }
}
