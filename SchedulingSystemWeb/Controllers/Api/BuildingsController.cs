using SchedulingSystemClassLibrary;
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
                            .Select(b => new
                            {
                                Id = b.Id, 
                                Name = b.Name, 
                                Number = b.Number,
                                Rooms = b.Rooms
                            })
                            .ToList();

            return Ok(buildings); 
        }
        protected override void Dispose(bool disposing)
        {
            _context.Dispose(); 
        }
    }
}
