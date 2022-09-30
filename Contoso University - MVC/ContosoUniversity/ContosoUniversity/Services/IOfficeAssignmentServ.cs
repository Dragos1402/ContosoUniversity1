using ContosoUniversity.Models;

namespace ContosoUniversity.Services
{
    public interface IOfficeAssignmentServ
    {
        List<OfficeAssignment> OfficeAssignments { get; set; }

        List<OfficeAssignment> GetOfficeAssignments();
    }
}
