using SchedulingSystemClassLibrary;
using SchedulingSystemClassLibrary.Models;
using SchedulingSystemClassLibrary.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
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
        public ActionResult Index()
        {
            
            return View();
        }

        public ActionResult New()
        {
            var departments = _context.Departments.ToList();

            var curriculumViewModel = new CurriculumsFormViewModel()
            {
                Departments = departments
            };
            return View("CurriculumForm", curriculumViewModel);
        }

        public ActionResult Edit(int id)
        {
            var curriculum = _context.Curriculums.SingleOrDefault(c => c.Id == id);
            var departments = _context.Departments.ToList();

            var viewModel = new CurriculumsFormViewModel(curriculum)
            {
                Departments = departments
            };

            return View("CurriculumForm", viewModel);
        }

        [HttpPost]
        public ActionResult Save(Curriculum curriculum)
        {
            if (curriculum.Id == 0)
            {
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