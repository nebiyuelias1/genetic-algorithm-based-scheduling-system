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
                .Include(x => x.Days
                .Select(d => d.Periods
                .Select(p => p.LabGroup)))
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

            while (best == null)
            {
                algorithm = new GeneticAlgorithm(id);
                best = algorithm.FindBestSchedule();
            }
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
                            .Select(p => p.Room.Building)))
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
                            day.Periods[period.Period].LabGroupId = null;
                            day.Periods[period.Period].LabGroup = null; 

                        }
                        else
                        {
                            if (day.Periods[period.Period].IsLab)
                            {
                                day.Periods[period.Period].CourseId = day.Periods[period.Period].Course.Id;
                                day.Periods[period.Period].Course = null;
                                day.Periods[period.Period].InstructorId = day.Periods[period.Period].Instructor.Id;
                                day.Periods[period.Period].Instructor = null;
                                day.Periods[period.Period].RoomId = day.Periods[period.Period].Room.Id;
                                day.Periods[period.Period].Room = null;
                                day.Periods[period.Period].LabGroupId = day.Periods[period.Period].LabGroup.Id;
                                day.Periods[period.Period].LabGroup = null;
                                
                            }
                            else
                            {
                                day.Periods[period.Period].CourseId = day.Periods[period.Period].Course.Id;
                                day.Periods[period.Period].Course = null;
                                day.Periods[period.Period].InstructorId = day.Periods[period.Period].Instructor.Id;
                                day.Periods[period.Period].Instructor = null;
                                day.Periods[period.Period].RoomId = day.Periods[period.Period].Room.Id;
                                day.Periods[period.Period].Room = null;
                                day.Periods[period.Period].LabGroup = null;
                                day.Periods[period.Period].LabGroupId = null;
                            }
                            
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

        [HttpPost]
        [Route("api/schedules/canbeswapped")]
        public IHttpActionResult CanScheduleSwapTakePlace(ScheduleEntrySwapModel model)
        {
            var currentSemester = _context
                                    .AcademicSemesters
                                    .SingleOrDefault(s => s.CurrentSemester);


            var scheduleEntries = _context.ScheduleEntries
                                    .Include(s => s.Instructor)
                                    .Include(s => s.Room)
                                    .Include(s => s.Day)
                                    .Include(s => s.Day.Schedule)
                                    .Where(s => s.Day.Schedule.AcademicSemesterId == currentSemester.Id)
                                    .ToList();

            bool willPeriodAInstructorClash = scheduleEntries.Any(s => s.Day.DayNumber == model.PeriodBDayNumber
                                                    && s.Period == model.PeriodB.Period
                                                    && s.InstructorId == model.PeriodA.Instructor.Id);
            if (willPeriodAInstructorClash)
            {
                return NotFound();
            }

            bool willPeriodBInstructorClash = scheduleEntries.Any(s => s.Day.DayNumber == model.PeriodADayNumber
                                                    && s.Period == model.PeriodA.Period
                                                    && s.InstructorId == model.PeriodB.Instructor.Id);

            if (willPeriodBInstructorClash)
            {
                return NotFound();
            }

            bool willPeriodARoomClash = scheduleEntries.Any(s => s.Day.DayNumber == model.PeriodBDayNumber
                                                        && s.Period == model.PeriodB.Period
                                                        && s.RoomId == model.PeriodA.Room.Id);

            if (willPeriodARoomClash)
            {
                return NotFound();
            }

            bool willPeriodBRoomClash = scheduleEntries.Any(s => s.Day.DayNumber == model.PeriodADayNumber
                                                        && s.Period == model.PeriodA.Period
                                                        && s.RoomId == model.PeriodB.Room.Id);

            return Ok(); 
        }
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

    }
}
