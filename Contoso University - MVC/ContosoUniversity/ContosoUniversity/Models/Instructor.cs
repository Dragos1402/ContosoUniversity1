using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ContosoUniversity.Models
{
    public class Instructor
    {
        
        public int ID { get; set; }
        [Required]
        [Display(Name="Last Name")]
        [StringLength(50)]
        #region Documentation
        /// <summary>
        /// Date for the weahter
        /// </summary>
        #endregion

        public string LastName { get; set; }
        /// <summary>
        /// Summary
        /// </summary>

        [Required]
        [Display(Name = "First Name")]
        [StringLength (50)]
        public string FirstMidName { get; set; }
        /// <summary>
        /// Summary
        /// </summary>

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Hire Date")]
        public DateTime HireDate { get; set; }
        [Display(Name= "Full Name")]
        /// <summary>
        /// Summary
        /// </summary>

        public string FullName
        {
            get
            {
                return LastName + "," + FirstMidName;
            }
        }
        /// <summary>
        /// Summary
        /// </summary>

        public OfficeAssignment OfficeAssignment { get; set; }
        /// <summary>
        /// Summary
        /// </summary>

        public ICollection<CourseAssignment> CourseAssignments { get; set; }
    }
}
