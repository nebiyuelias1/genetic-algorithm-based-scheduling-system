﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchedulingSystemClassLibrary.Models
{
    public class Room
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Building { get; set; }
        public int Size { get; set; }
        public List<Section> AssignedSections { get; set; }
        // TODO - should you add IsLab, IsShared properties? 
        public bool IsLabRoom { get; set; }
        public bool IsLectureRoom { get; set; }
    }
}