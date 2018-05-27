using SchedulingSystemClassLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SchedulingSystemWeb.Controllers.Api
{
    public class LabGroupsController : ApiController
    {
        private SchedulingContext _context;

        public LabGroupsController()
        {
            _context = new SchedulingContext();
        }

        [HttpDelete]
        public void DeleteSection(int id)
        {
            var labGroupInDb = _context.LabGroups.SingleOrDefault(g => g.Id == id);

            if (labGroupInDb != null)
            {
                _context.LabGroups.Remove(labGroupInDb);
                _context.SaveChanges();
            }
        }
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }
    }
}
