using SchedulingSystemClassLibrary;
using SchedulingSystemClassLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SchedulingSystemWeb.Controllers.Api
{
    public class AcademicCalendarsController : ApiController
    {
        private SchedulingContext _context;

        public AcademicCalendarsController()
        {
            _context = new SchedulingContext();
        }

        // GET /api/AcademicCalendars
        public IHttpActionResult GetCalendarEvents()
        {
            var calendarEvents = _context.AcademicEvents.ToList();


            return Ok(calendarEvents);
        }

        [HttpPost]
        public IHttpActionResult Save(AcademicEvent model)
        {
            if (ModelState.IsValid)
            {
                if (model.Id == 0)
                {
                    model = _context.AcademicEvents.Add(model);
                }
                else
                {
                    var eventInDb = _context.AcademicEvents.SingleOrDefault(e => e.Id == model.Id);
                    eventInDb.Subject = model.Subject;
                    eventInDb.Description = model.Description;
                    eventInDb.Start = model.Start;
                    eventInDb.End = model.End;
                    eventInDb.Color = model.Color;
                }

                _context.SaveChanges();
            }

            return Ok(model);
        }

        [HttpDelete]
        public void DeleteCalendarEvent(int id)
        {
            var eventInDb = _context.AcademicEvents.SingleOrDefault(e => e.Id == id);

            if (eventInDb != null)
            {
                _context.AcademicEvents.Remove(eventInDb);
                _context.SaveChanges();
            }
        }
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }
    }
}
