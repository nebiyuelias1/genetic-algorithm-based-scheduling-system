﻿using SchedulingSystemClassLibrary;
using SchedulingSystemClassLibrary.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using SchedulingSystemClassLibrary.Models;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;

namespace SchedulingSystemWeb.Controllers
{
    public class InstructorsController : Controller
    {
        private SchedulingContext _context;

        public InstructorsController()
        {
            _context = new SchedulingContext();
        }

        // GET: Instructors
        public async Task<ActionResult> Index()
        {
            if (User.IsInRole(RoleName.IsACollegeDean))
            {
                var model = _context
                        .Instructors
                        .Include(i => i.Department)
                        .Include(i => i.CurrentlyAssignedCourses
                        .Select(c => c.Course))
                        .ToList();
                return View(model);
            }
            else
            {
                var userManager = new ApplicationUserManager(new UserStore<ApplicationUser>(new ApplicationDbContext()));
                var user = await userManager.FindByNameAsync(User.Identity.Name);
                var deptHead = user == null ? null : _context.Instructors.Include(i => i.Department).Single(i => i.AccountId == user.Id);

                var model = _context
                        .Instructors
                        .Include(i => i.Department)
                        .Include(i => i.CurrentlyAssignedCourses
                        .Select(c => c.Course))
                        .Where(i => i.DepartmentId == deptHead.DepartmentId)
                        .ToList();

                return View("DepartmentHeadIndex", model);
            }
            
        }

        public ActionResult New()
        {
            var departments = _context.Departments.ToList();

            var viewModel = new InstructorsFormViewModel()
            {
                Departments = departments
            };

            return View("InstructorForm", viewModel);
        }
        public ActionResult Save(Instructor instructor)
        {
            if (instructor.Id == 0)
            {
                _context.Instructors.Add(instructor);
            }
            else
            {
                var instructorInDB = _context.Instructors.Single(c => c.Id == instructor.Id);

                instructorInDB.FirstName = instructor.FirstName;
                instructorInDB.FatherName = instructor.FatherName;
                instructorInDB.GrandFatherName = instructor.GrandFatherName;
                instructorInDB.DepartmentId = instructor.DepartmentId;


            }

            _context.SaveChanges();
                        
            return RedirectToAction("Index", "Instructors");
        }
        
                   

        public ActionResult Edit(int id) 
        {
            var instructor = _context.Instructors.Single(i => i.Id == id);
            var departments = _context.Departments.ToList();

            var viewModel = new InstructorsFormViewModel(instructor)
            {
                Departments=departments
            };

            return View("InstructorForm", viewModel);
        }
        public ActionResult CourseOfferings(int id)
        {
            var courseOfferings = _context
                                .CourseOfferings
                                .Include(c => c.Section.Department)
                                .Include(c => c.Course)
                                .Where(c => c.InstructorId == id)
                                .ToList();

            var viewModel = new InstructorCourseOfferingsViewModel
            {
                CourseOfferings = courseOfferings, 
                InstructorFullName = _context.Instructors.Single(i => i.Id == id).FullName 
            };

            return View(viewModel);
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }
    }
}