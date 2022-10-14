using ContosoUniversityAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ContosoUniversityAPI.Models;

namespace ContosoUniversityAPI.Services
{
    public interface IOfficeAssignmentServ
    {
        List<OfficeAssignment> OfficeAssignments { get; set; }
        string GetOfficeAssignment();
        List<OfficeAssignment> GetOfficeAssignments(int id);
        string UpdateOfficeAssignment(AddOfficeAssignment officeAssignment, int id);
        string AddOfficeAssignment(AddOfficeAssignment officeAssignment);
    }
}
