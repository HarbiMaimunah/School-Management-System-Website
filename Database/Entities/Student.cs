using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebApi.Entities
{
    public class Student
    {
        [Key]
        [Display(Name = "ID")]
        public int ID { get; set; }
        [Required]
        [StringLength(50)]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Required]
        [StringLength(50)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        [Required]
        [Display(Name = "Birth Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime BirthDate { get; set; }
        [Required]
        [StringLength(6)]
        [Display(Name = "Gender")]
        public string Gender { get; set; }
        [Required]
        [Display(Name = "Enrollment Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime EnrollmentDate { get; set; }
        [Required]
        [StringLength(50)]
        [Display(Name = "Major")]
        public string Major { get; set; }
        [Required]
        [StringLength(10)]
        [Display(Name = "Status")]
        public string Status { get; set; }
        [Required]
        [Display(Name = "GPA")]
        public decimal GPA { get; set; }
        public virtual ICollection<Course> Courses { get; set; }
    }

}
