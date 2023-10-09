using System.Collections.Generic;

namespace UniversityRegistrar.Models
{
  public class Course
  {
    public int CourseId { get; set; }
    public string CourseName { get; set; }
    public string CourseNumber { get; set; }
    
    // Collection Navigation Property
    public List<Enrollment> JoinEntities { get; set; }
  }
}