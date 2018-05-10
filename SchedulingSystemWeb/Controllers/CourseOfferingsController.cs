using SchedulingSystemClassLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using AutoMapper;
using SchedulingSystemClassLibrary.Models;
using SchedulingSystemClassLibrary.ViewModels;

namespace SchedulingSystemWeb.Controllers
{
    public class CourseOfferingsController : Controller
    {
        private SchedulingContext _context;

        public CourseOfferingsController()
        {
            _context = new SchedulingContext(); 
        }
        // GET: CourseOfferings
        public ActionResult Index()
        {
            return View();
        }
       /* public ActionResult New()
        {
            var instructors = _context.Instructors.ToList();
            var viewModel = new CourseOfferingsFormViewModel
            {
                Instructors = instructors
            };
            return View("CourseOfferingForm", viewModel);
        }*/
        public ActionResult Assign(int id)
        {
            var courseOffering = _context.CourseOfferings
                                        .Include(c => c.Section)
                                        .Include(c => c.Instructor)
                                        .Include(c => c.Course).Single(c => c.Id == id);
            // TODO - This is where the instructor allotment would happen based on interest, previous background, etc. 

            var viewModel = Mapper.Map<CourseOffering, CourseOfferingsFormViewModel>(courseOffering);
            
            var instructors = _context.Instructors.ToList();

            viewModel.Instructors = instructors; 

            return View(viewModel);

        }

        [HttpPost]
        public ActionResult Save(CourseOffering courseOffering)
        {
            var courseOfferingInDb = _context.CourseOfferings.Single(c => c.Id == courseOffering.Id);

            courseOfferingInDb.InstructorId = courseOffering.InstructorId;
            _context.SaveChanges();

            return RedirectToAction("Index", "CourseOfferings");
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }
    }
}