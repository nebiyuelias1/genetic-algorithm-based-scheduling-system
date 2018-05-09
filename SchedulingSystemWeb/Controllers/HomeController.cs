
using SchedulingSystemClassLibrary;
using SchedulingSystemClassLibrary.Models;
using SchedulingSystemClassLibrary.ViewModels;
using SchedulingSystemWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;

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
            if (User.IsInRole(RoleName.IsACollegeDean))
            {
                var viewModel = new CollegeDeanHomeViewModel
                {
                    CoursesCount = _context.Courses.Count(),
                    DepartmentsCount = _context.Departments.Count(),
                    SectionsCount = _context.Sections.Count(),
                    BuildingsCount = _context.Buildings.Count(),
                    InstructorsCount = _context.Instructors.Count(),
                    //UsersCount = applicationDbContext.Users.Count()
                    RoomsCount = _context.Rooms.Count(),
                    CourseOfferingsCount = _context.CourseOfferings.Count(),
                    Semester = _context.AcademicSemesters.Include(s => s.AcademicYear).Single(s => s.CurrentSemester)
                };

                return View("CollegeDeanHome", viewModel);
            }
            else
            {
                var semester = _context.AcademicSemesters.Count() > 0 ? _context.AcademicSemesters.Include(s => s.AcademicYear).Single(s => s.CurrentSemester) : null; 

                var viewModel = new CollegeDeanHomeViewModel
                {
                    CoursesCount = _context.Courses.Count(),
                    DepartmentsCount = _context.Departments.Count(),
                    SectionsCount = _context.Sections.Count(),
                    BuildingsCount = _context.Buildings.Count(),
                    InstructorsCount = _context.Instructors.Count(),
                    //UsersCount = applicationDbContext.Users.Count()
                    RoomsCount = _context.Rooms.Count(),
                    CourseOfferingsCount = _context.CourseOfferings.Count(),
                    Semester = semester
                };

                return View("DepartmentHeadHome", viewModel);
            } 
            
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