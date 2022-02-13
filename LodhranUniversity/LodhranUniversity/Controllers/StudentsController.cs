using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using LodhranUniversity.Data;
using LodhranUniversity.Models;

namespace LodhranUniversity.Controllers
{
    public class StudentsController : Controller
    {
        private UniversityModelContainer db = new UniversityModelContainer();

        // GET: Students
        public ActionResult Index()
        {
            return View(db.Students.ToList());
        }

        // GET: Students/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            //Student student = db.Students.Find(id);
            Data.Student student = db.Students
                .Include(x => x.Enrollments)
                .Include(x => x.Enrollments.Select(y => y.Course))
                .FirstOrDefault(x => x.ID == id);

            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        // GET: Students/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Students/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,FirstName,LastName,EnrollmentDate")] Data.Student student)
        {
            if (ModelState.IsValid)
            {
                db.Students.Add(student);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(student);
        }

        // GET: Students/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Data.Student student = db.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        // POST: Students/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,FirstName,LastName,EnrollmentDate")] Data.Student student)
        {
            if (ModelState.IsValid)
            {
                db.Entry(student).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(student);
        }

        // GET: Students/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Data.Student student = db.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        // POST: Students/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Data.Student student = db.Students.Find(id);
            db.Students.Remove(student);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: Students/Enroll/5
        public ActionResult Enroll(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Data.Student student = db.Students
                .Include(x => x.Enrollments)
                .Include(x => x.Enrollments.Select(y => y.Course))
                .FirstOrDefault(x => x.ID == id);

            var model = new Models.Student()
            {
                StudentId = student.ID,
                StudentName = $"{student.FirstName} {student.LastName}",
                Courses = student.Enrollments.Select(x => new Models.Course()
                {
                    CourseId = x.CourseID,
                    CourseTitle = x.Course.Title,
                    CourseCreditHours = x.Course.CreditHours,
                    Grade = x.Grade
                }).ToList()
            };

            var studentCoursesIDs = model.Courses.Select(y => y.CourseId);
            var allCourses = db.Courses
                .Where(x => !studentCoursesIDs.Contains(x.ID))
                .Select(x => new Models.Course()
                {
                    CourseId = x.ID,
                    CourseTitle = x.Title,
                    CourseCreditHours = x.CreditHours,
                }).ToList();

            model.Courses.AddRange(allCourses);

            return View(model);
        }

        // POST: Students/Enroll/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Enroll(Models.Student student)
        {
            foreach (var course in student.Courses)
            {
                if (string.IsNullOrEmpty(course.Grade)) continue;

                var enrollment = db.Enrollments.FirstOrDefault(x => x.StudentID == student.StudentId && x.CourseID == course.CourseId);
                    
                if(enrollment == null)
                {
                    enrollment = new Data.Enrollment()
                    {
                        CourseID = course.CourseId,
                        StudentID = student.StudentId,
                        Grade = course.Grade,
                    };
                    db.Enrollments.Add(enrollment);
                }
                else
                    enrollment.Grade = course.Grade;
            }
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
