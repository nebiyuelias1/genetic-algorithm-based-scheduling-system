using AutoMapper;
using SchedulingSystemClassLibrary;
using SchedulingSystemClassLibrary.Dtos;
using SchedulingSystemClassLibrary.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SchedulingSystemWeb.Controllers.Api
{
    public class SectionsController : ApiController
    {
        private SchedulingContext _context;

        public SectionsController()
        {
            _context = new SchedulingContext();
        }

        // GET /api/sections 
        public IHttpActionResult GetSections()
        {
            var rooms = _context.Rooms.Include(r => r.Building).ToList();
            rooms.Insert(0, new Room {
                IsLectureRoom = true, 
                IsLabRoom = true
            });

            var roomDtos = rooms.Select(Mapper.Map<Room, RoomDto>);

            var sections = _context.Sections
                            .Include(s => s.Department)
                            .ToList();
            var sectionDtos = sections.Select(Mapper.Map<Section, SectionDto>);
                            //.Select(s => new
                            //{
                            //    s.Id, 
                            //    s.Name, 
                            //    s.EntranceYear, 
                            //    s.StudentCount, 
                            //    Department = Mapper.Map<Department, DepartmentDto>(s.Department),
                            //    AssignedLectureRoomId =  s.AssignedLectureRoomId == null ? 0 : s.AssignedLectureRoomId,
                            //    AssignedLabRoomId = s.AssignedLabRoom.Id == null ? 0 : s.AssignedLabRoomId,
                            //    Rooms = roomDtos
                            //})
                            //.ToList();

            return Ok(sectionDtos.Select(s => new
            {
                Id = s.Id,
                Name = s.Name, 
                EntranceYear = s.EntranceYear, 
                StudentCount = s.StudentCount, 
                Department = s.Department, 
                DepartmentId = s.DepartmentId,
                AssignedLectureRoomId = s.AssignedLectureRoomId,
                AssignedLabRoomId = s.AssignedLabRoomId,
                Rooms = roomDtos

            }));
        }

        // DELETE /api/sections/1 
        [HttpDelete]
        public void DeleteSection(int id)
        {
            var sectionInDb = _context.Sections.SingleOrDefault(x => x.Id == id);

            if (sectionInDb == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            _context.Sections.Remove(sectionInDb);
            _context.SaveChanges();
        }

        // POST /api/sections/assign 
        [Route("api/sections/assign")]
        [HttpPost]
        public void AssignRoom(RoomAssignmentInfo info)
        {
            var sectionInDb = _context.Sections.SingleOrDefault(s => s.Id == info.SectionId);

            if (info.LectureRoomId != 0)
            {
                sectionInDb.AssignedLectureRoomId = info.LectureRoomId; 
            }

            if (info.LabRoomId != 0)
            {
                sectionInDb.AssignedLabRoomId = info.LabRoomId; 
            }
            _context.SaveChanges();
        }
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }
    }
}
