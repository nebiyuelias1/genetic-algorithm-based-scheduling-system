using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using SchedulingSystemClassLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SchedulingSystemWeb.Controllers
{
    public class UsersController : Controller
    {

        // GET: Users
        public ActionResult Index()
        {
            using(var context = new ApplicationDbContext())
            {
                var currentUserId = User.Identity.GetUserId();
                var users = context.Users.Where(x => x.Id != currentUserId).ToList();
                return View(users);
            }
        }
    }
}