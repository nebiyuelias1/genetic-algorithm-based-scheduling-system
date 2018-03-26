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
            var curriculums = _context.Curriculums.Include(c => c.Department).ToList(); 
            return View(curriculums);
        }

        public ActionResult New()
        {
            var departments = _context.Departments.ToList();

            var curriculumViewModel = new CurriculumsFormViewModel
            {
                Departments = departments
            };
            return View("CurriculumForm", curriculumViewModel);
        }
    }
}