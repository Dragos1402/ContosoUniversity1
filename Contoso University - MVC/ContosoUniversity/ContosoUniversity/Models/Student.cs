using ContosoUniversity.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Drawing;

namespace ContosoUniversity.Models
{
    public class Student
    {
        [Key]
        public int ID { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string FirstMidName { get; set; }
        public DateTime EnrollmentDate { get; set; }
        [Display(Name = "FullName")]
        public string FullName
        {
            get
            {
                return LastName + "," + FirstMidName;
            }
        }
        [ValidateNever]
       public ICollection<Enrollment> Enrollments { get; set; }
    }
}