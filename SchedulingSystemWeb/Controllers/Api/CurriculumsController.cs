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
    public class CurriculumsController : ApiController
    {
        private SchedulingContext _context;

        public CurriculumsController()
        {
            _context = new SchedulingContext();
        }

        // GET /api/curriculums
        public IHttpActionResult GetCurriculums()
        {
            var curriculums = _context.Curriculums
                .Include(c => c.Department)
                .ToList();

            return Ok(curriculums);
        }

        // DELETE /api/curriculums/1 
        [HttpDelete]
        public void DeleteCurriculum(int id)
        {
            var curriculumInDb = _context.Curriculums.SingleOrDefault(x => x.Id == id);

            if (curriculumInDb == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            _context.Curriculums.Remove(curriculumInDb);
            _context.SaveChanges();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose(); 
        }
    }
}
