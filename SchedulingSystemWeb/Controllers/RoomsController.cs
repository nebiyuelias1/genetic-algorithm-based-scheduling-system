﻿using SchedulingSystemClassLibrary;
using SchedulingSystemClassLibrary.Models;
using SchedulingSystemClassLibrary.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;

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
            if (User.IsInRole(RoleName.IsACollegeDean))
            {
                var rooms = _context
                        .Rooms
                        .Include(r => r.Building)
                        .ToList();

                return View(rooms);
            }
            else if(User.IsInRole(RoleName.IsADepartmentHead))
            {
                var rooms = _context
                            .Rooms
                            .Include(c => c.Building)
                            .ToList();

                return View("DepartmentHeadIndex", rooms);
            }

            return RedirectToAction("Login", "Account");
        }

        public ActionResult New()
        {
            var buildings = _context.Buildings.ToList();
            var viewModel = new RoomsFormViewModel
            {
                Buildings = buildings
            };
            return View("RoomForm", viewModel);
        }

        public ActionResult Edit(int id)
        {
            var room = _context.Rooms.Single(r => r.Id == id);
            var buildings = _context.Buildings.ToList();
            var viewModel = new RoomsFormViewModel(room)
            {
               Buildings = buildings

            };

            return View("RoomForm", viewModel);
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
                roomInDb.IsSharedRoom = room.IsSharedRoom;
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