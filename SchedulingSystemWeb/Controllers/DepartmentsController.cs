using SchedulingSystemClassLibrary;
using SchedulingSystemClassLibrary.Models;
using SchedulingSystemClassLibrary.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using System.Web.Security;

namespace SchedulingSystemWeb.Controllers
{
    public class DepartmentsController : Controller
    {
        private SchedulingContext _context;

        public DepartmentsController()
        {
            _context = new SchedulingContext();
        }
        // GET: Departments
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult New()
        {
            var viewModel = new DepartmentsFormViewModel
            {
                Faculties = _context.Faculties.ToList()
            };

            return View("DepartmentForm", viewModel);
        }

        public ActionResult Edit(int id)
        {
            var faculties = _context.Faculties.ToList();
            var departmentInDb = _context.Departments.Single(d => d.Id == id);

            var viewModel = new DepartmentsFormViewModel(departmentInDb);
            viewModel.Faculties = faculties;

            return View("DepartmentForm", viewModel);
        }

        [HttpPost]
        public ActionResult Save(Department department)
        {
            if (ModelState.IsValid)
            {
                if (department.Id == 0)
                {
                    _context.Departments.Add(department);
                }
                else
                {
                    var deparmentInDb = _context.Departments.Single(d => d.Id == department.Id);

                    deparmentInDb.Name = department.Name;
                    deparmentInDb.Email = department.Email;
                    deparmentInDb.FacultyId = department.FacultyId;
                    deparmentInDb.Fax = department.Fax;
                    deparmentInDb.Phone = department.Phone; 
                }

                _context.SaveChanges();
                return RedirectToAction("Index", "Departments");
            }
            return View();
        }

        public ActionResult Instructors(int id)
        {
            var instructors = _context
                            .Instructors
                            .Include(i => i.InstructorPreference)
                            .Include(i => i.CurrentlyAssignedCourses
                            .Select(c => c.Course))
                            .Where(i => i.DepartmentId == id).ToList();

            var viewModel = new DepartmentInstructorsViewModel
            {
                DepartmentName = _context.Departments.Single(d => d.Id == id).Name, 
                Instructors = instructors
            };

            return View(viewModel);
        }

        public ActionResult Sections(int id)
        {
            var sections = _context.Sections
                        .Include(s => s.AssignedLabRoom)
                        .Include(s => s.AssignedLectureRoom)
                        .Include(s => s.CourseOfferings)
                        .Where(s => s.DepartmentId == id)
                        .ToList();

            var departmentName = _context.Departments.Single(d => d.Id == id).Name;

            var viewModel = new DepartmentSectionsViewModel
            {
                Sections = sections,
                DepartmentName = departmentName
            };

            return View(viewModel);
        }

        public ActionResult AssignHead(int id)
        {
            var department = _context
                .Departments
                .Include(d => d.Instructors)
                .Single(d => d.Id == id);

            var viewModel = new AssignDepartmentHeadViewModel
            {
                Id = department.Id,
                DepartmentHeadId = department.DepartmentHeadId,
                Instructors = department.Instructors,
                Name = department.Name
            };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateAccount(CreateDepartmentHeadAccountViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Find the department in the database and update it's departmentheadid field
                var departmentInDb = _context.Departments.Single(d => d.Id == model.DepartmentId);
                departmentInDb.DepartmentHeadId = model.InstructorId;

                // Creating a new application user using the provided email
                var userStore = new UserStore<ApplicationUser>(new ApplicationDbContext());
                var userManager = new ApplicationUserManager(userStore);
                var user = new ApplicationUser { UserName = model.Email,Email = model.Email }; 
                var result = await userManager.CreateAsync(user);

                if (result.Succeeded)
                {
                    // Add the user to department head role 
                    var addToInstructorRoleResult = await userManager.AddToRoleAsync(user.Id, RoleName.IsAnInstructor);
                    var addToDepartmentHeadRoleResult = await userManager.AddToRoleAsync(user.Id, RoleName.IsADepartmentHead);

                    if (addToDepartmentHeadRoleResult.Succeeded && addToInstructorRoleResult.Succeeded)
                    {
                        // Update the department head's account id field with the newly created account
                        var departmentHead = _context.Instructors.Single(i => i.Id == model.InstructorId);
                        departmentHead.AccountId = user.Id;

                        _context.SaveChanges();
                    }

                    return View("DepartmentHeadAccountCreated");
                }
                
                
                model.Errors = result.Errors;

                
                
            }

            return View("NewAccount", model);
        }

        public ActionResult ChangeHead(int id)
        {
            var department = _context
                            .Departments
                            .Include(d => d.DepartmentHead)
                            .Include(d => d.Instructors)
                            .Single(d => d.Id == id);

            var viewModel = new ChangeDepartmentHeadViewModel
            {
                OldDepartmentHead = department.DepartmentHead,
                OldDepartmentHeadAccountId = department.DepartmentHead.AccountId,
                DepartmentHeadId = department.DepartmentHeadId, 
                Instructors = department.Instructors, 
                Name = department.Name
            };
            
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ChangeHead(ChangeDepartmentHeadViewModel model)
        {
            if (ModelState.IsValid)
            {
                var departmentInDb = _context.Departments.Single(d => d.Id == model.Id);
                

                // find the old department head and remove him from the department head role
                var userStore = new UserStore<ApplicationUser>(new ApplicationDbContext());
                var userManager = new ApplicationUserManager(userStore);

                var result = await userManager.RemoveFromRoleAsync(model.OldDepartmentHeadAccountId, RoleName.IsADepartmentHead); 
                if (result.Succeeded)
                {
                    departmentInDb.DepartmentHeadId = model.DepartmentHeadId;
                }
            }

            return View();
        }
        public ActionResult Account()
        {
            var instructorsWithAccounts = _context
                                        .Instructors
                                        .Include(i => i.Department)
                                        .Where(i => i.AccountId != null && i.Id != i.Department.DepartmentHeadId);

            var viewModel = new DepartmentHeadAccountsViewModel
            {
                Instructors = instructorsWithAccounts.ToList()
            };

            return View(viewModel);
        }

        public ActionResult NewAccount()
        {
            var departments = _context.Departments.ToList();
            var viewModel = new CreateDepartmentHeadAccountViewModel
            {
                Departments = departments
            };

            return View(viewModel);
        }
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }
    }
}