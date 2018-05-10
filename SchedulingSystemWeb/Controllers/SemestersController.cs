using SchedulingSystemClassLibrary;
using SchedulingSystemClassLibrary.Models;
using SchedulingSystemClassLibrary.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SchedulingSystemWeb.Controllers
{
    public class SemestersController : Controller
    {
        private SchedulingContext _context;

        public SemestersController()
        {
            _context = new SchedulingContext();
        }
        // GET: Semesters
        public ActionResult Index()
        {
            var academicYears = _context.AcademicYears.ToList();

            var viewModel = new AcademicSemestersViewModel
            {
                AcademicYears = academicYears
            };

            return View(viewModel);
        }

        public ActionResult NewAcademicYear()
        {
            var viewModel = new AcademicYearsViewModel();
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult SaveYear(AcademicYearsViewModel model)
        {
            if (ModelState.IsValid)
            {
                var academicYear = new AcademicYear
                {
                    StartDate = model.StartDate,
                    EndDate = model.EndDate,
                    EtYear = model.EtYear,
                    GcYear = model.GcYear
                };

                _context.AcademicYears.Add(academicYear);
                _context.SaveChanges();

                return RedirectToAction("Index", "Semesters");
            }

            return View("NewAcademicYear");
        }

        [HttpPost]
        public ActionResult SaveSemester(AcademicSemestersViewModel model)
        {
            if (ModelState.IsValid)
            {
                var academicSemester = new AcademicSemester
                {
                    AcademicYearId = model.AcademicYearId,
                    CurrentSemester = true,
                    Semester = model.Semester,
                    StartDate = model.StartDate,
                    EndDate = model.EndDate
                };

                var previousSemesters = _context.AcademicSemesters.Where(s => s.CurrentSemester);

                foreach (var semester in previousSemesters)
                {
                    semester.CurrentSemester = false; 
                }

                _context.AcademicSemesters.Add(academicSemester);
                _context.SaveChanges();

                return RedirectToAction("Index", "Home");
            }

            return View("Index");
        }
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }
    }
}