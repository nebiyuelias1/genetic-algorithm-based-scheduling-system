using SchedulingSystemClassLibrary;
using SchedulingSystemClassLibrary.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using SchedulingSystemClassLibrary.Models;

namespace SchedulingSystemWeb.Controllers
{
    public class InstructorsController : Controller
    {
        private SchedulingContext _context;

        public InstructorsController()
        {
            _context = new SchedulingContext();
        }
        // GET: Instructors
        public ActionResult Index()
        {
            var model = _context
                        .Instructors
                        .Include(i => i.Department)
                        .Include(i => i.CurrentlyAssignedCourses
                        .Select(c => c.Course))
                        .ToList();
            return View(model);
        }
         public ActionResult New()
        {
            var departments = _context.Departments  .ToList();

            var viewModel = new InstructorsFormViewModel
            {
                Departments = departments
            };

            return View("InstructorForm", viewModel);
        }

        [HttpPost]
        public ActionResult Save(InstructorsFormViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.Id == 0)
                {
                    var instructor = new Instructor
                    {
                        FirstName = model.FirstName,
                        FatherName = model.FatherName,
                        GrandFatherName = model.GrandFatherName,
                        DepartmentId = model.DepartmentId
                    };

                    _context.Instructors.Add(instructor);
                }
                else
                {
                    var instructorInDB = _context.Instructors.Single(c => c.Id == model.Id);

                    instructorInDB.FirstName = model.FirstName;
                    instructorInDB.FatherName = model.FatherName;
                    instructorInDB.GrandFatherName = model.GrandFatherName;
                    instructorInDB.DepartmentId = model.DepartmentId;


                }

                _context.SaveChanges();
            }
            
            return RedirectToAction("Index", "Instructors");
        }

        public ActionResult Edit(int id) 
        {
            var instructor = _context.Instructors.Single(i => i.Id == id);
            var departments = _context.Departments.ToList();

            var viewModel = new InstructorsFormViewModel(instructor)
            {
                Departments=departments
            };

            return View("InstructorForm", viewModel);
        }
        public ActionResult CourseOfferings(int id)
        {
            var courseOfferings = _context
                                .CourseOfferings
                                .Include(c => c.Section.Department)
                                .Include(c => c.Course)
                                .Where(c => c.InstructorId == id)
                                .ToList();

            var viewModel = new InstructorCourseOfferingsViewModel
            {
                CourseOfferings = courseOfferings, 
                InstructorFullName = _context.Instructors.Single(i => i.Id == id).FullName 
            };

            return View(viewModel);
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }
    }
}