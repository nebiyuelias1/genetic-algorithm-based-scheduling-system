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
    public class SectionsController : ApiController
    {
        private SchedulingContext _context;

        public SectionsController()
        {
            _context = new SchedulingContext();
        }

        // GET /api/sections 
        public IHttpActionResult GetSections()
        {
            var sections = _context.Sections
                            .Include(s => s.Department)
                            .Include(s => s.AssignedRooms)
                            .Select(s => new
                            {
                                s.Id, 
                                s.Name, 
                                s.EntranceYear, 
                                s.StudentCount, 
                                Department = new { s.Department.Id, s.Department.Name},
                                AssignedRooms = s.AssignedRooms
                            })
                            .ToList();

            return Ok(sections);
        }

        // DELETE /api/sections/1 
        [HttpDelete]
        public void DeleteSection(int id)
        {
            var sectionInDb = _context.Sections.SingleOrDefault(x => x.Id == id);

            if (sectionInDb == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            _context.Sections.Remove(sectionInDb);
            _context.SaveChanges();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }
    }
}
