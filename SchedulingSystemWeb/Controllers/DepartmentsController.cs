using SchedulingSystemClassLibrary;
using SchedulingSystemClassLibrary.Models;
using SchedulingSystemClassLibrary.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;

namespace SchedulingSystemWeb.Controllers
{
    public class DepartmentsController : Controller
    {
        private SchedulingContext _context;

        public DepartmentsController()
        {
            _context = new SchedulingContext();
        }
        // GET: Departments
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult New()
        {
            var viewModel = new DepartmentsFormViewModel
            {
                Faculties = _context.Faculties.ToList()
            };

            return View("DepartmentForm", viewModel);
        }

        public ActionResult Edit(int id)
        {
            var faculties = _context.Faculties.ToList();
            var departmentInDb = _context.Departments.Single(d => d.Id == id);

            var viewModel = new DepartmentsFormViewModel(departmentInDb);
            viewModel.Faculties = faculties;

            return View("DepartmentForm", viewModel);
        }

        [HttpPost]
        public ActionResult Save(Department department)
        {
            if (ModelState.IsValid)
            {
                if (department.Id == 0)
                {
                    _context.Departments.Add(department);
                }
                else
                {
                    var deparmentInDb = _context.Departments.Single(d => d.Id == department.Id);

                    deparmentInDb.Name = department.Name;
                    deparmentInDb.Email = department.Email;
                    deparmentInDb.FacultyId = department.FacultyId;
                    deparmentInDb.Fax = department.Fax;
                    deparmentInDb.Phone = department.Phone; 
                }

                _context.SaveChanges();
                return RedirectToAction("Index", "Departments");
            }
            return View();
        }

        public ActionResult Instructors(int id)
        {
            var instructors = _context
                            .Instructors
                            .Include(i => i.InstructorPreference)
                            .Include(i => i.CurrentlyAssignedCourses
                            .Select(c => c.Course))
                            .Where(i => i.DepartmentId == id).ToList();

            var viewModel = new DepartmentInstructorsViewModel
            {
                DepartmentName = _context.Departments.Single(d => d.Id == id).Name, 
                Instructors = instructors
            };

            return View(viewModel);
        }

        public ActionResult Sections(int id)
        {
            var sections = _context.Sections
                        .Include(s => s.AssignedLabRoom)
                        .Include(s => s.AssignedLectureRoom)
                        .Include(s => s.CourseOfferings)
                        .Where(s => s.DepartmentId == id)
                        .ToList();

            var departmentName = _context.Departments.Single(d => d.Id == id).Name;

            var viewModel = new DepartmentSectionsViewModel
            {
                Sections = sections,
                DepartmentName = departmentName
            };

            return View(viewModel);
        }
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }
    }
}