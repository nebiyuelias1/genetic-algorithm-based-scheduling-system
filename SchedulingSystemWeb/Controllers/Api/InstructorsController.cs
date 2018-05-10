using SchedulingSystemClassLibrary;
using SchedulingSystemClassLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data.Entity;
using AutoMapper;
using SchedulingSystemClassLibrary.Dtos;

namespace SchedulingSystemWeb.Controllers.Api
{
    public class InstructorsController : ApiController
    {
        private SchedulingContext _context;

        public InstructorsController()
        {
            _context = new SchedulingContext(); 
        }

        // POST - /api/instructors/
        public void SetPreference(InstructorPreference preference)
        {
            var instructorInDb = _context.Instructors.Single(i => i.Id == preference.InstructorId);
            instructorInDb.InstructorPreference = preference;
            _context.SaveChanges();
            instructorInDb.InstructorPreferenceId = preference.Id;
            _context.SaveChanges();

        }

        public IHttpActionResult GetInstructors()
        {
            var instructors = _context.Instructors
                                .Include(i => i.Department)
                                .ToList()
                                .Select(Mapper.Map<Instructor, InstructorDto>);

            return Ok(instructors); 
        }
        public void DeleteInstructor(int id)
        {
            var instructorInDb = _context.Instructors.SingleOrDefault(x => x.Id == id);

            if (instructorInDb == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            _context.Instructors.Remove(instructorInDb);
            _context.SaveChanges();
        }
        protected override void Dispose(bool disposing)
        {
            _context.Dispose(); 
        }
    }
}
