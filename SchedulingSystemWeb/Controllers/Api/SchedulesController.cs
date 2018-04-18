using AutoMapper;
using SchedulingSystemClassLibrary;
using SchedulingSystemClassLibrary.Dtos;
using SchedulingSystemClassLibrary.Models;
using System.Data.Entity; 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;

namespace SchedulingSystemWeb.Controllers.Api
{
    public class SchedulesController : ApiController
    {
        private SchedulingContext _context;

        public SchedulesController()
        {
            _context = new SchedulingContext();
        }

        public IHttpActionResult GetSchedules()
        {
            
            var schedules = _context.Schedules
                .Include(s => s.Section)
                .Include(s => s.Days
                .Select(d => d.Periods
                .Select(p => p.Course)))
                .Include(s => s.Days
                .Select(d => d.Periods
                .Select(p => p.Instructor)))
                .Include(s => s.Days
                .Select(d => d.Periods
                .Select(p => p.Room)))
                .ToList()
                .Select(Mapper.Map<Schedule, ScheduleDto>); 

            return Ok(schedules);
        }
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

    }
}
