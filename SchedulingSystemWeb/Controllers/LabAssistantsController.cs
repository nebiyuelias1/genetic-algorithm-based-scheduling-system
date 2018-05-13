using SchedulingSystemClassLibrary;
using SchedulingSystemClassLibrary.Models;
using SchedulingSystemClassLibrary.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

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
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }
    }
}