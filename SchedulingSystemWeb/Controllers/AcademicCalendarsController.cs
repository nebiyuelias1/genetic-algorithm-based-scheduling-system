using SchedulingSystemClassLibrary;
using SchedulingSystemClassLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SchedulingSystemWeb.Controllers
{
    public class AcademicCalendarsController : Controller
    {
        private SchedulingContext _context;

        public AcademicCalendarsController()
        {
            _context = new SchedulingContext();
        }

        // GET: AcademicCalendars
        public ActionResult Index()
        {
            return View();
        }

        
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }
    }
}