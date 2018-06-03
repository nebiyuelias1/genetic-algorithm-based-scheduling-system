using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using SchedulingSystemClassLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace SchedulingSystemWeb.Controllers.Api
{
    public class UsersController : ApiController
    {

        [HttpDelete]
        [Route("api/users/{userId}")]
        public async Task<IHttpActionResult> DeleteUser(string userId)
        {
            var context = new ApplicationDbContext();
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context)); 

            var user = await userManager.FindByIdAsync(userId);
            bool roleRemoveResult = true;
            var userRoles = user.Roles.ToList();
            foreach (var role in userRoles)
            {
                var roleName = roleManager.FindById(role.RoleId).Name;
                var result = await userManager.RemoveFromRoleAsync(userId, roleName);
                if (!result.Succeeded)
                {
                    roleRemoveResult = false; 
                }
            }

            if (roleRemoveResult)
            {
                context.Users.Remove(user);
                context.SaveChanges();
            }

            return Ok();
        }
    }
}
