using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace UniversityRegistrar.Models
{
  public class Course
  {
    public int CourseId { get; set; }
    [Required(ErrorMessage = "* The course name can't be empty.")]
    public string CourseName { get; set; }
    [Required(ErrorMessage = "* The course number can't be empty.")]
    public string CourseNumber { get; set; }
    
    // Collection Navigation Property
    public List<Enrollment> JoinEntities { get; set; }
  }
}