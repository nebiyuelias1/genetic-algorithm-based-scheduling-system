using SchedulingSystemClassLibrary;
using SchedulingSystemClassLibrary.Models;
using SchedulingSystemClassLibrary.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity; 

namespace SchedulingSystemWeb.Controllers
{
    public class SectionsController : Controller
    {
        private SchedulingContext _context;
        public SectionsController()
        {
            _context = new SchedulingContext(); 
        }
        // GET: Sections
        public ActionResult Index()
        {
            var sections = _context
                        .Sections
                        .Include(s => s.Department)
                        .Include(s => s.AssignedLectureRoom)
                        .Include(s => s.AssignedLabRoom)
                        .ToList(); 

            return View(sections);
        }

        public ActionResult New()
        {
            var departments = _context.Departments.ToList(); 

            var viewModel = new SectionsFormViewModel
            {
                Departments = departments
            };
            return View("SectionForm", viewModel);
        }

        public ActionResult Edit(int id)
        {
            var section = _context.Sections.Single(s => s.Id == id); 

            var departments = _context.Departments.ToList();

            var viewModel = new SectionsFormViewModel(section)
            {
                Departments = departments
            };

            return View("SectionForm", viewModel);
        }

        [HttpPost]
        public ActionResult Save(Section section)
        {
            if (section.Id == 0)
            {
                _context.Sections.Add(section); 
            }
            else
            {
                var sectionInDb = _context.Sections.Single(s => s.Id == section.Id);

                sectionInDb.DepartmentId = section.DepartmentId;
                sectionInDb.EntranceYear = section.EntranceYear;
                sectionInDb.Name = section.Name;
                sectionInDb.StudentCount = section.StudentCount; 

            }

            _context.SaveChanges();

            return RedirectToAction("Index", "Sections"); 
        }

        public ActionResult CourseOfferings(int id)
        {
            var section = _context
                        .Sections
                        .Include(s => s.CourseOfferings
                        .Select(c => c.Course))
                        .Include(s => s.CourseOfferings
                        .Select(c => c.Instructor))
                        .Single(s => s.Id == id); 
                        
            return View(section);
        }
        public ActionResult Assign()
        {
            return View();
        }
        protected override void Dispose(bool disposing)
        {
            _context.Dispose(); 
        }
    }
}