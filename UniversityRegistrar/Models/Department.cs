using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace UniversityRegistrar.Models
{
  public class Department
  {
    public int DepartmentId { get; set; }
    [Required(ErrorMessage = "* Department Name can't be empty.")]
    public string Name { get; set; }
    public List<Course> Courses { get; set; }
    public List<Student> Students { get; set; }
  }
}