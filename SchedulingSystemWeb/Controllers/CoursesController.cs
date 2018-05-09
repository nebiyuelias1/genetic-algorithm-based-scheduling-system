using SchedulingSystemClassLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using SchedulingSystemClassLibrary.ViewModels;
using SchedulingSystemClassLibrary.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Threading.Tasks;

namespace SchedulingSystemWeb.Controllers
{
    public class CoursesController : Controller
    {
        private SchedulingContext _context;

        public CoursesController()
        {
            _context = new SchedulingContext(); 
        }
        // GET: Courses
        public ActionResult Index()
        {
            var courses = _context.Courses.Include(c => c.Curriculum).ToList(); 

            return View(courses);
        }

        public async Task<ActionResult> New()
        {
            if (User.IsInRole(RoleName.IsACollegeDean))
            {
                var curriculums = _context.Curriculums.ToList();

                var viewModel = new CoursesFormViewModel
                {
                    Curriculums = curriculums
                };

                return View("CourseForm", viewModel);
            }
            else if (User.IsInRole(RoleName.IsADepartmentHead))
            {
                var userManager = new ApplicationUserManager(new UserStore<ApplicationUser>(new ApplicationDbContext()));
                var user = await userManager.FindByNameAsync(User.Identity.Name);
                var deptHead = _context.Instructors.Single(i => i.AccountId == user.Id);

                var curriculums = _context.Curriculums.Where(c => c.DepartmentId == deptHead.DepartmentId).ToList();

                var viewModel = new CoursesFormViewModel
                {
                    Curriculums = curriculums
                };

                return View("CourseForm", viewModel);
            }

            return RedirectToAction("Login", "Accounts");
        }

        public ActionResult Edit(int id)
        {
            var course = _context.Courses.Single(c => c.Id == id); 
            var curriculums = _context.Curriculums.ToList();

            var viewModel = new CoursesFormViewModel(course)
            {
                Curriculums = curriculums
            };

            return View("CourseForm", viewModel);
        }

        [HttpPost]
        public ActionResult Save(Course course)
        {
            if (course.Id == 0)
            {
                _context.Courses.Add(course); 
            }
            else
            {
                var courseInDb = _context.Courses.Single(c => c.Id == course.Id);

                courseInDb.Id = course.Id;
                courseInDb.CourseCode = course.CourseCode;
                courseInDb.Credit = course.Credit;
                courseInDb.CurriculumId = course.CurriculumId;
                courseInDb.DeliverySemester = course.DeliverySemester;
                courseInDb.DeliveryYear = course.DeliveryYear;
                courseInDb.Laboratory = course.Laboratory;
                courseInDb.Lecture = course.Lecture;
                courseInDb.Title = course.Title;
                courseInDb.Tutor = course.Tutor;
                courseInDb.Color = course.Color;
                courseInDb.Acronym = course.Acronym;

            }

            _context.SaveChanges(); 
            return RedirectToAction("Index", "Courses"); 
        }
        protected override void Dispose(bool disposing)
        {
            _context.Dispose(); 
        }
    }
}