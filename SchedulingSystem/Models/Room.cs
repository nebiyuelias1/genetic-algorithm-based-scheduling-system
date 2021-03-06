﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchedulingSystem.Models
{
    public class Room
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Building Building { get; set; }
        public int BuildingId { get; set; }
        public int Size { get; set; }
        public List<Section> AssignedSections { get; set; }
        // TODO - should you add IsLab, IsShared properties? 
        public bool IsLabRoom { get; set; }
        public bool IsLectureRoom { get; set; }
        public string Title
        {
            get
            {
                return $"{Name} in {Building.Title}";
            }
        }
    }
}
