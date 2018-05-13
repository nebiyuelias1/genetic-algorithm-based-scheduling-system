using SchedulingSystemClassLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using SchedulingSystemClassLibrary.Models;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;
using SchedulingSystemClassLibrary.GeneticAlgorithm;
using Newtonsoft.Json;
using AutoMapper;
using SchedulingSystemClassLibrary.Dtos;
using SchedulingSystemClassLibrary.ViewModels.DepartmentHead;

namespace SchedulingSystemWeb.Controllers
{
    public class SchedulesController : Controller
    {
        private SchedulingContext _context;

        public SchedulesController()
        {
            _context = new SchedulingContext();
        }
        // GET: Schedules
        public async Task<ActionResult> Index()
        {
            if (User.IsInRole(RoleName.IsACollegeDean))
            {

            }
            else if (User.IsInRole(RoleName.IsADepartmentHead))
            {
                var userManager = new ApplicationUserManager(new UserStore<ApplicationUser>(new ApplicationDbContext()));
                var user = await userManager.FindByNameAsync(User.Identity.Name);
                var deptId = _context.Instructors.SingleOrDefault(i => i.AccountId == user.Id).DepartmentId;

                var sections = _context
                                .Sections
                                .Include(s => s.Department)
                                .Where(s => s.DepartmentId == deptId)
                                .OrderByDescending(s => s.EntranceYear)
                                .ToList();

                return View("DepartmentHeadIndex", sections);
            }

            return RedirectToAction("Login", "Account");
        }

        public ActionResult Generate(int id)
        {
            var section = _context
                            .Sections
                            .Include(s => s.Department)
                            .Include(s => s.AssignedLectureRoom
                            .Building)
                            .Include(s => s.AssignedLabRoom
                            .Building)
                            .Include(s => s.CourseOfferings
                            .Select(c => c.Instructor))
                            .Include(s => s.CourseOfferings
                            .Select(c => c.Course))
                            .Single(s => s.Id == id);

            var semester = _context
                            .AcademicSemesters
                            .Include(s => s.AcademicYear)
                            .Single(s => s.CurrentSemester);
                            
            var viewModel = new ViewScheduleViewModel
            {
                Section = section, 
                AcademicSemester = semester
            };

            return View("ViewSchedule", viewModel); 
        }
        public ActionResult Departments()
        {
            return View();
        }

        public ActionResult Sections(int id)
        {
            var sections = _context.Sections.Where(s => s.DepartmentId == id);

            return View(sections);
        }

        public ActionResult ByDepartment(int id)
        {
            var department = _context
                            .Departments
                            .Include(d => d.Sections)
                            .Single(d => d.Id == id);


            return View("ScheduleByDepartment",department);
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }
    }
}