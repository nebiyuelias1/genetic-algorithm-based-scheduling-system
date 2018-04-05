using AutoMapper;
using SchedulingSystemClassLibrary.Dtos;
using SchedulingSystemClassLibrary.Models;
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
            Mapper.CreateMap<Department, DepartmentDto>();
            Mapper.CreateMap<Room, RoomDto>();
            Mapper.CreateMap<Building, BuildingDto>();

        }
    }
}