using SchedulingSystemClassLibrary;
using SchedulingSystemClassLibrary.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;

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