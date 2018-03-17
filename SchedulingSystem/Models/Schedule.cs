using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchedulingSystem.Models
{
    public class Schedule
    {
        public int Id { get; set; }

        private SchedulingContext _context;
        public virtual List<ScheduleEntry> ScheduleDNA { get; set; }
        public double Fitness { get; set; }
        public virtual Section Section { get; set; }
        public int SectionId { get; set; }


        public Schedule(Section section)
        {
            Section = section; 

            if (ScheduleDNA == null)
            {
                ScheduleDNA = new List<ScheduleEntry>(GlobalConfig.NUM_OF_DAYS * GlobalConfig.NUM_OF_PERIODS);
            }

            InitializeScheduleDna(); 

            _context = new SchedulingContext();

            List<CourseOffering> courseOfferings = _context.CourseOfferings.Where(c => c.SectionId == Section.Id).ToList();

            foreach (CourseOffering offering in courseOfferings)
            {
                var instructor = offering.Instructor;
                var course = offering.Course;

                byte lecture = course.Lecture;
                byte lab = course.Laboratory;
                byte tutor = course.Tutor;

                Random rand = new Random();


                while (lecture > 0)
                {
                    // Get the lecture room for the current section
                    var room = Section.AssignedRooms.SingleOrDefault(r => r.IsLectureRoom == true);

                    // pick a random slot to index into the ScheduleDNA list 
                    byte randSlot = (byte)rand.Next(0, GlobalConfig.NUM_OF_DAYS * GlobalConfig.NUM_OF_PERIODS);

                    if (lecture > 1)
                    {
                        byte[] slots = FindConsecutiveSlots(randSlot, 2); 

                        if(slots.Where(b => b == 0).Count() <= 1)
                        {
                            var entry = ScheduleDNA[slots[0]];
                            entry.Course = course;
                            entry.Instructor = instructor;
                            entry.Room = room;
                            entry.IsLecture = true;

                            entry = ScheduleDNA[slots[1]];
                            entry.Course = course;
                            entry.Instructor = instructor;
                            entry.Room = room;
                            entry.IsLecture = true;

                            lecture -= 2;
                        }
                    }
                    else
                    {
                        if(ScheduleDNA[randSlot].Course == null)
                        {
                            var entry = ScheduleDNA[randSlot];
                            entry.Course = course;
                            entry.Instructor = instructor;
                            entry.Room = room;
                            entry.IsLecture = true;

                            lecture--; 
                        }
                    }
                }

                while (lab > 0)
                {
                    var room = Section.AssignedRooms.SingleOrDefault(r => r.IsLabRoom == true);

                    // pick a random slot to index into the ScheduleDNA list 
                    byte randSlot = (byte)rand.Next(0, GlobalConfig.NUM_OF_DAYS * GlobalConfig.NUM_OF_PERIODS);

                    if (lab > 2)
                    {
                        byte[] slots = FindConsecutiveSlots(randSlot, 3);

                        if (slots.Where(b => b == 0).Count() <= 1)
                        {
                            var entry = ScheduleDNA[slots[0]];
                            entry.Course = course;
                            entry.Instructor = instructor;
                            entry.Room = room;
                            entry.IsLab = true;

                            entry = ScheduleDNA[slots[1]];
                            entry.Course = course;
                            entry.Instructor = instructor;
                            entry.Room = room;
                            entry.IsLab = true;

                            entry = ScheduleDNA[slots[2]];
                            entry.Course = course;
                            entry.Instructor = instructor;
                            entry.Room = room;
                            entry.IsLab = true;

                            lab -= 3;
                        }
                        //    if (randSlot == 0)
                        //{
                        //    if (ScheduleDNA[randSlot].Course == null && ScheduleDNA[randSlot + 1].Course == null && ScheduleDNA[randSlot + 2].Course == null )
                        //    {
                        //        var entry = ScheduleDNA[randSlot];
                        //        entry.Course = course;
                        //        entry.Instructor = instructor;
                        //        entry.Room = room;
                        //        entry.IsLab = true;

                        //        entry = ScheduleDNA[randSlot + 1];
                        //        entry.Course = course;
                        //        entry.Instructor = instructor;
                        //        entry.Room = room;
                        //        entry.IsLab = true;

                        //        entry = ScheduleDNA[randSlot + 2];
                        //        entry.Course = course;
                        //        entry.Instructor = instructor;
                        //        entry.Room = room;
                        //        entry.IsLab = true;

                        //        lab -= 3;
                        //    }
                        //}
                        //else if (randSlot == GlobalConfig.NUM_OF_DAYS * GlobalConfig.NUM_OF_PERIODS - 1)
                        //{
                        //    if (ScheduleDNA[randSlot - 2].Course == null && ScheduleDNA[randSlot - 1].Course == null && ScheduleDNA[randSlot].Course == null)
                        //    {
                        //        var entry = ScheduleDNA[randSlot - 2];
                        //        entry.Course = course;
                        //        entry.Instructor = instructor;
                        //        entry.Room = room;
                        //        entry.IsLab = true;

                        //        entry = ScheduleDNA[randSlot - 1];
                        //        entry.Course = course;
                        //        entry.Instructor = instructor;
                        //        entry.Room = room;
                        //        entry.IsLab = true;

                        //        entry = ScheduleDNA[randSlot];
                        //        entry.Course = course;
                        //        entry.Instructor = instructor;
                        //        entry.Room = room;
                        //        entry.IsLab = true;

                        //        lab -= 3;
                        //    }
                        //}
                        //else if (randSlot == 1)
                        //{
                        //    if (ScheduleDNA[randSlot - 1].Course == null && ScheduleDNA[randSlot].Course == null && ScheduleDNA[randSlot + 1].Course == null)
                        //    {
                        //        var entry = ScheduleDNA[randSlot - 1];
                        //        entry.Course = course;
                        //        entry.Instructor = instructor;
                        //        entry.Room = room;
                        //        entry.IsLab = true;

                        //        entry = ScheduleDNA[randSlot];
                        //        entry.Course = course;
                        //        entry.Instructor = instructor;
                        //        entry.Room = room;
                        //        entry.IsLab = true;

                        //        entry = ScheduleDNA[randSlot + 1];
                        //        entry.Course = course;
                        //        entry.Instructor = instructor;
                        //        entry.Room = room;
                        //        entry.IsLab = true;

                        //        lab -= 3;
                        //    }
                        //    if (ScheduleDNA[randSlot].Course == null && ScheduleDNA[randSlot + 1].Course == null && ScheduleDNA[randSlot + 2].Course == null)
                        //    {
                        //        var entry = ScheduleDNA[randSlot];
                        //        entry.Course = course;
                        //        entry.Instructor = instructor;
                        //        entry.Room = room;
                        //        entry.IsLab = true;

                        //        entry = ScheduleDNA[randSlot + 1];
                        //        entry.Course = course;
                        //        entry.Instructor = instructor;
                        //        entry.Room = room;
                        //        entry.IsLab = true;

                        //        entry = ScheduleDNA[randSlot + 2];
                        //        entry.Course = course;
                        //        entry.Instructor = instructor;
                        //        entry.Room = room;
                        //        entry.IsLab = true;

                        //        lab -= 3;
                        //    }
                        //}
                        //else if (randSlot == GlobalConfig.NUM_OF_DAYS * GlobalConfig.NUM_OF_PERIODS - 2)
                        //{
                        //    if (ScheduleDNA[randSlot - 1].Course == null && ScheduleDNA[randSlot].Course == null && ScheduleDNA[randSlot + 1].Course == null)
                        //    {
                        //        var entry = ScheduleDNA[randSlot - 1];
                        //        entry.Course = course;
                        //        entry.Instructor = instructor;
                        //        entry.Room = room;
                        //        entry.IsLab = true;

                        //        entry = ScheduleDNA[randSlot];
                        //        entry.Course = course;
                        //        entry.Instructor = instructor;
                        //        entry.Room = room;
                        //        entry.IsLab = true;

                        //        entry = ScheduleDNA[randSlot + 1];
                        //        entry.Course = course;
                        //        entry.Instructor = instructor;
                        //        entry.Room = room;
                        //        entry.IsLab = true;

                        //        lab -= 3;
                        //    }
                        //    else if (ScheduleDNA[randSlot - 2].Course == null && ScheduleDNA[randSlot - 1].Course == null && ScheduleDNA[randSlot].Course == null)
                        //    {
                        //        var entry = ScheduleDNA[randSlot - 2];
                        //        entry.Course = course;
                        //        entry.Instructor = instructor;
                        //        entry.Room = room;
                        //        entry.IsLab = true;

                        //        entry = ScheduleDNA[randSlot - 1];
                        //        entry.Course = course;
                        //        entry.Instructor = instructor;
                        //        entry.Room = room;
                        //        entry.IsLab = true;

                        //        entry = ScheduleDNA[randSlot];
                        //        entry.Course = course;
                        //        entry.Instructor = instructor;
                        //        entry.Room = room;
                        //        entry.IsLab = true;

                        //        lab -= 3;
                        //    }
                        //}
                        //else
                        //{
                        //    if (ScheduleDNA[randSlot - 1].Course == null && ScheduleDNA[randSlot].Course == null && ScheduleDNA[randSlot + 1].Course == null)
                        //    {
                        //        var entry = ScheduleDNA[randSlot - 1];
                        //        entry.Course = course;
                        //        entry.Instructor = instructor;
                        //        entry.Room = room;
                        //        entry.IsLab = true;

                        //        entry = ScheduleDNA[randSlot];
                        //        entry.Course = course;
                        //        entry.Instructor = instructor;
                        //        entry.Room = room;
                        //        entry.IsLab = true;

                        //        entry = ScheduleDNA[randSlot + 1];
                        //        entry.Course = course;
                        //        entry.Instructor = instructor;
                        //        entry.Room = room;
                        //        entry.IsLab = true;

                        //        lab -= 3;
                        //    }
                        //    else if (ScheduleDNA[randSlot - 2].Course == null && ScheduleDNA[randSlot - 1].Course == null && ScheduleDNA[randSlot].Course == null)
                        //    {
                        //        var entry = ScheduleDNA[randSlot - 2];
                        //        entry.Course = course;
                        //        entry.Instructor = instructor;
                        //        entry.Room = room;
                        //        entry.IsLab = true;

                        //        entry = ScheduleDNA[randSlot - 1];
                        //        entry.Course = course;
                        //        entry.Instructor = instructor;
                        //        entry.Room = room;
                        //        entry.IsLab = true;

                        //        entry = ScheduleDNA[randSlot];
                        //        entry.Course = course;
                        //        entry.Instructor = instructor;
                        //        entry.Room = room;
                        //        entry.IsLab = true;

                        //        lab -= 3;
                        //    }
                        //    else if (ScheduleDNA[randSlot].Course == null && ScheduleDNA[randSlot + 1].Course == null && ScheduleDNA[randSlot + 2].Course == null)
                        //    {
                        //        var entry = ScheduleDNA[randSlot];
                        //        entry.Course = course;
                        //        entry.Instructor = instructor;
                        //        entry.Room = room;
                        //        entry.IsLab = true;

                        //        entry = ScheduleDNA[randSlot + 1];
                        //        entry.Course = course;
                        //        entry.Instructor = instructor;
                        //        entry.Room = room;
                        //        entry.IsLab = true;

                        //        entry = ScheduleDNA[randSlot + 2];
                        //        entry.Course = course;
                        //        entry.Instructor = instructor;
                        //        entry.Room = room;
                        //        entry.IsLab = true;

                        //        lab -= 3;
                        //    }

                        //}
                    }
                    else if (lab > 1)
                    {
                        byte[] slots = FindConsecutiveSlots(randSlot, 2);

                        if (slots.Where(b => b == 0).Count() <= 1)
                        {
                            var entry = ScheduleDNA[slots[0]];
                            entry.Course = course;
                            entry.Instructor = instructor;
                            entry.Room = room;
                            entry.IsLab = true;

                            entry = ScheduleDNA[slots[1]];
                            entry.Course = course;
                            entry.Instructor = instructor;
                            entry.Room = room;
                            entry.IsLab = true;

                            lab -= 2;
                        }
                        //    if (randSlot == 0)
                        //{
                        //    if (ScheduleDNA[randSlot].Course == null && ScheduleDNA[randSlot + 1].Course == null)
                        //    {
                        //        var entry = ScheduleDNA[randSlot];
                        //        entry.Course = course;
                        //        entry.Instructor = instructor;
                        //        entry.Room = room;
                        //        entry.IsLab = true;

                        //        entry = ScheduleDNA[randSlot + 1];
                        //        entry.Course = course;
                        //        entry.Instructor = instructor;
                        //        entry.Room = room;
                        //        entry.IsLab = true;

                        //        lab -= 2; 
                        //    }
                        //}
                        //else if (randSlot == GlobalConfig.NUM_OF_DAYS * GlobalConfig.NUM_OF_PERIODS - 1)
                        //{
                        //    if (ScheduleDNA[randSlot - 1].Course == null && ScheduleDNA[randSlot].Course == null)
                        //    {
                        //        var entry = ScheduleDNA[randSlot - 1];
                        //        entry.Course = course;
                        //        entry.Instructor = instructor;
                        //        entry.Room = room;
                        //        entry.IsLab = true;

                        //        entry = ScheduleDNA[randSlot];
                        //        entry.Course = course;
                        //        entry.Instructor = instructor;
                        //        entry.Room = room;
                        //        entry.IsLab = true;

                        //        lab -= 2;
                        //    }
                        //}
                        //else
                        //{
                        //    if (ScheduleDNA[randSlot - 1].Course == null && ScheduleDNA[randSlot].Course == null)
                        //    {
                        //        var entry = ScheduleDNA[randSlot - 1];
                        //        entry.Course = course;
                        //        entry.Instructor = instructor;
                        //        entry.Room = room;
                        //        entry.IsLab = true;

                        //        entry = ScheduleDNA[randSlot];
                        //        entry.Course = course;
                        //        entry.Instructor = instructor;
                        //        entry.Room = room;
                        //        entry.IsLab = true;

                        //        lab -= 2;
                        //    }
                        //    else if (ScheduleDNA[randSlot].Course == null && ScheduleDNA[randSlot + 1].Course == null)
                        //    {
                        //        var entry = ScheduleDNA[randSlot];
                        //        entry.Course = course;
                        //        entry.Instructor = instructor;
                        //        entry.Room = room;
                        //        entry.IsLab = true;

                        //        entry = ScheduleDNA[randSlot + 1];
                        //        entry.Course = course;
                        //        entry.Instructor = instructor;
                        //        entry.Room = room;
                        //        entry.IsLab = true;

                        //        lab -= 2;
                        //    }
                        //}
                    }
                    else
                    {
                        if (ScheduleDNA[randSlot].Course == null)
                        {
                            var entry = ScheduleDNA[randSlot];
                            entry.Course = course;
                            entry.Instructor = instructor;
                            entry.Room = room;
                            entry.IsLab = true;

                            lab--;
                        }
                    }

                }

                while (tutor > 0)
                {
                    var room = Section.AssignedRooms.SingleOrDefault(r => r.IsLectureRoom == true);

                    // pick a random slot to index into the ScheduleDNA list 
                    byte randSlot = (byte)rand.Next(0, GlobalConfig.NUM_OF_DAYS * GlobalConfig.NUM_OF_PERIODS);

                    if (tutor > 1)
                    {
                        byte[] slots = FindConsecutiveSlots(randSlot, 2);

                        if (slots.Where(b => b == 0).Count() <= 1)
                        {
                            var entry = ScheduleDNA[slots[0]];
                            entry.Course = course;
                            entry.Instructor = instructor;
                            entry.Room = room;
                            entry.IsTutor = true;

                            entry = ScheduleDNA[slots[1]];
                            entry.Course = course;
                            entry.Instructor = instructor;
                            entry.Room = room;
                            entry.IsTutor = true;

                            tutor -= 2;
                        }
                    }
                    else
                    {
                        if (ScheduleDNA[randSlot].Course == null)
                        {
                            var entry = ScheduleDNA[randSlot];
                            entry.Course = course;
                            entry.Instructor = instructor;
                            entry.Room = room;
                            entry.IsTutor = true;

                            tutor--;
                        }
                    }

                }

                //for (byte i = 0; i < lecture; i++)
                //{
                //    var room = Section.AssignedRooms.SingleOrDefault(r => r.IsLectureRoom == true);

                //    while (true)
                //    {
                //        byte randSlot = (byte)rand.Next(0, GlobalConfig.NUM_OF_DAYS * GlobalConfig.NUM_OF_PERIODS);

                //        if (ScheduleDNA[randSlot].Course == null)
                //        {
                //            var entry = ScheduleDNA[randSlot];
                //            entry.Course = course;
                //            entry.Instructor = instructor;
                //            entry.Room = room;
                //            entry.IsLecture = true;

                //            break; 
                //        }
                //    }
                //}

                //for (byte i = 0; i < lab; i++)
                //{
                //    var room = Section.AssignedRooms.SingleOrDefault(r => r.IsLabRoom == true);

                //    while (true)
                //    {
                //        byte randSlot = (byte)rand.Next(0, GlobalConfig.NUM_OF_DAYS * GlobalConfig.NUM_OF_PERIODS);

                //        if (ScheduleDNA[randSlot].Course == null)
                //        {
                //            var entry = ScheduleDNA[randSlot];
                //            entry.Course = course;
                //            entry.Instructor = instructor;
                //            entry.Room = room;
                //            entry.IsLab = true;

                //            break;
                //        }
                //    }
                //}

                //for (byte i = 0; i < tutor; i++)
                //{
                //    var room = Section.AssignedRooms.SingleOrDefault(r => r.IsLectureRoom == true);

                //    while (true)
                //    {
                //        byte randSlot = (byte)rand.Next(0, GlobalConfig.NUM_OF_DAYS * GlobalConfig.NUM_OF_PERIODS);

                //        if (ScheduleDNA[randSlot].Course == null)
                //        {
                //            var entry = ScheduleDNA[randSlot];
                //            entry.Course = course;
                //            entry.Instructor = instructor;
                //            entry.Room = room;
                //            entry.IsTutor = true;

                //            break;
                //        }
                //    }
                //}
            }
        }

        public void CalculateFitness()
        {
            int score = 0;

            List<ScheduleEntry> scheduleEntries = ScheduleDNA.Where(s => s.Course != null).ToList();

            foreach (var scheduleEntry in scheduleEntries)
            {
                var instructor = scheduleEntry.Instructor;
                var day = scheduleEntry.Day;
                var period = scheduleEntry.Period;
                var room = scheduleEntry.Room;

                
                bool isInstructorAssignedMoreThanOnce = false;
                bool isRoomOccupiedByMoreThanOneSection = false;

                if (_context.ScheduleEntries.Count() > 0)
                {
                    // Make sure the teacher is not assigned to more than one place at the same time 
                    isInstructorAssignedMoreThanOnce = _context.ScheduleEntries.Any(s => s.InstructorId == instructor.Id
                                                                && s.Day == day
                                                                && s.Period == period
                                                                );
                    // Make sure the class is not occupied by more than one section at the time 
                    isRoomOccupiedByMoreThanOneSection = _context.ScheduleEntries.Any(s => s.Id == room.Id
                                                                && s.Day == day
                                                                && s.Period == period
                                                                );
                }
                int index = ScheduleDNA.IndexOf(scheduleEntry);
                if(FindPreviousCourseId(index) == scheduleEntry.Course.Id || FindNextCourseId(index) == scheduleEntry.Course.Id)
                {
                    score++; 
                }
                 
                if (!isInstructorAssignedMoreThanOnce)
                    score++;
                if (!isRoomOccupiedByMoreThanOneSection)
                    score++; 

            }
            
            Fitness = (float)score/GlobalConfig.NUM_OF_DAYS*GlobalConfig.NUM_OF_PERIODS*3; // 2 is the number of checks we performed 
        }

        private void InitializeScheduleDna()
        {
            for (byte i = 0; i < GlobalConfig.NUM_OF_DAYS; i++)
            {
                for (byte j = 0; j < GlobalConfig.NUM_OF_PERIODS; j++)
                {
                    ScheduleDNA.Add(new ScheduleEntry
                    {
                        Day = i,
                        Period = j
                    });
                }
            }
        }
        private int FindPreviousCourseId(int index)
        {
            if (index == 0)
                return -1;

            if (ScheduleDNA[index - 1].Course == null)
                return -1; 

            return ScheduleDNA[index - 1].Course.Id; 
        }

        private int FindNextCourseId(int index)
        {
            if (index == GlobalConfig.NUM_OF_DAYS * GlobalConfig.NUM_OF_PERIODS - 1)
                return -1;

            if (ScheduleDNA[index + 1].Course == null)
                return -1; 

            return ScheduleDNA[index + 1].Course.Id; 
        }

        private byte[] FindConsecutiveSlots(byte randSlot, byte size)
        {
            byte[] slots = new byte[size];

            byte min = 0;
            byte max = GlobalConfig.NUM_OF_DAYS * GlobalConfig.NUM_OF_PERIODS - 1; 

            if(randSlot == min)
            {
                var scheduleEntries = ScheduleDNA.GetRange(randSlot, size);

                if (scheduleEntries.Where(s => s.Course == null).Count() == size)
                {
                    for (byte i = 0; i < size; i++)
                    {
                        slots[i] = (byte)(randSlot + i); 
                    }
                }
            }
            else if (randSlot == max)
            {
                var scheduleEntries = ScheduleDNA.GetRange(randSlot - (size - 1), size);

                if (scheduleEntries.Where(s => s.Course == null).Count() == size)
                {
                    for (byte i = 0; i < size; i++)
                    {
                        slots[i] = (byte)(randSlot - i); 
                    }
                }
            }
            else
            {
                if (size > 2)
                {
                    if (randSlot - (size - 1) >= min && randSlot + (size - 1) <= max)
                    {
                        var scheduleEntriesLeft = ScheduleDNA.GetRange(randSlot - (size - 1), size);
                        var scheduleEntriesRight = ScheduleDNA.GetRange(randSlot, size);
                        
                        if (scheduleEntriesLeft.Where(s => s.Course == null).Count() == 3)
                        {
                            for (byte i = 0; i < size; i++)
                            {
                                slots[i] = (byte)(randSlot - (size - 1 - i));
                            }
                        }
                        else if (scheduleEntriesRight.Where(s => s.Course == null).Count() == 3)
                        {
                            for (byte i = 0; i < size; i++)
                            {
                                slots[i] = (byte)(randSlot + i); 
                            }
                        }
                        
                    }
                    else if (randSlot - (size - 2) >= min && randSlot + (size - 2) <= max)
                    {
                        var range = ScheduleDNA.GetRange(randSlot - 1, size);

                        if (range.Where(s => s.Course == null).Count() == 3)
                        {
                            for (byte i = 0; i < size; i++)
                            {
                                slots[i] = (byte)(randSlot - 1 + i);
                            }
                        }
                    }
                }
                else
                {
                    var scheduleEntriesLeft = ScheduleDNA.GetRange(randSlot - (size - 1), size);
                    var scheduleEntriesRight = ScheduleDNA.GetRange(randSlot, size);

                    if (scheduleEntriesLeft.Where(s => s.Course == null).Count() == size)
                    {
                        for (byte i = 0; i < size; i++)
                        {
                            slots[i] = (byte)(randSlot - i);
                        }
                    }
                    else if (scheduleEntriesRight.Where(s => s.Course == null).Count() == size)
                    {
                        for (byte i = 0; i < size; i++)
                        {
                            slots[i] = (byte)(randSlot + i);
                        }
                    }
                }
            }


            return slots; 
        }

        public void Print()
        {

            for (int i = 0; i < GlobalConfig.NUM_OF_DAYS; i++)
            {
                for (int j = 0; j < GlobalConfig.NUM_OF_PERIODS; j++)
                {
                    if (ScheduleDNA[(i * 8) + j].Course != null)
                    {
                        Console.Write(ScheduleDNA[(i * 8) + j].Course.Title + "(" + ScheduleDNA[(i * 8) + j].Period + ")");
                        if(ScheduleDNA[(i * 8) + j].IsLecture)
                            Console.Write("(Lecture)");
                        if (ScheduleDNA[(i * 8) + j].IsLab)
                            Console.Write("(Lab)");
                        if (ScheduleDNA[(i * 8) + j].IsTutor)
                            Console.Write("(T)");
                        Console.Write("|");

                    }
                        
                        
                        
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }
    }
}
