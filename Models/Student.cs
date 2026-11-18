using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace UniversityProject_2.Models
{
    public class Student
    {
        [Key]
        public int ID { get; set; }
        [Required(ErrorMessage ="Students' name is required.")]
        [StringLength(50,ErrorMessage ="Name cannot excees 50 characters.")]
        [Display(Name="Student Name")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Students' address is required.")]
        [StringLength(100, ErrorMessage = "Address cannot excees 100 characters.")]
        [Display(Name = "Student Address")]
        public string Address { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "Please select a campus.")]
        [Display(Name = "Campus")]
        public int? CampusID { get; set; }
        public virtual UniversityCampus Campus { get; set; }
    }
}