using AutoMapper;
using SchedulingSystemClassLibrary;
using SchedulingSystemClassLibrary.Dtos;
using SchedulingSystemClassLibrary.Models;
using System.Data.Entity; 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SchedulingSystemWeb.Controllers.Api
{
    public class CourseOfferingsController : ApiController
    {
        private SchedulingContext _context;

        public CourseOfferingsController()
        {
            _context = new SchedulingContext(); 
        }

        // GET - /api/courseofferings
        public IHttpActionResult GetCourseOfferings()
        {
            var currentSemester = _context.AcademicSemesters.SingleOrDefault(s => s.CurrentSemester);
            
            var courseOfferings = _context.CourseOfferings
                                    .Include(c => c.Course)
                                    .Include(c => c.Instructor)
                                    .Include(c => c.Section.Department)
                                    .Include(c => c.AcademicSemester.AcademicYear)
                                    .Where(c => c.AcademicSemester.CurrentSemester)
                                    .ToList()
                                    .Select(Mapper.Map<CourseOffering, CourseOfferingDto>);

            return Ok(courseOfferings); 
        }

        // GET - /api/courseofferings/id
        public IHttpActionResult GetCourseOfferingsForDepartment(int id)
        {
            var courseOfferings = _context.CourseOfferings
                                    .Include(c => c.Course.Curriculum)
                                    .Include(c => c.Instructor)
                                    .Include(c => c.Section.Department)
                                    .Include(c => c.AcademicSemester.AcademicYear)
                                    .Where(c => c.AcademicSemester.CurrentSemester && c.Course.Curriculum.DepartmentId == id)
                                    .ToList()
                                    .Select(Mapper.Map<CourseOffering, CourseOfferingDto>);

            return Ok(courseOfferings);
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose(); 
        }
    }
}
