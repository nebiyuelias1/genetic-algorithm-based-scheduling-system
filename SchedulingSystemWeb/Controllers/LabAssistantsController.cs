using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using SchedulingSystemClassLibrary;
using SchedulingSystemClassLibrary.Models;
using SchedulingSystemClassLibrary.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;

namespace SchedulingSystemWeb.Controllers
{
    public class LabAssistantsController : Controller
    {
        private SchedulingContext _context;

        public LabAssistantsController()
        {
            _context = new SchedulingContext();
        }

        // GET: LabAssistants
        public ActionResult Index()
        {
            var model = _context
                       .LabAssistances
                       .Include(l => l.AssignedLabRoom.Building)
                       .ToList();

            if (User.IsInRole(RoleName.IsACollegeDean))
            {
                return View(model);
            }
            else if (User.IsInRole(RoleName.IsADepartmentHead))
            {
                
                return View("DepartmentHeadIndex", model);
            }

            return RedirectToAction("Login", "Account");
        }

        public ActionResult New()
        {
            var viewModel = new LabAssistantsFormViewModel();
            return View("LabAssistantsForm", viewModel); 
        }
        
        public ActionResult Edit(int id)
        {
            var labAssistant = _context.LabAssistances.Single(i => i.Id == id);

            var viewModel = new LabAssistantsFormViewModel(labAssistant);
            return View("LabAssistantsForm", viewModel);
        }

        public ActionResult Save(LabAssistant model)
        {
            if (model.Id == 0)
            {
                _context.LabAssistances.Add(model);
            }
            else
            {
                var labAssistanceInDb = _context.LabAssistances.Single(l => l.Id == model.Id);

                labAssistanceInDb.FirstName = model.FirstName;
                labAssistanceInDb.FatherName = model.FatherName;
                labAssistanceInDb.GrandFatherName = model.GrandFatherName; 

            }

            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult CreateAccount(int id)
        {
            var labAssistant = _context.LabAssistances.Single(s => s.Id == id);

            var viewModel = new CreateLabAssistantAccountViewModel
            {
                LabAssistant = labAssistant,
                LabAssistantId = labAssistant.Id
            };

            return View(viewModel); 
        }

        [HttpPost]
        public async Task<ActionResult> CreateAccount(CreateLabAssistantAccountViewModel model)
        {
            var user = new ApplicationUser { UserName = model.EmailAddress, Email = model.EmailAddress };

            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));

            var result = await userManager.CreateAsync(user);

            if (result.Succeeded)
            {
                var identityResult = userManager.AddToRole(user.Id, RoleName.IsALabAssistant);

                if (identityResult.Succeeded)
                {
                    using (var schedulingContext = new SchedulingContext())
                    {
                        var labAssistantInDb = schedulingContext.LabAssistances.SingleOrDefault(i => i.Id == model.LabAssistantId);

                        if (labAssistantInDb != null)
                        {
                            labAssistantInDb.AccountId = user.Id;
                            schedulingContext.SaveChanges();
                        }
                    }
                }
            }

            if (!result.Succeeded)
            {
                model.Errors = result.Errors;
                
                return View("CreateAccount", model);
            }

            return RedirectToAction("Index");
        }

        public async Task<ActionResult> Account(int id)
        {
            var labAssistanceInDb = _context
                                    .LabAssistances
                                    .Single(s => s.Id == id);

            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));

            var user = await userManager.FindByIdAsync(labAssistanceInDb.AccountId);
       
            return View(user);
        }

        public ActionResult Assign(int id)
        {
            var labAssistantInDb = _context
                                    .LabAssistances
                                    .Single(s => s.Id == id);

            var labRooms = _context
                            .Rooms
                            .Include(s => s.Building)
                            .Where(r => r.IsLabRoom)
                            .ToList();

            var viewModel = new AssignLabAssistantViewModel
            {
                LabAssistant = labAssistantInDb, 
                LabAssistantId = labAssistantInDb.Id, 
                Rooms = labRooms
            };

            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Assign(AssignLabAssistantViewModel model)
        {
            var labAssistanceInDb = _context.LabAssistances.Single(s => s.Id == model.LabAssistantId);

            labAssistanceInDb.AssignedLabRoomId = model.RoomId;

            _context.SaveChanges();

            return RedirectToAction("Index");
        }
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }
    }
}