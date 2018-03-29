using SchedulingSystemClassLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SchedulingSystemWeb.Controllers.Api
{
    public class FacultiesController : ApiController
    {
        private SchedulingContext _context;

        public FacultiesController()
        {
            _context = new SchedulingContext();
        }

        // DELETE /api/faculties/1 
        [HttpDelete]
        public void DeleteFaculty(int id)
        {
            var facultyInDb = _context.Rooms.SingleOrDefault(x => x.Id == id);

            if (facultyInDb == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            _context.Rooms.Remove(facultyInDb);
            _context.SaveChanges();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }
    }
}
