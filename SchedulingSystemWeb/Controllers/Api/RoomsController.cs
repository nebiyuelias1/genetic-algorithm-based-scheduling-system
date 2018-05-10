using SchedulingSystemClassLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data.Entity; 

namespace SchedulingSystemWeb.Controllers.Api
{
    public class RoomsController : ApiController
    {
        private SchedulingContext _context;

        public RoomsController()
        {
            _context = new SchedulingContext();
        }

        // GET /api/rooms
        public IHttpActionResult GetRooms()
        {
            var rooms = _context.Rooms
                .ToList();

            return Ok(rooms);
        }

        // DELETE /api/rooms/1 
        [HttpDelete]
        public void DeleteRoom(int id)
        {
            var roomInDb = _context.Rooms
            .Include(r => r.AssignedLabSections)
            .Include(r => r.AssignedLectureSections)
            .SingleOrDefault(x => x.Id == id);

            if (roomInDb == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            roomInDb.AssignedLabSections = null;
            roomInDb.AssignedLectureSections = null; 

            _context.Rooms.Remove(roomInDb);
            _context.SaveChanges();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }
    }
}
