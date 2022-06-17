using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Entities
{
    public class Teacher
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
        [StringLength(50)]
        [Display(Name = "Department")]
        public string Department { get; set; }
        [Required]
        [Display(Name = "Salary")]
        public int Salary { get; set; }

        public virtual ICollection<Course> Courses { get; set; }
    }
}
