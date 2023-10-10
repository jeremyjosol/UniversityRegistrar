using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UniversityRegistrar.Models;
using System.Collections.Generic;
using System.Linq;

namespace UniversityRegistrar.Controllers
{
  public class StudentsController : Controller
  {
    private readonly UniversityRegistrarContext _db;

    public StudentsController(UniversityRegistrarContext db)
    {
      _db = db;
    }

    public ActionResult Index()
    {
      List<Student> model = _db.Students.ToList();
      return View(model);
    }

    public ActionResult Create()
    {
      ViewBag.DepartmentId = new SelectList(_db.Departments, "DepartmentId", "Name");
      return View();
    }

    [HttpPost]
    public ActionResult Create(Student student)
    {
      if(!ModelState.IsValid)
      {
        ViewBag.DepartmentId = new SelectList(_db.Departments, "DepartmentId", "Name");
        return View(student);
      }
      _db.Students.Add(student);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult Details(int id)
    {
      Student thisStudent = _db.Students
                                .Include(student => student.Department)
                                .Include(student => student.JoinEntities)
                                .ThenInclude(student => student.Course)
                                .FirstOrDefault(student => student.StudentId == id);
      return View(thisStudent);
    }
    
    public ActionResult AddCourse(int id)
    {
      Student thisStudent = _db.Students.FirstOrDefault(student => student.StudentId == id);
      ViewBag.CourseId = new SelectList(_db.Courses, "CourseId", "CourseName");
      return View(thisStudent);
    }

    [HttpPost]
    public ActionResult AddCourse(Student student, int courseId)
    { // Check the table for our many-to-many relationship: does this student exist in the table, and is this student ALREADY enrolled in this course?
      // If this student is not enrolled in this course already, this relationship doesn't exist so this Enrollment object will be NULL
      // If this student IS already enrolled in this course, this Enrollment object will be populated.
      #nullable enable 
      Enrollment? joinEntity = _db.Enrollments.FirstOrDefault(join => (join.CourseId == courseId && join.StudentId == student.StudentId));
      #nullable disable
      // So we check here to see if this relationship already exists, and if this student exists
      // If the relationship DOES NOT exist, and the Student is real, create this new relationship!
      // Otherwise, if this DOES exist already, don't do anything -> redirect to the Course detail page only
      if (joinEntity == null && courseId != 0)
      {
        _db.Enrollments.Add(new Enrollment() { StudentId = student.StudentId, CourseId = courseId });
        _db.SaveChanges();
      }
      return RedirectToAction("Details", new { id = student.StudentId });
    }

    [HttpPost]
    public ActionResult DeleteEnrollment(int enrollmentId)
    {
      Enrollment joinEntry = _db.Enrollments.FirstOrDefault(entry => entry.EnrollmentId == enrollmentId);
      _db.Enrollments.Remove(joinEntry);
      _db.SaveChanges();
      return RedirectToAction("Details", new { id = joinEntry.StudentId });
    }
    
    public ActionResult Delete(int id)
    {
      Student thisStudent = _db.Students.FirstOrDefault(student => student.StudentId == id);
      return View(thisStudent);
    }

    [HttpPost, ActionName("Delete")]
    public ActionResult DeleteConfirmed(int id)
    {
      Student thisStudent = _db.Students.FirstOrDefault(student => student.StudentId == id);
      _db.Students.Remove(thisStudent);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }
  }
}