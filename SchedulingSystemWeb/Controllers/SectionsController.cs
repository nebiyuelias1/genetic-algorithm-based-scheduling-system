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
            var sections = _context.Sections.ToList(); 

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