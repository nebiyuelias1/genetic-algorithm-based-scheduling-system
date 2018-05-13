using AutoMapper;
using SchedulingSystemClassLibrary;
using SchedulingSystemClassLibrary.Dtos;
using SchedulingSystemClassLibrary.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SchedulingSystemWeb.Controllers.Api
{
    public class BuildingsController : ApiController
    {
        private SchedulingContext _context;

        public BuildingsController()
        {
            _context = new SchedulingContext();
        }

        // GET - /api/buildings 
        public IHttpActionResult GetBuildings()
        {
            var buildings = _context.Buildings
                            .Include(b => b.Rooms)
                            .Select(Mapper.Map<Building, BuildingDto>)
                            .ToList();

            return Ok(buildings); 
        }

        // DELETE - /api/buildings 
        [HttpDelete]
        public void DeleteBuildings(int id) 
        {
            var buildingInDb = _context
                                .Buildings
                                .Include(b => b.Rooms.Select(r => r.AssignedLectureSections))
                                .Include(b => b.Rooms.Select(r => r.AssignedLabSections))
                                .SingleOrDefault(x => x.Id == id);

            if (buildingInDb == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            
            var rooms = buildingInDb.Rooms;
            buildingInDb.Rooms = null;
            foreach (var room in rooms)
            {
                room.AssignedLabSections = null;
                room.AssignedLectureSections = null;
                _context.Rooms.Remove(room);
            }
            _context.Buildings.Remove(buildingInDb);
            _context.SaveChanges();
        }
        protected override void Dispose(bool disposing)
        {
            _context.Dispose(); 
        }
    }
}
