using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchedulingSystem.Models
{
    public class Schedule
    {
        private SchedulingContext _context;
        public ScheduleEntry[,] ScheduleDNA { get; set; } = new ScheduleEntry[5, 8];
        public double Fitness { get; set; }


        public Schedule(Section section)
        {
            _context = new SchedulingContext();

            List<CourseOffering> courseOfferings = _context.CourseOfferings.Where(c => c.SectionId == section.Id).ToList();

            foreach (CourseOffering offering in courseOfferings)
            {
                var rooms = section.AssignedRooms;
                var instructor = offering.Instructor;
                var course = offering.Course;

                byte lecture = course.Lecture;
                byte lab = course.Laboratory;
                byte tutor = course.Tutor;

                Random rand = new Random();

                for (byte i = 0; i < lecture; i++)
                {
                    var scheduleEntry = new ScheduleEntry
                    {
                        Course = course,
                        Instructor = instructor,
                        Rooms = rooms,
                        IsLecture = true
                    };

                    while (true)
                    {
                        byte day = (byte)rand.Next(0, 5);
                        byte period = (byte)rand.Next(0, 8);

                        if (ScheduleDNA[day, period] == null)
                        {
                            ScheduleDNA[day, period] = scheduleEntry;
                            break;
                        }
                    }

                }

                for (byte i = 0; i < lab; i++)
                {
                    var scheduleEntry = new ScheduleEntry
                    {
                        Course = course,
                        Instructor = instructor,
                        Rooms = rooms,
                        IsLab = true
                    };

                    while (true)
                    {
                        byte day = (byte)rand.Next(0, 5);
                        byte period = (byte)rand.Next(0, 8);

                        if (ScheduleDNA[day, period] == null)
                        {
                            ScheduleDNA[day, period] = scheduleEntry;
                            break;
                        }
                    }
                }

                for (byte i = 0; i < tutor; i++)
                {
                    var scheduleEntry = new ScheduleEntry
                    {
                        Course = course,
                        Instructor = instructor,
                        Rooms = rooms,
                        IsTutor = true
                    };

                    while (true)
                    {
                        byte day = (byte)rand.Next(0, 5);
                        byte period = (byte)rand.Next(0, 8);

                        if (ScheduleDNA[day, period] == null)
                        {
                            ScheduleDNA[day, period] = scheduleEntry;
                            break;
                        }
                    }
                }


            }
        }

        public double CalculateFitness()
        {
            return 0;
        }
    }
}
