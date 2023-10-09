using System.Collections.Generic;
using System;

namespace UniversityRegistrar.Models
{
  public class Student
  {
    public string Name { get; set; }
    public int StudentId { get; set; }
    public DateTime EnrollmentDate { get; set; }

    // Collection Navigation Property
    public List<Enrollment> JoinEntities { get; set; }
  }
}