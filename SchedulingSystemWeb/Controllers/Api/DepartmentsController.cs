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
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity;
using System.Web.Security;
using System.Threading.Tasks;

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

        [HttpPost]
        public async Task<IHttpActionResult> ChangeDepartmentHead(DepartmentHeadChangeDto dto)
        {
            if (ModelState.IsValid)
            {
                // Find the department in the database with the given id 
                var departmentInDb = _context
                                    .Departments
                                    .Include(d => d.DepartmentHead)
                                    .Single(d => d.Id == dto.DepartmentId);


                // to use the user manager
                var userStore = new UserStore<ApplicationUser>(new ApplicationDbContext());
                var userManager = new ApplicationUserManager(userStore);
                
                // make sure the department has a head assigned to it already
                if (departmentInDb.DepartmentHead != null)
                {
                    // the old department head who is about to be replaced
                    var oldDepartmentHead = departmentInDb.DepartmentHead;

                    // remove the old department head from the department head role
                    var result = await userManager.RemoveFromRoleAsync(oldDepartmentHead.AccountId, RoleName.IsADepartmentHead);
                }
                
                // update department head to the new instructor 
                departmentInDb.DepartmentHeadId = dto.InstructorId;

                // find the instructor from the db with the id
                var instructor = _context.Instructors.Single(i => i.Id == dto.InstructorId);

                // add the new department head to the department head role
                await userManager.AddToRoleAsync(instructor.AccountId, RoleName.IsADepartmentHead);
                _context.SaveChanges();
            }
            return Ok();
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
