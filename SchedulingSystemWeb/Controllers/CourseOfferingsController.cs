using SchedulingSystemClassLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using AutoMapper;
using SchedulingSystemClassLibrary.Models;
using SchedulingSystemClassLibrary.ViewModels;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;

namespace SchedulingSystemWeb.Controllers
{
    public class CourseOfferingsController : Controller
    {
        private SchedulingContext _context;

        public CourseOfferingsController()
        {
            _context = new SchedulingContext();
        }

        // GET: CourseOfferings
        public async Task<ActionResult> Index()
        {
            if (User.IsInRole(RoleName.IsACollegeDean))
            {
                var courseOfferingsCount = _context
                                            .CourseOfferings
                                            .Include(c => c.AcademicSemester)
                                            .Where(c => c.AcademicSemester.CurrentSemester)
                                            .Count();

                var viewModel = new CourseOfferingsViewModel
                {
                    CourseOfferingsCount = courseOfferingsCount
                };

                return View(viewModel);
            }
            else
            {
                var userManager = new ApplicationUserManager(new UserStore<ApplicationUser>(new ApplicationDbContext()));
                var user = await userManager.FindByNameAsync(User.Identity.Name);
                var deptHead = user == null ? null : _context.Instructors.Include(i => i.Department).Single(i => i.AccountId == user.Id);

                var courseOfferingsCount = _context
                                            .CourseOfferings
                                            .Include(c => c.AcademicSemester)
                                            .Include(c => c.Course.Curriculum)
                                            .Where(c => c.AcademicSemester.CurrentSemester && c.Course.Curriculum.DepartmentId == deptHead.DepartmentId)
                                            .Count();

                var viewModel = new CourseOfferingsViewModel
                {
                    CourseOfferingsCount = courseOfferingsCount,
                    Department = deptHead.Department
                };

                return View("DepartmentHeadIndex", viewModel);
            }

        }

        public ActionResult Assign(int id)
        {
            var courseOffering = _context.CourseOfferings
                                        .Include(c => c.Section)
                                        .Include(c => c.Instructor)
                                        .Include(c => c.Course).Single(c => c.Id == id);
            // TODO - This is where the instructor allotment would happen based on interest, previous background, etc. 

            var viewModel = Mapper.Map<CourseOffering, CourseOfferingsFormViewModel>(courseOffering);

            var instructors = _context.Instructors.ToList();

            viewModel.Instructors = instructors;

            return View(viewModel);

        }

        public async Task<ActionResult> Create()
        {
            var userManager = new ApplicationUserManager(new UserStore<ApplicationUser>(new ApplicationDbContext()));
            var user = await userManager.FindByNameAsync(User.Identity.Name);
            var deptHead = user == null ? null : _context.Instructors.Include(i => i.Department).Single(i => i.AccountId == user.Id);

            var sections = _context
                            .Sections
                            .Where(s => s.DepartmentId == deptHead.DepartmentId)
                            .ToList();

            var currentSemester = _context
                                    .AcademicSemesters
                                    .Include(s => s.AcademicYear)
                                    .Single(s => s.CurrentSemester);

            foreach (var section in sections)
            {
                section.CurrentYear = (byte)((int.Parse(currentSemester.AcademicYear.EtYear) - section.EntranceYear) + 1);
                var courses = _context
                                .Courses
                                .Where(c => c.DeliveryYear == section.CurrentYear && c.DeliverySemester == currentSemester.Semester);

                foreach (var course in courses)
                {
                    _context.CourseOfferings.Add(
                        new CourseOffering
                        {
                            Name = course.Title,
                            SectionId = section.Id, 
                            CourseId = course.Id, 
                            AcademicSemesterId = currentSemester.Id
                        }
                    );
                }
            }

            _context.SaveChanges();
            return RedirectToAction("Index", "CourseOfferings");
        }

        [HttpPost]
        public ActionResult Save(CourseOffering courseOffering)
        {
            var courseOfferingInDb = _context.CourseOfferings.Single(c => c.Id == courseOffering.Id);

            courseOfferingInDb.InstructorId = courseOffering.InstructorId;
            _context.SaveChanges();

            return RedirectToAction("Index", "CourseOfferings");
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }
    }
}