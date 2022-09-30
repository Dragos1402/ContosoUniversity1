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
         List<OfficeAssignment> GetOfficeAssignments(int id);
    }
}
