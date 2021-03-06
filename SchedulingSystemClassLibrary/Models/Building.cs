﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchedulingSystemClassLibrary.Models
{
    public class Building
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Number { get; set; }
        public List<Room> Rooms { get; set; }

        public string BuildingTitle
        {
            get
            {
                string buildingNumber = "";
                if (Number < 10)
                {
                    buildingNumber = "0" + Number;
                }
                else
                {
                    buildingNumber = Number.ToString();
                }
                
                return $"{Name}-{buildingNumber}";
            }
        }
    }
}
