
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using UniversityProject_2.Data;
using UniversityProject_2.Models;

namespace UniversityProject_2.Controllers
{
    public class StudentsController : Controller
    {
        private UniversityContext db = new UniversityContext();

        // GET: Students
        // GET: Students

        public ActionResult Index(string sortOrder, int? page)
        {
            // Sorting settings
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.AddressSortParm = sortOrder == "Address" ? "address_desc" : "Address";
            ViewBag.CampusSortParm = sortOrder == "CampusID" ? "campus_desc" : "CampusID";

            // Keep the current sorting order when changing pages
            ViewBag.CurrentSort = sortOrder;

            // Get all students
            var students = from s in db.Students
                           select s;

            // Apply sorting
            switch (sortOrder)
            {
                case "name_desc":
                    students = students.OrderByDescending(s => s.Name);
                    break;

                case "Address":
                    students = students.OrderBy(s => s.Address);
                    break;

                case "address_desc":
                    students = students.OrderByDescending(s => s.Address);
                    break;

                case "CampusID":
                    students = students.OrderBy(s => s.CampusID);
                    break;

                case "campus_desc":
                    students = students.OrderByDescending(s => s.CampusID);
                    break;

                default:
                    students = students.OrderBy(s => s.Name);
                    break;
            }

            // Paging settings
            int pageSize = 3;
            int pageNumber = page ?? 1;

            // Get the total number of students
            int totalStudents = students.Count();

            // Calculate the total number of pages
            int totalPages = (int)Math.Ceiling((double)totalStudents / pageSize);

            // Make sure the page number is valid
            if (pageNumber < 1)
            {
                pageNumber = 1;
            }

            if (totalPages > 0 && pageNumber > totalPages)
            {
                pageNumber = totalPages;
            }

            // Pass paging information to the View
            ViewBag.CurrentPage = pageNumber;
            ViewBag.TotalPages = totalPages;

            // Get students for the current page
            var pagedStudents = students
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            return View(pagedStudents);
        }


        // GET: Students/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Student student = db.Students.Find(id);

            if (student == null)
            {
                return HttpNotFound();
            }

            return View(student);
        }

        // GET: Students/Create
        public ActionResult Create()
        {
            ViewBag.CampusList = new SelectList(
                db.UniversityCampuses.ToList(),
                "ID",
                "Name"
            );

            return View();
        }

        // POST: Students/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(
            [Bind(Include = "ID,Name,Address,CampusID")] Student student)
        {
            // Check whether the selected campus exists
            if (!student.CampusID.HasValue ||
                !db.UniversityCampuses.Any(
                    c => c.ID == student.CampusID.Value))
            {
                ModelState.AddModelError(
                    "CampusID",
                    "Please select a valid campus."
                );
            }

            if (ModelState.IsValid)
            {
                db.Students.Add(student);
                db.SaveChanges();

                return RedirectToAction("Index");
            }

            // Reload campus list if validation fails
            ViewBag.CampusList = new SelectList(
                db.UniversityCampuses.ToList(),
                "ID",
                "Name",
                student.CampusID
            );

            return View(student);
        }

        // GET: Students/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Student student = db.Students.Find(id);

            if (student == null)
            {
                return HttpNotFound();
            }

            // Load campus list for dropdown
            ViewBag.CampusList = new SelectList(
                db.UniversityCampuses.ToList(),
                "ID",
                "Name",
                student.CampusID
            );

            return View(student);
        }

        // POST: Students/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(
            [Bind(Include = "ID,Name,Address,CampusID")] Student student)
        {
            // Check whether the selected campus exists
            if (!student.CampusID.HasValue ||
                !db.UniversityCampuses.Any(
                    c => c.ID == student.CampusID.Value))
            {
                ModelState.AddModelError(
                    "CampusID",
                    "Please select a valid campus."
                );
            }

            if (ModelState.IsValid)
            {
                db.Entry(student).State = EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("Index");
            }

            // Reload campus list if validation fails
            ViewBag.CampusList = new SelectList(
                db.UniversityCampuses.ToList(),
                "ID",
                "Name",
                student.CampusID
            );

            return View(student);
        }

        // GET: Students/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Student student = db.Students.Find(id);

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
            Student student = db.Students.Find(id);

            if (student == null)
            {
                return HttpNotFound();
            }

            db.Students.Remove(student);
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
