using AutoMapper;
using SchedulingSystemClassLibrary.Dtos;
using SchedulingSystemClassLibrary.Models;
using SchedulingSystemClassLibrary.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SchedulingSystemWeb.App_Start
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            Mapper.CreateMap<Section, SectionDto>();
            Mapper.CreateMap<Section, SectionDtoMinimal>();
            Mapper.CreateMap<Department, DepartmentDto>();
            Mapper.CreateMap<Room, RoomDto>();
            Mapper.CreateMap<Building, BuildingDto>();
            Mapper.CreateMap<Building, BuildingDtoMinimal>();
            Mapper.CreateMap<Instructor, InstructorDto>();
            Mapper.CreateMap<Instructor, InstructorDtoMinimal>();
            Mapper.CreateMap<Course, CourseDto>();
            Mapper.CreateMap<CourseOffering, CourseOfferingDto>();
            Mapper.CreateMap<CourseOffering, CourseOfferingsFormViewModel>();
            Mapper.CreateMap<Day, DayDto>();
            Mapper.CreateMap<ScheduleEntry, ScheduleEntryDto>();
            Mapper.CreateMap<Schedule, ScheduleDto>();
            Mapper.CreateMap<AcademicSemester, AcademicSemesterDto>();
            Mapper.CreateMap<AcademicYear, AcademicYearDto>();
            Mapper.CreateMap<LabGroup, LabGroupDto>();

            Mapper.CreateMap<SectionDto, Section>();
            Mapper.CreateMap<SectionDtoMinimal, Section>();
            Mapper.CreateMap<DepartmentDto, Department>();
            Mapper.CreateMap<RoomDto, Room>();
            Mapper.CreateMap<BuildingDto, Building>();
            Mapper.CreateMap<BuildingDtoMinimal, Building>();
            Mapper.CreateMap<InstructorDto, Instructor>();
            Mapper.CreateMap<InstructorDtoMinimal, Instructor>();
            Mapper.CreateMap<CourseDto, Course>();
            Mapper.CreateMap<CourseOfferingDto, CourseOffering>();
            Mapper.CreateMap<DayDto, Day>();
            Mapper.CreateMap<ScheduleEntryDto, ScheduleEntry>();
            Mapper.CreateMap<ScheduleDto, Schedule>();
            Mapper.CreateMap<AcademicSemesterDto, AcademicSemester>();
            Mapper.CreateMap<AcademicYearDto, AcademicYear>();
            Mapper.CreateMap<LabGroupDto, LabGroup>();
        }
    }
}