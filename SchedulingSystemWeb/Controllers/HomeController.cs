
using SchedulingSystemClassLibrary;
using SchedulingSystemClassLibrary.ViewModels;
using SchedulingSystemWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SchedulingSystemWeb.Controllers
{
    public class HomeController : Controller
    {
        private SchedulingContext _context;

        public HomeController()
        {
            _context = new SchedulingContext();
        }

        public ActionResult Index()
        {
            var applicationDbContext = new ApplicationDbContext();

            var viewModel = new CollegeDeanHomeViewModel
            {
                CoursesCount = _context.Courses.Count(),
                DepartmentsCount = _context.Departments.Count(),
                SectionsCount = _context.Sections.Count(),
                BuildingsCount = _context.Buildings.Count(),
                InstructorsCount = _context.Instructors.Count(),
                //UsersCount = applicationDbContext.Users.Count()
                RoomsCount = _context.Rooms.Count(),
                CourseOfferingsCount = _context.CourseOfferings.Count()
            };

            return View("CollegeDeanHome", viewModel);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }
    }
}