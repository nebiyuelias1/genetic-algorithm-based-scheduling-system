using Microsoft.AspNet.Identity.EntityFramework;
using SchedulingSystemClassLibrary;
using SchedulingSystemClassLibrary.Models;
using SchedulingSystemClassLibrary.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace SchedulingSystemWeb.Controllers
{
    public class CurriculumsController : Controller
    {
        private SchedulingContext _context;
        public CurriculumsController()
        {
            _context = new SchedulingContext(); 
        }
        // GET: Curriculums
        public async Task<ActionResult> Index()
        {
            var userManager = new ApplicationUserManager(new UserStore<ApplicationUser>(new ApplicationDbContext()));
            var user = await userManager.FindByNameAsync(User.Identity.Name);

           
            var deptHead = user == null ? null : _context.Instructors.Include(i => i.Department).Single(i => i.AccountId == user.Id);

            var viewModel = new CurriculumsIndexViewModel
            {
                DepartmentName = deptHead == null ? null : deptHead.Department.Name, 
                CurriculumsCount = deptHead == null ? 0 : _context.Curriculums.Where(c => c.DepartmentId == deptHead.DepartmentId).Count(),
                DepartmentId = deptHead == null ? 0 : deptHead.DepartmentId
            };

            return View(viewModel);
        }

        public ActionResult New()
        {
            var departments = _context.Departments.ToList();

            var curriculumViewModel = new CurriculumsFormViewModel()
            {
            };
            return View("CurriculumForm", curriculumViewModel);
        }

        public ActionResult Edit(int id)
        {
            var curriculum = _context.Curriculums.SingleOrDefault(c => c.Id == id);
            var departments = _context.Departments.ToList();

            var viewModel = new CurriculumsFormViewModel(curriculum)
            {
            };

            return View("CurriculumForm", viewModel);
        }

        [HttpPost]
        public async Task<ActionResult> Save(Curriculum curriculum)
        {
            var userManager = new ApplicationUserManager(new UserStore<ApplicationUser>(new ApplicationDbContext()));
            var user = await userManager.FindByNameAsync(User.Identity.Name);

            var deptHead = user == null ? null : _context.Instructors.Include(i => i.Department).Single(i => i.AccountId == user.Id);

            if (curriculum.Id == 0)
            {
                curriculum.DepartmentId = deptHead.DepartmentId;
                _context.Curriculums.Add(curriculum); 
            }
            else
            {
                var curriculumInDb = _context.Curriculums.SingleOrDefault(c => c.Id == curriculum.Id); 

                curriculumInDb.AdmissionClassification = curriculum.AdmissionClassification;
                curriculumInDb.DepartmentId = curriculum.DepartmentId;
                curriculumInDb.FieldOfStudy = curriculum.FieldOfStudy;
                curriculumInDb.MinimumCredit = curriculum.MinimumCredit;
                curriculumInDb.Program = curriculum.Program;
                curriculumInDb.StaySemester = curriculum.StaySemester;
                curriculumInDb.StayYear = curriculum.StayYear;
                curriculumInDb.Nomenclature = curriculum.Nomenclature;
                curriculumInDb.DepartmentId = deptHead.DepartmentId;
            }

            _context.SaveChanges();
            return RedirectToAction("Index", "Curriculums"); 
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }
    }
}