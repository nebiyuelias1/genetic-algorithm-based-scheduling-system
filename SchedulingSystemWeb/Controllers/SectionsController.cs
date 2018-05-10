using SchedulingSystemClassLibrary;
using SchedulingSystemClassLibrary.Models;
using SchedulingSystemClassLibrary.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;
using SchedulingSystemClassLibrary.ViewModels.DepartmentHead;

namespace SchedulingSystemWeb.Controllers
{
    public class SectionsController : Controller
    {
        private SchedulingContext _context;
        public SectionsController()
        {
            _context = new SchedulingContext(); 
        }
        // GET: Sections
        public async Task<ActionResult> Index()
        {
            if (User.IsInRole(RoleName.IsACollegeDean))
            {
                var sections = _context
                                .Sections
                                .Include(s => s.Department)
                                .Include(s => s.AssignedLectureRoom)
                                .Include(s => s.AssignedLabRoom)
                                .ToList();

                return View(sections);
            }
            else
            {
                var userManager = new ApplicationUserManager(new UserStore<ApplicationUser>(new ApplicationDbContext()));
                var user = await userManager.FindByNameAsync(User.Identity.Name);

                var deptHead = user == null ? null : _context.Instructors.Include(i => i.Department).Single(i => i.AccountId == user.Id);

                var sections = _context
                                .Sections
                                .Include(s => s.Department)
                                .Include(s => s.AssignedLectureRoom)
                                .Include(s => s.AssignedLabRoom)
                                .Where(s => s.DepartmentId == deptHead.DepartmentId)
                                .ToList();

                return View("DepartmentHeadIndex", sections);
            }
            
        }

        public async Task<ActionResult> New()
        {
            

            if (User.IsInRole(RoleName.IsACollegeDean))
            {
                var departments = _context.Departments.ToList();

                var viewModel = new SectionsFormViewModel
                {
                    Departments = departments
                };

                return View("SectionForm", viewModel);
            }
            else
            {
                var userManager = new ApplicationUserManager(new UserStore<ApplicationUser>(new ApplicationDbContext()));
                var user = await userManager.FindByNameAsync(User.Identity.Name);
                var deptHead = user == null ? null : _context.Instructors.Include(i => i.Department).Single(i => i.AccountId == user.Id);

                var viewModel = new SectionsFormViewModel
                {
                    DepartmentId = deptHead.DepartmentId
                };

                return View("DepartmentHeadSectionForm", viewModel);
            }
        }

        public async Task<ActionResult> Edit(int id)
        {
            var section = _context.Sections.Single(s => s.Id == id);

            if (User.IsInRole(RoleName.IsACollegeDean))
            {
                var departments = _context.Departments.ToList();

                var viewModel = new SectionsFormViewModel(section)
                {
                    Departments = departments
                };

                return View("SectionForm", viewModel);
            }
            else
            {
                var userManager = new ApplicationUserManager(new UserStore<ApplicationUser>(new ApplicationDbContext()));
                var user = await userManager.FindByNameAsync(User.Identity.Name);
                var deptHead = user == null ? null : _context.Instructors.Include(i => i.Department).Single(i => i.AccountId == user.Id);

                var viewModel = new SectionsFormViewModel(section)
                {
                    DepartmentId = deptHead.DepartmentId
                };

                return View("DepartmentHeadSectionForm", viewModel);
            }
            
        }

        [HttpPost]
        public ActionResult Save(Section section)
        {
            if (section.Id == 0)
            {
                _context.Sections.Add(section); 
            }
            else
            {
                var sectionInDb = _context.Sections.Single(s => s.Id == section.Id);

                sectionInDb.DepartmentId = section.DepartmentId;
                sectionInDb.EntranceYear = section.EntranceYear;
                sectionInDb.Name = section.Name;
                sectionInDb.StudentCount = section.StudentCount; 

            }

            _context.SaveChanges();

            return RedirectToAction("Index", "Sections"); 
        }

        public ActionResult CourseOfferings(int id)
        {
            var section = _context
                        .Sections
                        .Include(s => s.CourseOfferings
                        .Select(c => c.Course))
                        .Include(s => s.CourseOfferings
                        .Select(c => c.Instructor))
                        .Single(s => s.Id == id); 
                        
            return View(section);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Assign(AssignRoomViewModel model)
        {
            if (ModelState.IsValid)
            {
                var sectionInDb = _context.Sections.Single(s => s.Id == model.SectionId);

                if (model.LectureRoomId > 0)
                {
                    sectionInDb.AssignedLectureRoomId = model.LectureRoomId;
                }
                else if (model.LabRoomId > 0)
                {
                    sectionInDb.AssignedLabRoomId = model.LabRoomId;
                }

                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        public ActionResult AssignLectureRoom(int id)
        {
            var section = _context.Sections.Single(s => s.Id == id);
            var rooms = _context.Rooms.Where(r => r.IsLectureRoom);
            var viewModel = new AssignRoomViewModel
            {
                SectionId = id, 
                Section = section, 
                Rooms = rooms
            };

            return View(viewModel);
        }
        protected override void Dispose(bool disposing)
        {
            _context.Dispose(); 
        }
    }
}