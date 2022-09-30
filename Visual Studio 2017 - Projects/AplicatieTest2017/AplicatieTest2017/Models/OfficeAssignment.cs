namespace AplicatieTest2017.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("OfficeAssignment")]
    public partial class OfficeAssignment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int InstructorID { get; set; }

        [Required]
        [StringLength(50)]
        public string Location { get; set; }

        public virtual Instructor Instructor { get; set; }
    }
}
