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

            if (_context.Schedules.Any(s => s.SectionId == id && s.AcademicSemesterId == currentSemester.Id))
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
                .Select(p => p.Room.Building)))
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

        [HttpPost]
        public IHttpActionResult Save(ScheduleDto model)
        {
            try
            {
                var currentSemester = _context.AcademicSemesters.SingleOrDefault(s => s.CurrentSemester);
                var schedule = Mapper.Map<ScheduleDto, Schedule>(model);
                foreach (var day in schedule.Days)
                {
                    foreach (var period in day.Periods)
                    {
                        if (period.Course.Title == null)
                        {
                            day.Periods[period.Period].Course = null;
                            day.Periods[period.Period].Instructor = null;
                            day.Periods[period.Period].Room = null;
                        }
                        else
                        {
                            day.Periods[period.Period].CourseId = day.Periods[period.Period].Course.Id; 
                            day.Periods[period.Period].Course = null;
                            day.Periods[period.Period].InstructorId = day.Periods[period.Period].Instructor.Id;
                            day.Periods[period.Period].Instructor = null;
                            day.Periods[period.Period].RoomId = day.Periods[period.Period].Room.Id;
                            day.Periods[period.Period].Room = null; 
                        }
                    }
                }
                schedule.SectionId = schedule.Section.Id;
                schedule.Section = null;
                schedule.AcademicSemesterId = currentSemester.Id; 
                _context.Schedules.Add(schedule);
                _context.SaveChanges();
                return Ok();
            }
            catch (Exception ex)
            {

                throw ex;
            }
            
        }
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

    }
}
