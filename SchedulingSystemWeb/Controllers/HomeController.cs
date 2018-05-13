
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
using SchedulingSystemClassLibrary.ViewModels.DepartmentHead;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;

namespace SchedulingSystemWeb.Controllers
{
    public class HomeController : Controller
    {
        private SchedulingContext _context;

        public HomeController()
        {
            _context = new SchedulingContext();
        }

        public async Task<ActionResult> Index()
        {
            if (User.IsInRole(RoleName.IsACollegeDean))
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

                return View("CollegeDeanHome", viewModel);
            }
            else if(User.IsInRole(RoleName.IsADepartmentHead))
            {
                var semester = _context.AcademicSemesters.Count() > 0 ? _context.AcademicSemesters.Include(s => s.AcademicYear).Single(s => s.CurrentSemester) : null;
                var userManager = new ApplicationUserManager(new UserStore<ApplicationUser>(new ApplicationDbContext()));
                var user = await userManager.FindByNameAsync(User.Identity.Name);
                var deptHead = user == null ? null : _context.Instructors.Include(i => i.Department).Single(i => i.AccountId == user.Id);

                var viewModel = new DepartmentHeadHomeViewModel
                {
                    CoursesCount = _context.Courses.Count(),
                    DepartmentsCount = _context.Departments.Count(),
                    SectionsCount = _context.Sections.Count(),
                    BuildingsCount = _context.Buildings.Count(),
                    InstructorsCount = _context.Instructors.Count(),
                    //UsersCount = applicationDbContext.Users.Count()
                    RoomsCount = _context.Rooms.Count(),
                    CourseOfferingsCount = _context.CourseOfferings.Count(),
                    Semester = semester, 
                    Department = deptHead.Department
                };

                return View("DepartmentHeadHome", viewModel);
            }
            return RedirectToAction("Login", "Account");

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