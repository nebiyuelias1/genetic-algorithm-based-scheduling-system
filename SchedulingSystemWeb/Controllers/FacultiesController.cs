using SchedulingSystemClassLibrary;
using SchedulingSystemClassLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SchedulingSystemWeb.Controllers
{
    public class FacultiesController : Controller
    {
        private SchedulingContext _context;

        public FacultiesController()
        {
            _context = new SchedulingContext();
        }
        // GET: Faculties
        public ActionResult Index()
        {
            var faculties = _context.Rooms.ToList(); 
            return View(faculties);
        }
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }
    }
}