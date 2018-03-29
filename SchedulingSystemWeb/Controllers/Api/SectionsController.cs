using SchedulingSystemClassLibrary;
using System;
using System.Collections.Generic;
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
