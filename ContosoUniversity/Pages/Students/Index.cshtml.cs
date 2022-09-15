using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ContosoUniversity.Data;
using ContosoUniversity.Models;
using System.Security.Cryptography.X509Certificates;
using System.Configuration;

namespace ContosoUniversity.Pages.Students
{
    public class IndexModel : PageModel
    {
        private readonly SchoolContext _context;
        private readonly IConfiguration _configuration;
        public IndexModel(SchoolContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public string NameSort { get; set; }
        public string DateSort { get; set; }
        public string CurrentFilter { get; set; }
        public string CurrentSort { get; set; }
        public PaginatedList<Student> Students { get; set; }
      

    /* Prima linie (NameSort) imi specifica ca atunci canda sortOrder este null sau empty
    NameSort se seteaza pe name_desc. In caz contrar, NameSort este setat pe un string gol*/

    //Cele doua linii imi seteaza hyperlink-urile (NameSort/DateSort/Currentfilter)
    // si sunt denumiri pentru switch-case function (name_desc, Date, date_desc)
    public async Task OnGetAsync(string sortOrder, string searchString, int? pageIndex, 
                                 string currentFilter)

        {
            CurrentSort = sortOrder;
            NameSort = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            DateSort = sortOrder == "Date" ? "date_desc" : "Date"; 
            
  /* Daca am scris ceva pe bara de search si am apasat pe buton, imi va afisa pagina 1
     in rest, pagina actuala (if) */
           if(searchString!= null)
            {
                pageIndex = 1;
            }
            else
            {
                searchString = currentFilter;
            }

           CurrentFilter=searchString;
           IQueryable<Student> studentsIQ = from s in _context.Students
                                             select s;

   // Imi memoreaza string-ul in searchString, iar daca nu este Null sau empty, imi cauta
   // in tabel(database) daca variabila searchString coincide cu numele din database)

         if (!String.IsNullOrEmpty(searchString))
            {
    studentsIQ = studentsIQ.Where(s => s.LastName.Contains(searchString) || s.FirstName.Contains(searchString));

            }
   
            switch (sortOrder)
            {
                case "name_desc":
                    studentsIQ = studentsIQ.OrderByDescending(s => s.LastName);
                    break;
                case "Date":
                    studentsIQ = studentsIQ.OrderBy(s => s.EnrollmentDate);
                    break;
                case "date_desc":
                    studentsIQ = studentsIQ.OrderByDescending(s => s.EnrollmentDate);
                    break;
                default:
                    studentsIQ = studentsIQ.OrderBy(s => s.LastName);
                    break;
            }
            var pageSize = _configuration.GetValue("PageSize", 4);
            Students = await PaginatedList<Student>.CreateAsync(
                studentsIQ.AsNoTracking(), pageIndex ?? 1, pageSize);
        }
    }
}
