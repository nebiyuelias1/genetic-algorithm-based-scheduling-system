using SchedulingSystemClassLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data.Entity;
using AutoMapper;
using SchedulingSystemClassLibrary.Models;
using SchedulingSystemClassLibrary.Dtos;

namespace SchedulingSystemWeb.Controllers.Api
{
    public class DepartmentsController : ApiController
    {
        private SchedulingContext _context;

        public DepartmentsController()
        {
            _context = new SchedulingContext();
        }

        public IHttpActionResult GetDepartments()
        {
            var departments = _context.Departments
                                .Include(d => d.Sections)
                                .Include(d => d.Instructors)
                                .Include(d => d.DepartmentHead)
                                .Select(Mapper.Map<Department, DepartmentDto>);

            return Ok(departments);
        }

        // DELETE /api/departments/1 
        [HttpDelete]
        public void DeleteDepartment(int id)
        {
            var departmentInDb = _context.Departments.SingleOrDefault(x => x.Id == id);

            if (departmentInDb == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            _context.Departments.Remove(departmentInDb);
            _context.SaveChanges();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }
    }
}
