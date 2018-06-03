using SchedulingSystemClassLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SchedulingSystemWeb.Controllers.Api
{
    public class LabAssistantsController : ApiController
    {
        private SchedulingContext _context;

        public LabAssistantsController()
        {
            _context = new SchedulingContext();
        }
        [HttpDelete]
        public void DeleteLabAssistants(int id)
        {
            var labAssistantInDb = _context.LabAssistances.SingleOrDefault(x => x.Id == id);

            if (labAssistantInDb == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound); 
            }

            _context.LabAssistances.Remove(labAssistantInDb);
            _context.SaveChanges();
        }

        [HttpGet]
        [Route("api/labassistants/removeassignment/{id}")]
        public void RemoveAssignment(int id)
        {
            var labAssistantInDb = _context.LabAssistances.SingleOrDefault(x => x.Id == id);

            if (labAssistantInDb == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            labAssistantInDb.AssignedLabRoomId = null; 
            _context.SaveChanges();
        }
        protected override void Dispose(bool disposing)
        {
            _context.Dispose(); 
        }
    }
}
