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

        public IHttpActionResult GetInstructorsInADepartment(int id)
        {
            var instructors = _context
                                .Instructors
                                .Where(i => i.DepartmentId == id)
                                .Select(Mapper.Map<Instructor, InstructorDto>);

            return Ok(instructors);
        }
        protected override void Dispose(bool disposing)
        {
            _context.Dispose(); 
        }
    }
}
