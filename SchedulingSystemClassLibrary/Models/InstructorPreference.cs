using SchedulingSystemClassLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchedulingSystemClassLibrary.Models
{
    public class InstructorPreference
    {
        public int Id { get; set; }
        public List<bool> Preference {
            get
            {
                List<bool> preferenceList = new List<bool>(5); 

                var array = PreferenceData.Split(',');

                for (int i = 0; i < 5; i++)
                {
                    preferenceList.Add(array[i] == "0" ? false : true);
                }

                return preferenceList;
            }
        }
        public Instructor Instructor { get; set; }
        public int InstructorId { get; set; }
        public string PreferenceData { get; set; }

    }
}
