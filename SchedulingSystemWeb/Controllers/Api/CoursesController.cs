using SchedulingSystemClassLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SchedulingSystemWeb.Controllers.Api
{
    public class CoursesController : ApiController
    {
        private SchedulingContext _context;

        public CoursesController()
        {
            _context = new SchedulingContext(); 
        }

        // DELETE /api/courses/1 
        [HttpDelete]
        public void DeleteCourse(int id)
        {
            var courseInDb = _context.Courses.SingleOrDefault(x => x.Id == id);

            if (courseInDb == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            _context.Courses.Remove(courseInDb);
            _context.SaveChanges();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose(); 
        }
    }
}
