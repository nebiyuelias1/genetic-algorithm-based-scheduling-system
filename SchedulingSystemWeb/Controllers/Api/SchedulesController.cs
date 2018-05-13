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
using SchedulingSystemClassLibrary.GeneticAlgorithm;

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

        [System.Web.Http.Route("api/schedules/sections/{id}")]
        public IHttpActionResult GetSchedule(int id)
        {
            var currentSemester = _context
                                   .AcademicSemesters
                                   .SingleOrDefault(s => s.CurrentSemester);

            if (_context.Schedules.Any(s => s.Id == id && s.AcademicSemesterId == currentSemester.Id))
            {
                var schedule = _context.Schedules
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
                .Select(Mapper.Map<Schedule, ScheduleDto>)
                .SingleOrDefault(s => s.Section.Id == id);

                return Ok(schedule);
            }
            else
            {
                return NotFound(); 
            }
        }

        [System.Web.Http.Route("api/schedules/generate/{id}")]
        [System.Web.Http.HttpGet]
        public IHttpActionResult GenerateSchedule(int id)
        {
            var algorithm = new GeneticAlgorithm(id);
            var best = algorithm.FindBestSchedule();
            var scheduleDto = Mapper.Map<Schedule, ScheduleDto>(best);

            return Ok(scheduleDto);
        }

        public IHttpActionResult GetScheduleForADepartment(int id)
        {
            var schedules = _context
                            .Schedules
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
                            .Where(s => s.Section.DepartmentId == id)
                            .Select(Mapper.Map<Schedule, ScheduleDto>); 
                            
            return Ok(schedules);
        }
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

    }
}
