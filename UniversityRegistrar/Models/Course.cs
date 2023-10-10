using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace UniversityRegistrar.Models
{
  public class Course
  {
    public int CourseId { get; set; }
    [Required(ErrorMessage = "* The Course Name can't be empty.")]
    public string CourseName { get; set; }
    [Required(ErrorMessage = "* The Course Number can't be empty.")]
    public string CourseNumber { get; set; }
    
    // Collection Navigation Property
    public List<Enrollment> JoinEntities { get; set; }
    // Foreign Key
    [Range(1, int.MaxValue, ErrorMessage = "* You must add a course to a department.")]
    public int DepartmentId { get; set; }
    // Reference Navigation Property
    public Department Department { get; set; }
  }
}