using SchedulingSystemClassLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity; 

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
        public ActionResult Index()
        {
            return View();
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