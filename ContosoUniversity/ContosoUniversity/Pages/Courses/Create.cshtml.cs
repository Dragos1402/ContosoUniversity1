using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ContosoUniversity.Data;
using ContosoUniversity.Models;

namespace ContosoUniversity.Pages.Courses
{
    public class CreateModel : DepartmentNamePageModel
    {
        private readonly ContosoUniversity.Data.SchoolContext _context;

        public CreateModel(ContosoUniversity.Data.SchoolContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            PopulateDepartmentsDropDownList(_context);
            return Page();
        }

        [BindProperty]
        public Course Course { get; set; }
        public async Task<IActionResult> OnPostAsync()
        {
            var emptyCourse = new Course();
            if (await TryUpdateModelAsync(emptyCourse,
                "course",
                c=>c.CourseID, c=>c.DepartmentID, c=>c.Title, c=> c.Credits))
            {
                _context.Courses.AddAsync(emptyCourse);
                await _context.SaveChangesAsync();
                return RedirectToPage("./Index");

            }
            PopulateDepartmentsDropDownList(_context, emptyCourse.DepartmentID);
            return Page();
        }
    }
}
