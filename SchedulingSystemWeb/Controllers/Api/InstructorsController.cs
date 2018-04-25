using SchedulingSystemClassLibrary;
using SchedulingSystemClassLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SchedulingSystemWeb.Controllers.Api
{
    public class InstructorsController : ApiController
    {
        private SchedulingContext _context;

        public InstructorsController()
        {
            _context = new SchedulingContext(); 
        }

        public void SetPreference(InstructorPreference preference)
        {
            var instructorInDb = _context.Instructors.Single(i => i.Id == preference.InstructorId);
            instructorInDb.InstructorPreference = preference;
            _context.SaveChanges();
            instructorInDb.InstructorPreferenceId = preference.Id;
            _context.SaveChanges();

        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose(); 
        }
    }
}
