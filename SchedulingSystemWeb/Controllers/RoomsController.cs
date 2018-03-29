using SchedulingSystemClassLibrary;
using SchedulingSystemClassLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SchedulingSystemWeb.Controllers
{
    public class RoomsController : Controller
    {
        private SchedulingContext _context;

        public RoomsController()
        {
            _context = new SchedulingContext(); 
        }
        // GET: Rooms
        public ActionResult Index()
        {
            var rooms = _context.Rooms.ToList(); 

            return View(rooms);
        }

        public ActionResult New()
        {
            var room = new Room(); 
            return View("RoomForm", room);
        }

        public ActionResult Edit(int id)
        {
            var room = _context.Rooms.Single(r => r.Id == id); 

            return View("RoomForm", room);
        }

        [HttpPost]
        public ActionResult Save(Room room)
        {
            if (room.Id == 0)
            {
                _context.Rooms.Add(room); 
            }
            else
            {
                var roomInDb = _context.Rooms.Single(r => r.Id == room.Id);

                roomInDb.Building = room.Building;
                roomInDb.IsLabRoom = room.IsLabRoom;
                roomInDb.IsLectureRoom = room.IsLectureRoom;
                roomInDb.Name = room.Name;
                roomInDb.Size = room.Size; 
            }

            _context.SaveChanges(); 
            return RedirectToAction("Index", "Rooms"); 
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose(); 
        }
    }
}