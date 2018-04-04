using SchedulingSystemClassLibrary;
using SchedulingSystemClassLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SchedulingSystemWeb.Controllers
{
    public class BuildingsController : Controller
    {
        private SchedulingContext _context;

        public BuildingsController()
        {
            _context = new SchedulingContext(); 
        }
        // GET: Buildings
        public ActionResult Index()
        {
            var buildings = _context.Buildings.ToList(); 

            return View(buildings);
        }

        public ActionResult New()
        {

            return View("BuildingForm");
        }

        public ActionResult Edit(int id)
        {
            var buildingInDb = _context.Buildings.Single(b => b.Id == id); 
            return View("BuildingForm", buildingInDb);
        }

        [HttpPost]
        public ActionResult Save(Building building)
        {
            if (building.Id == 0)
            {
                _context.Buildings.Add(building); 
            }
            else
            {
                var buildingInDb = _context.Buildings.Single(b => b.Id == building.Id);
                buildingInDb.Name = building.Name;
                buildingInDb.Number = building.Number; 


            }

            _context.SaveChanges(); 
            return RedirectToAction("Index", "Buildings");
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose(); 
        }
    }
}