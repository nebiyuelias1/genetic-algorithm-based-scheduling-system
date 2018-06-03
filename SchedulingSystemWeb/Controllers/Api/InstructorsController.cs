using SchedulingSystemClassLibrary;
using SchedulingSystemClassLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data.Entity;
using AutoMapper;
using SchedulingSystemClassLibrary.Dtos;
using Microsoft.AspNet.Identity;

namespace SchedulingSystemWeb.Controllers.Api
{
    public class InstructorsController : ApiController
    {
        private SchedulingContext _context;

        public InstructorsController()
        {
            _context = new SchedulingContext(); 
        }

        // POST - /api/instructors/
        public void SetPreference(InstructorPreference preference)
        {
            var userId = User.Identity.GetUserId();
            var instructorInDb = _context.Instructors.SingleOrDefault(s => s.AccountId == userId);
            instructorInDb.InstructorPreference = preference;
            _context.SaveChanges();
            instructorInDb.InstructorPreferenceId = preference.Id;
            _context.SaveChanges();

        }

        public IHttpActionResult GetInstructors()
        {
            var instructors = _context.Instructors
                                .Include(i => i.Department)
                                .ToList()
                                .Select(Mapper.Map<Instructor, InstructorDto>);

            return Ok(instructors); 
        }

        public IHttpActionResult GetInstructorsInADepartment(int id)
        {
            var instructors = _context
                                .Instructors
                                .Where(i => i.DepartmentId == id)
                                .Select(Mapper.Map<Instructor, InstructorDto>);

            return Ok(instructors);
        }
        public void DeleteInstructor(int id)
        {
            var instructorInDb = _context.Instructors.SingleOrDefault(x => x.Id == id);

            if (instructorInDb == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            _context.Instructors.Remove(instructorInDb);
            _context.SaveChanges();
        }

        [HttpGet]
        [Route("api/instructors/getschedule")]
        public IHttpActionResult GetScheduleForInstructor()
        {
            var userId = User.Identity.GetUserId();

            var instructor = _context.Instructors.SingleOrDefault(s => s.AccountId == userId);

            if (instructor != null)
            {
                var currentSemesterId = _context.AcademicSemesters.Single(s => s.CurrentSemester).Id;

                var scheduleEntries = _context
                                        .ScheduleEntries
                                        .Include(x => x.Day)
                                        .Include(x => x.Course)
                                        .Include(x => x.Room.Building)
                                        .Include(x => x.LabGroup)
                                        .Include(x => x.Day.Schedule)
                                        .Where(s => s.InstructorId == instructor.Id && s.Day.Schedule.AcademicSemesterId == currentSemesterId)
                                        .ToList();

                var schedule = new ScheduleDto {
                    Days = new List<DayDto>()
                };


                for (int i = 0; i < GlobalConfig.NUM_OF_DAYS; i++)
                {
                    var day = new DayDto
                    {
                        DayNumber = (byte)i,
                        Periods = new List<ScheduleEntryDto>()
                    }; 

                    for (int j = 0; j < GlobalConfig.NUM_OF_PERIODS; j++)
                    {

                        if (scheduleEntries.Any(s => s.Period == j && s.Day.DayNumber == i))
                        {
                            var scheduleEntry = scheduleEntries.Single(s => s.Period == j && s.Day.DayNumber == i);
                            var scheduleEntryDto = Mapper.Map<ScheduleEntry, ScheduleEntryDto>(scheduleEntry);
                            day.Periods.Add(scheduleEntryDto);
                        }
                        else
                        {
                            day.Periods.Add(new ScheduleEntryDto());
                        }
                    }
                    schedule.Days.Add(day);
                }

                return Ok(schedule);
            }
            return NotFound();
        }
        protected override void Dispose(bool disposing)
        {
            _context.Dispose(); 
        }
    }
}
