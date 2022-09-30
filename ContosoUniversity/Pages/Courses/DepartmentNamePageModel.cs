using ContosoUniversity.Data;
using ContosoUniversity.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace ContosoUniversity.Pages.Courses
{
    public class DepartmentNamePageModel : PageModel
    {
        public SelectList DepartmentNameSL { get; set; }  // SelectList reprezinta o lista ce lasa utilizatorii sa selecteze un singur item

        public void PopulateDepartmentsDropDownList(SchoolContext _context, object selectDepartment=null) // Imi populez lista de departamente folosindu-ma de Baza de date(SchoolContext) si un obj)
        {
            var departmentsQuery = from d in _context.Departments
                                   orderby d.Name descending
                                   select d;
            DepartmentNameSL = new SelectList(departmentsQuery.AsNoTracking(), "DepartmentID", "Name", selectDepartment); //
        }

    }
}
