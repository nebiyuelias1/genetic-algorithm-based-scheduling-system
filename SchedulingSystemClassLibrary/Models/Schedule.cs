using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using SchedulingSystemClassLibrary.GeneticAlgorithm;
using System.Diagnostics;

namespace SchedulingSystemClassLibrary.Models
{
    public class Schedule
    {
        private SchedulingContext _context;
        public int Id { get; set; }
        public double Fitness { get; set; }
        public double MaximumScore { get; set; }
        public Section Section { get; set; }
        public int SectionId { get; set; }
        public AcademicSemester AcademicSemester { get; set; }
        public int AcademicSemesterId { get; set; }
        public virtual List<Day> Days { get; set; }

        public IList<ScheduleEntry> scheduleEntries { get; set; }

        public Schedule()
        {

        }
        public Schedule(bool isAChild)
        {
            _context = new SchedulingContext();

            if (Days == null)
            {
                Days = new List<Day>(GlobalConfig.NUM_OF_DAYS);
            }

            InitializeSchedule();
        }
        public Schedule(Section section, Dictionary<string, byte[]> dictionary, IList<ScheduleEntry> scheduleEntries)
        {
            _context = new SchedulingContext();

            Section = section;

            if (Days == null)
            {
                Days = new List<Day>(GlobalConfig.NUM_OF_DAYS); 
            }
            
            InitializeSchedule();

            List<CourseOffering> courseOfferings = section.CourseOfferings;


            this.scheduleEntries = scheduleEntries;

            
            foreach (CourseOffering offering in courseOfferings)
            {
                var instructor = offering.Instructor;
                
               
                var course = offering.Course;

                byte lecture = course.Lecture;
                byte lab = course.Laboratory;
                byte tutor = course.Tutor;

                Random rand = new Random();

                
                while (lab > 0)
                {
                    var room = Section.AssignedLabRoom;

                    // pick a random slot to index into the ScheduleDNA list 
                    //byte randDay = (byte)rand.Next(0, GlobalConfig.NUM_OF_DAYS);
                    byte randIndex = (byte)rand.Next((byte)dictionary[offering.Course.Title].Count());
                    byte randDay = dictionary[offering.Course.Title][randIndex];
                    byte randPeriod = (byte)rand.Next(0, GlobalConfig.NUM_OF_PERIODS);

                    if (lab > 2)
                    {
                        byte[] slots = FindConsecutiveSlots(randDay, randPeriod, 3);

                        if (slots.Where(b => b == 0).Count() <= 1)
                        {
                            var entry = Days[randDay].Periods[slots[0]];
                            entry.Course = course;
                            entry.Instructor = instructor;
                            entry.Room = room;
                            entry.IsLab = true;

                            entry = Days[randDay].Periods[slots[1]];
                            entry.Course = course;
                            entry.Instructor = instructor;
                            entry.Room = room;
                            entry.IsLab = true;

                            entry = Days[randDay].Periods[slots[2]];
                            entry.Course = course;
                            entry.Instructor = instructor;
                            entry.Room = room;
                            entry.IsLab = true;

                            lab -= 3;
                        }
                    }

                    if (lab > 1)
                    {
                        byte[] slots = FindConsecutiveSlots(randDay, randPeriod, 2);

                        if (slots.Where(b => b == 0).Count() <= 1)
                        {
                            var entry = Days[randDay].Periods[slots[0]];
                            entry.Course = course;
                            entry.Instructor = instructor;
                            entry.Room = room;
                            entry.IsLab = true;

                            entry = Days[randDay].Periods[slots[1]];
                            entry.Course = course;
                            entry.Instructor = instructor;
                            entry.Room = room;
                            entry.IsLab = true;

                            lab -= 2;
                        }
                    }
                    
                    if (lab > 0)
                    {
                        if (Days[randDay].Periods[randPeriod].Course == null)
                        {
                            var entry = Days[randDay].Periods[randPeriod];
                            entry.Course = course;
                            entry.Instructor = instructor;
                            entry.Room = room;
                            entry.IsLab = true;

                            lab--;
                        }
                    }

                }

                while (lecture > 0)
                {
                    // Get the lecture room for the current section
                    var room = Section.AssignedLectureRoom;

                    // pick a random slot to index into the Day list
                    //byte randDay = (byte)rand.Next(0, GlobalConfig.NUM_OF_DAYS);
                    byte randIndex = (byte)rand.Next((byte)dictionary[offering.Course.Title].Count());
                    byte randDay = dictionary[offering.Course.Title][randIndex];
                    byte randPeriod = (byte)rand.Next(0, GlobalConfig.NUM_OF_PERIODS);

                    if (lecture > 1)
                    {
                        byte[] slots = FindConsecutiveSlots(randDay, randPeriod, 2);

                        if (slots.Where(b => b == 0).Count() <= 1)
                        {
                            var entry = Days[randDay].Periods[slots[0]];
                            entry.Course = course;
                            entry.Instructor = instructor;
                            entry.Room = room;
                            entry.IsLecture = true;

                            entry = Days[randDay].Periods[slots[1]];
                            entry.Course = course;
                            entry.Instructor = instructor;
                            entry.Room = room;
                            entry.IsLecture = true;

                            lecture -= 2;
                        }
                    }
                    
                    if(lecture > 0)
                    {
                        if (Days[randDay].Periods[randPeriod].Course == null)
                        {
                            var entry = Days[randDay].Periods[randPeriod];
                            entry.Course = course;
                            entry.Instructor = instructor;
                            entry.Room = room;
                            entry.IsLecture = true;

                            lecture--;
                        }
                    }
                }

                while (tutor > 0)
                {
                    var room = Section.AssignedLectureRoom;

                    // pick a random slot to index into the ScheduleDNA list 
                    //byte randDay = (byte)rand.Next(0, GlobalConfig.NUM_OF_DAYS);
                    byte randIndex = (byte)rand.Next((byte)dictionary[offering.Course.Title].Count());
                    byte randDay = dictionary[offering.Course.Title][randIndex];
                    byte randPeriod = (byte)rand.Next(0, GlobalConfig.NUM_OF_PERIODS);

                    if (tutor > 1)
                    {
                        byte[] slots = FindConsecutiveSlots(randDay, randPeriod, 2);

                        if (slots.Where(b => b == 0).Count() <= 1)
                        {
                            var entry = Days[randDay].Periods[slots[0]];
                            entry.Course = course;
                            entry.Instructor = instructor;
                            entry.Room = room;
                            entry.IsTutor = true;

                            entry = Days[randDay].Periods[slots[1]];
                            entry.Course = course;
                            entry.Instructor = instructor;
                            entry.Room = room;
                            entry.IsTutor = true;

                            tutor -= 2;
                        }
                    }
                    
                    if (tutor > 0)
                    {
                        if (Days[randDay].Periods[randPeriod].Course == null)
                        {
                            var entry = Days[randDay].Periods[randPeriod];
                            entry.Course = course;
                            entry.Instructor = instructor;
                            entry.Room = room;
                            entry.IsTutor = true;

                            tutor--;
                        }
                    }

                }
            }
        }
        public void Mutate()
        {
            // TODO - Implement this mutate method
            Random rand = new Random();

            SwapMorningAndAfternoonPeriods();
            ShiftToEarlyPeriods();
            SwapDays();
            //SwapPeriods();

            //Random rand = new Random(); 

            //    int randDay = rand.Next(GlobalConfig.NUM_OF_DAYS);
            //    int randPeriod = rand.Next(GlobalConfig.NUM_OF_PERIODS);

            //    if (Days[randDay].Periods[randPeriod].Course != null)
            //    {
            //        int secondRandDay = rand.Next(GlobalConfig.NUM_OF_DAYS);
            //        int secondRandPeriod = rand.Next(GlobalConfig.NUM_OF_PERIODS);

            //        var temp = Days[randDay].Periods[randPeriod];

            //        Days[randDay].Periods[randPeriod].Course = Days[secondRandDay].Periods[secondRandPeriod].Course;
            //        Days[randDay].Periods[randPeriod].Instructor = Days[secondRandDay].Periods[secondRandPeriod].Instructor;
            //        Days[randDay].Periods[randPeriod].Room = Days[secondRandDay].Periods[secondRandPeriod].Room;
            //        Days[randDay].Periods[randPeriod].IsLecture = Days[secondRandDay].Periods[secondRandPeriod].IsLecture;
            //        Days[randDay].Periods[randPeriod].IsLab = Days[secondRandDay].Periods[secondRandPeriod].IsLab;
            //        Days[randDay].Periods[randPeriod].IsTutor = Days[secondRandDay].Periods[secondRandPeriod].IsTutor;

            //        Days[secondRandDay].Periods[secondRandPeriod].Course = temp.Course; 
            //        Days[secondRandDay].Periods[secondRandPeriod].Instructor = temp.Instructor; 
            //        Days[secondRandDay].Periods[secondRandPeriod].Room = temp.Room; 
            //        Days[secondRandDay].Periods[secondRandPeriod].IsLecture = temp.IsLecture; 
            //        Days[secondRandDay].Periods[secondRandPeriod].IsLab = temp.IsLab; 
            //        Days[secondRandDay].Periods[secondRandPeriod].IsTutor = temp.IsTutor;

            //    }
        }

        private void SwapDays()
        {
            var mwfDays = new byte[] { 0, 2, 4 };
            var tthDays = new byte[] { 1, 3 };

            Random rand = new Random();

            var r = rand.Next(2);
            byte randDay;
            byte secondRandDay; 
            switch (r)
            {
                case 0:
                    randDay = mwfDays[rand.Next(3)];
                    secondRandDay = mwfDays[rand.Next(3)];

                    while (randDay == secondRandDay)
                    {
                        secondRandDay = mwfDays[rand.Next(3)];
                    }

                    var temp = this.Days[randDay].Periods;
                    this.Days[randDay].Periods = this.Days[secondRandDay].Periods;
                    this.Days[secondRandDay].Periods = temp; 
                    break;
                case 1:
                    randDay = tthDays[rand.Next(2)];
                    secondRandDay = tthDays[rand.Next(2)];

                    while (randDay == secondRandDay)
                    {
                        secondRandDay = tthDays[rand.Next(2)];
                    }

                    temp = this.Days[randDay].Periods;
                    this.Days[randDay].Periods = this.Days[secondRandDay].Periods;
                    this.Days[secondRandDay].Periods = temp;
                    break; 
            }
        }

        private void SwapPeriods()
        {
            var mwfDays = new byte[] { 0, 2, 4 };
            var tthDays = new byte[] { 1, 3 };

            Random rand = new Random();

            var r = rand.Next(2);
            byte randDay;
            switch (r)
            {
                
                
                case 0:
                    randDay = mwfDays[rand.Next(3)];
                    int randPeriod = rand.Next(GlobalConfig.NUM_OF_PERIODS);

                    int secondRandDay = mwfDays[rand.Next(3)];
                    int secondRandPeriod = rand.Next(GlobalConfig.NUM_OF_PERIODS);

                    var temp = new ScheduleEntry
                    {
                        Course = Days[randDay].Periods[randPeriod].Course,
                        Instructor = Days[randDay].Periods[randPeriod].Instructor,
                        Room = Days[randDay].Periods[randPeriod].Room ,
                        IsLecture = Days[randDay].Periods[randPeriod].IsLecture,
                        IsLab = Days[randDay].Periods[randPeriod].IsLab,
                        IsTutor = Days[randDay].Periods[randPeriod].IsTutor,
                        Period = Days[randDay].Periods[randPeriod].Period
                    };

                    Days[randDay].Periods[randPeriod].Course = Days[secondRandDay].Periods[secondRandPeriod].Course;
                    Days[randDay].Periods[randPeriod].Instructor = Days[secondRandDay].Periods[secondRandPeriod].Instructor;
                    Days[randDay].Periods[randPeriod].Room = Days[secondRandDay].Periods[secondRandPeriod].Room;
                    Days[randDay].Periods[randPeriod].IsLecture = Days[secondRandDay].Periods[secondRandPeriod].IsLecture;
                    Days[randDay].Periods[randPeriod].IsLab = Days[secondRandDay].Periods[secondRandPeriod].IsLab;
                    Days[randDay].Periods[randPeriod].IsTutor = Days[secondRandDay].Periods[secondRandPeriod].IsTutor;

                    Days[secondRandDay].Periods[secondRandPeriod].Course = temp.Course;
                    Days[secondRandDay].Periods[secondRandPeriod].Instructor = temp.Instructor;
                    Days[secondRandDay].Periods[secondRandPeriod].Room = temp.Room;
                    Days[secondRandDay].Periods[secondRandPeriod].IsLecture = temp.IsLecture;
                    Days[secondRandDay].Periods[secondRandPeriod].IsLab = temp.IsLab;
                    Days[secondRandDay].Periods[secondRandPeriod].IsTutor = temp.IsTutor;
                    break;
                case 1:
                    randDay = tthDays[rand.Next(2)];

                    randPeriod = rand.Next(GlobalConfig.NUM_OF_PERIODS);

                    secondRandDay = tthDays[rand.Next(2)];
                    secondRandPeriod = rand.Next(GlobalConfig.NUM_OF_PERIODS);

                    temp = new ScheduleEntry
                    {
                        Course = Days[randDay].Periods[randPeriod].Course,
                        Instructor = Days[randDay].Periods[randPeriod].Instructor,
                        Room = Days[randDay].Periods[randPeriod].Room,
                        IsLecture = Days[randDay].Periods[randPeriod].IsLecture,
                        IsLab = Days[randDay].Periods[randPeriod].IsLab,
                        IsTutor = Days[randDay].Periods[randPeriod].IsTutor,
                        Period = Days[randDay].Periods[randPeriod].Period
                    };

                    Days[randDay].Periods[randPeriod].Course = Days[secondRandDay].Periods[secondRandPeriod].Course;
                    Days[randDay].Periods[randPeriod].Instructor = Days[secondRandDay].Periods[secondRandPeriod].Instructor;
                    Days[randDay].Periods[randPeriod].Room = Days[secondRandDay].Periods[secondRandPeriod].Room;
                    Days[randDay].Periods[randPeriod].IsLecture = Days[secondRandDay].Periods[secondRandPeriod].IsLecture;
                    Days[randDay].Periods[randPeriod].IsLab = Days[secondRandDay].Periods[secondRandPeriod].IsLab;
                    Days[randDay].Periods[randPeriod].IsTutor = Days[secondRandDay].Periods[secondRandPeriod].IsTutor;

                    Days[secondRandDay].Periods[secondRandPeriod].Course = temp.Course;
                    Days[secondRandDay].Periods[secondRandPeriod].Instructor = temp.Instructor;
                    Days[secondRandDay].Periods[secondRandPeriod].Room = temp.Room;
                    Days[secondRandDay].Periods[secondRandPeriod].IsLecture = temp.IsLecture;
                    Days[secondRandDay].Periods[secondRandPeriod].IsLab = temp.IsLab;
                    Days[secondRandDay].Periods[secondRandPeriod].IsTutor = temp.IsTutor;
                    break; 
            }
        }

        private void SwapMorningAndAfternoonPeriods()
        {
            Random rand = new Random();

            var dayNumber = rand.Next(GlobalConfig.NUM_OF_DAYS);

            var morningPeriods = this.Days[dayNumber].Periods.GetRange(0, 4);
            var afternoonPeriods = this.Days[dayNumber].Periods.GetRange(4, 4);
            var swapped = afternoonPeriods.Concat(morningPeriods).ToList();

            for (byte i = 0; i < GlobalConfig.NUM_OF_PERIODS; i++)
            {
                swapped[i].Period = i;
            }

            this.Days[dayNumber].Periods = swapped;
        }
        private void ShiftToEarlyPeriods()
        {
            byte minMorning = 0;
            byte minAfternoon = 4;
            foreach (var day in Days)
            {
                var morningPeriods = day.Periods.GetRange(minMorning, 4);
                var moringPeriodsCount = morningPeriods.Where(s => s.Course != null).Count();

                var afternoonPeriods = day.Periods.GetRange(minAfternoon, 4);
                var afternoonPeriodsCount = afternoonPeriods.Where(s => s.Course != null).Count();

                if (moringPeriodsCount > 0 && day.Periods[minMorning].Course == null)
                {
                    var period = morningPeriods.FirstOrDefault(s => s.Course != null);
                    var index = day.Periods.IndexOf(period);

                    for (int i = index, count = 0; count < moringPeriodsCount; i++, count++)
                    {
                        day.Periods[i - 1].Course = day.Periods[i].Course;
                        day.Periods[i - 1].Instructor = day.Periods[i].Instructor;
                        day.Periods[i - 1].Room = day.Periods[i].Room;
                        if (day.Periods[i].IsLecture)
                            day.Periods[i - 1].IsLecture = true;
                        else if (day.Periods[i].IsLab)
                            day.Periods[i - 1].IsLab = true;
                        else if (day.Periods[i].IsTutor)
                        {
                            day.Periods[i - 1].IsTutor = true;
                        }

                        day.Periods[i].Course = null;
                        day.Periods[i].Instructor = null;
                        day.Periods[i].Room = null;
                        day.Periods[i].IsLecture = false;
                        day.Periods[i].IsLab = false;
                        day.Periods[i].IsTutor = false;

                    }
                }
                else if (afternoonPeriodsCount > 0 && day.Periods[minAfternoon].Course == null)
                {
                    var period = afternoonPeriods.FirstOrDefault(s => s.Course != null);
                    var index = day.Periods.IndexOf(period);

                    for (int i = index, count = 0; count < afternoonPeriodsCount; i++, count++)
                    {
                        day.Periods[i - 1].Course = day.Periods[i].Course;
                        day.Periods[i - 1].Instructor = day.Periods[i].Instructor;
                        day.Periods[i - 1].Room = day.Periods[i].Room;
                        if (day.Periods[i].IsLecture)
                            day.Periods[i - 1].IsLecture = true;
                        else if (day.Periods[i].IsLab)
                            day.Periods[i - 1].IsLab = true;
                        else if (day.Periods[i].IsTutor)
                        {
                            day.Periods[i - 1].IsTutor = true;
                        }

                        day.Periods[i].Course = null;
                        day.Periods[i].Instructor = null;
                        day.Periods[i].Room = null;
                        day.Periods[i].IsLecture = false;
                        day.Periods[i].IsLab = false;
                        day.Periods[i].IsTutor = false;
                    }
                }
            }
        }
        public Schedule Crossover(Schedule parentB)
        {
            #region Crossover function
            //var lectureDictionary = new Dictionary<string, byte>();
            //var labDictionary = new Dictionary<string, byte>();
            //var tutorDictionary = new Dictionary<string, byte>();

            //List<CourseOffering> courseOfferings = _context.CourseOfferings.Where(c => c.SectionId == Section.Id).ToList();

            //foreach (var courseOffering in courseOfferings)
            //{
            //    lectureDictionary.Add(courseOffering.Course.Title, courseOffering.Course.Lecture);
            //    labDictionary.Add(courseOffering.Course.Title, courseOffering.Course.Laboratory);
            //    tutorDictionary.Add(courseOffering.Course.Title, courseOffering.Course.Tutor); 
            //}
            //var child = new Schedule
            //{
            //    Section = this.Section
            //};

            //Random rand = new Random();

            ////var parentADictionary = new Dictionary<string, List<Dictionary<byte, byte>>>();
            ////var parentBDictionary = new Dictionary<string, List<Dictionary<byte, byte>>>(); 

            //for (int i = 0; i < GlobalConfig.NUM_OF_DAYS; i++)
            //{
            //    byte parentAOrB = (byte)rand.Next(0, 2);
            //    for (int j = 0; j < GlobalConfig.NUM_OF_PERIODS; j++)
            //    {

            //        switch (parentAOrB)
            //        {
            //            case 0:
            //                if (this.Days[i].Periods[j].IsLecture && lectureDictionary[this.Days[i].Periods[j].Course.Title] > 0)
            //                {
            //                    child.Days[i].Periods[j] = this.Days[i].Periods[j];
            //                    lectureDictionary[this.Days[i].Periods[j].Course.Title]--;
            //                }
            //                else if (this.Days[i].Periods[j].IsLab && labDictionary[this.Days[i].Periods[j].Course.Title] > 0)
            //                {
            //                    child.Days[i].Periods[j] = this.Days[i].Periods[j];
            //                    labDictionary[this.Days[i].Periods[j].Course.Title]--;
            //                }
            //                else if (this.Days[i].Periods[j].IsTutor && tutorDictionary[this.Days[i].Periods[j].Course.Title] > 0)
            //                {
            //                    child.Days[i].Periods[j] = this.Days[i].Periods[j];
            //                    tutorDictionary[this.Days[i].Periods[j].Course.Title]--;
            //                }
            //                break;
            //            case 1:
            //                if (parentB.Days[i].Periods[j].IsLecture && lectureDictionary[parentB.Days[i].Periods[j].Course.Title] > 0)
            //                {
            //                    child.Days[i].Periods[j] = parentB.Days[i].Periods[j];
            //                    lectureDictionary[parentB.Days[i].Periods[j].Course.Title]--;
            //                }
            //                else if (parentB.Days[i].Periods[j].IsLab && labDictionary[parentB.Days[i].Periods[j].Course.Title] > 0)
            //                {
            //                    child.Days[i].Periods[j] = parentB.Days[i].Periods[j];
            //                    labDictionary[parentB.Days[i].Periods[j].Course.Title]--;
            //                }
            //                else if (parentB.Days[i].Periods[j].IsTutor && tutorDictionary[parentB.Days[i].Periods[j].Course.Title] > 0)
            //                {
            //                    child.Days[i].Periods[j] = parentB.Days[i].Periods[j];
            //                    tutorDictionary[parentB.Days[i].Periods[j].Course.Title]--;
            //                }
            //                break; 
            //        }
            //        #region Unimportant Code
            //        //if (this.Days[i].Periods[j].Course != null)
            //        //{
            //        //    if (parentADictionary[Days[i].Periods[j].Course.Title] == null)
            //        //    {
            //        //        var dict = new Dictionary<byte, byte>();
            //        //        dict.Add(this.Days[i].DayNumber, this.Days[i].Periods[j].Period);
            //        //        var list = new List<Dictionary<byte, byte>>();
            //        //        list.Add(dict);
            //        //        parentADictionary.Add(this.Days[i].Periods[j].Course.Title, list);
            //        //    }
            //        //    else
            //        //    {
            //        //        var dict = new Dictionary<byte, byte>();
            //        //        dict.Add(this.Days[i].DayNumber, this.Days[i].Periods[j].Period);
            //        //        parentBDictionary[parentB.Days[i].Periods[j].Course.Title].Add(dict);
            //        //    }
            //        //}
            //        //else if (parentB.Days[i].Periods[j].Course != null)
            //        //{
            //        //    if (parentBDictionary[Days[i].Periods[j].Course.Title] == null)
            //        //    {
            //        //        var dict = new Dictionary<byte, byte>();
            //        //        dict.Add(parentB.Days[i].DayNumber, parentB.Days[i].Periods[j].Period);
            //        //        var list = new List<Dictionary<byte, byte>>();
            //        //        list.Add(dict);
            //        //        parentBDictionary.Add(parentB.Days[i].Periods[j].Course.Title, list);
            //        //    }
            //        //    else
            //        //    {
            //        //        var dict = new Dictionary<byte, byte>();
            //        //        dict.Add(parentB.Days[i].DayNumber, parentB.Days[i].Periods[j].Period);
            //        //        parentBDictionary[parentB.Days[i].Periods[j].Course.Title].Add(dict);
            //        //    }
            //        //}   
            //        #endregion
            //    }
            //}

            //var remainingLectureKeys = lectureDictionary.Where(s => s.Value > 0).Select(s => s.Key).ToList();
            //var remainingLabKeys = labDictionary.Where(s => s.Value > 0).Select(s => s.Key).ToList();
            //var remainingTutorKeys = tutorDictionary.Where(s => s.Value > 0).Select(s => s.Key).ToList();


            //while (remainingLectureKeys.Count() > 0 || remainingLabKeys.Count() > 0 || remainingTutorKeys.Count() > 0)
            //{
            //    foreach (var courseTitle in remainingLectureKeys)
            //    {
            //        if (lectureDictionary[courseTitle] > 1)
            //        {
            //            var room = Section.AssignedRooms.SingleOrDefault(r => r.IsLectureRoom == true);

            //            byte randDay = (byte)rand.Next(GlobalConfig.NUM_OF_DAYS);
            //            byte randPeriod = (byte)rand.Next(GlobalConfig.NUM_OF_PERIODS);

            //            byte[] slots = FindConsecutiveSlotsForChild(child, randDay, randPeriod, 2);

            //            if (slots.Where(b => b == 0).Count() <= 1)
            //            {
            //                var entry = child.Days[randDay].Periods[slots[0]];
            //                var courseOffering = Section.CourseOfferings.Single(s => s.Course.Title == courseTitle);
            //                entry.Course = courseOffering.Course;
            //                entry.Instructor = courseOffering.Instructor;
            //                entry.Room = room;
            //                entry.IsLecture = true;

            //                entry = Days[randDay].Periods[slots[1]];
            //                entry.Course = courseOffering.Course;
            //                entry.Instructor = courseOffering.Instructor;
            //                entry.Room = room;
            //                entry.IsLecture = true;

            //                lectureDictionary[courseTitle] -= 2; 
            //            }
            //        }
            //        else
            //        {
            //            var room = Section.AssignedRooms.SingleOrDefault(r => r.IsLectureRoom == true);

            //            byte randDay = (byte)rand.Next(GlobalConfig.NUM_OF_DAYS);
            //            byte randPeriod = (byte)rand.Next(GlobalConfig.NUM_OF_PERIODS);

            //            if (child.Days[randDay].Periods[randPeriod].Course == null)
            //            {
            //                var entry = child.Days[randDay].Periods[randPeriod];
            //                var courseOffering = Section.CourseOfferings.Single(s => s.Course.Title == courseTitle);
            //                entry.Course = courseOffering.Course;
            //                entry.Instructor = courseOffering.Instructor;
            //                entry.Room = room;
            //                entry.IsLecture = true;

            //                lectureDictionary[courseTitle] -= 1;
            //            }
            //        }
            //    }

            //    foreach (var courseTitle in remainingLabKeys)
            //    {
            //        if (labDictionary[courseTitle] > 2)
            //        {
            //            var room = Section.AssignedRooms.SingleOrDefault(r => r.IsLabRoom == true);

            //            byte randDay = (byte)rand.Next(GlobalConfig.NUM_OF_DAYS);
            //            byte randPeriod = (byte)rand.Next(GlobalConfig.NUM_OF_PERIODS);

            //            byte[] slots = FindConsecutiveSlotsForChild(child, randDay, randPeriod, 3);

            //            if (slots.Where(b => b == 0).Count() <= 1)
            //            {
            //                var entry = child.Days[randDay].Periods[slots[0]];
            //                var courseOffering = Section.CourseOfferings.Single(s => s.Course.Title == courseTitle);
            //                entry.Course = courseOffering.Course;
            //                entry.Instructor = courseOffering.Instructor;
            //                entry.Room = room;
            //                entry.IsLab = true;

            //                entry = Days[randDay].Periods[slots[1]];
            //                entry.Course = courseOffering.Course;
            //                entry.Instructor = courseOffering.Instructor;
            //                entry.Room = room;
            //                entry.IsLab = true;

            //                entry = Days[randDay].Periods[slots[2]];
            //                entry.Course = courseOffering.Course;
            //                entry.Instructor = courseOffering.Instructor;
            //                entry.Room = room;
            //                entry.IsLab = true;

            //                labDictionary[courseTitle] -= 3;

            //            }
            //        }
            //        else if (labDictionary[courseTitle] > 1)
            //        {
            //            var room = Section.AssignedRooms.SingleOrDefault(r => r.IsLabRoom == true);

            //            byte randDay = (byte)rand.Next(GlobalConfig.NUM_OF_DAYS);
            //            byte randPeriod = (byte)rand.Next(GlobalConfig.NUM_OF_PERIODS);

            //            byte[] slots = FindConsecutiveSlotsForChild(child, randDay, randPeriod, 2);

            //            if (slots.Where(b => b == 0).Count() <= 1)
            //            {
            //                var entry = child.Days[randDay].Periods[slots[0]];
            //                var courseOffering = Section.CourseOfferings.Single(s => s.Course.Title == courseTitle);
            //                entry.Course = courseOffering.Course;
            //                entry.Instructor = courseOffering.Instructor;
            //                entry.Room = room;
            //                entry.IsLab = true;

            //                entry = Days[randDay].Periods[slots[1]];
            //                entry.Course = courseOffering.Course;
            //                entry.Instructor = courseOffering.Instructor;
            //                entry.Room = room;
            //                entry.IsLab = true;

            //                labDictionary[courseTitle] -= 2;
            //            }
            //        }
            //        else
            //        {
            //            var room = Section.AssignedRooms.SingleOrDefault(r => r.IsLabRoom == true);

            //            byte randDay = (byte)rand.Next(GlobalConfig.NUM_OF_DAYS);
            //            byte randPeriod = (byte)rand.Next(GlobalConfig.NUM_OF_PERIODS);

            //            if (child.Days[randDay].Periods[randPeriod].Course == null)
            //            {
            //                var entry = child.Days[randDay].Periods[randPeriod];
            //                var courseOffering = Section.CourseOfferings.Single(s => s.Course.Title == courseTitle);
            //                entry.Course = courseOffering.Course;
            //                entry.Instructor = courseOffering.Instructor;
            //                entry.Room = room;
            //                entry.IsLab = true;

            //                labDictionary[courseTitle] -= 1;
            //            }
            //        }
            //    }

            //    foreach (var courseTitle in remainingTutorKeys)
            //    {
            //        if (tutorDictionary[courseTitle] > 1)
            //        {
            //            var room = Section.AssignedRooms.SingleOrDefault(r => r.IsLectureRoom == true);

            //            byte randDay = (byte)rand.Next(GlobalConfig.NUM_OF_DAYS);
            //            byte randPeriod = (byte)rand.Next(GlobalConfig.NUM_OF_PERIODS);

            //            byte[] slots = FindConsecutiveSlotsForChild(child, randDay, randPeriod, 2);

            //            if (slots.Where(b => b == 0).Count() <= 1)
            //            {
            //                var entry = child.Days[randDay].Periods[slots[0]];
            //                var courseOffering = Section.CourseOfferings.Single(s => s.Course.Title == courseTitle);
            //                entry.Course = courseOffering.Course;
            //                entry.Instructor = courseOffering.Instructor;
            //                entry.Room = room;
            //                entry.IsTutor = true;

            //                entry = Days[randDay].Periods[slots[1]];
            //                entry.Course = courseOffering.Course;
            //                entry.Instructor = courseOffering.Instructor;
            //                entry.Room = room;
            //                entry.IsTutor = true;

            //                tutorDictionary[courseTitle] -= 2;
            //            }
            //        }
            //        else
            //        {
            //            var room = Section.AssignedRooms.SingleOrDefault(r => r.IsLectureRoom == true);

            //            byte randDay = (byte)rand.Next(GlobalConfig.NUM_OF_DAYS);
            //            byte randPeriod = (byte)rand.Next(GlobalConfig.NUM_OF_PERIODS);

            //            if (child.Days[randDay].Periods[randPeriod].Course == null)
            //            {
            //                var entry = child.Days[randDay].Periods[randPeriod];
            //                var courseOffering = Section.CourseOfferings.Single(s => s.Course.Title == courseTitle);
            //                entry.Course = courseOffering.Course;
            //                entry.Instructor = courseOffering.Instructor;
            //                entry.Room = room;
            //                entry.IsTutor = true;

            //                tutorDictionary[courseTitle] -= 1;
            //            }
            //        }
            //    }

            //    remainingLectureKeys = lectureDictionary.Where(s => s.Value > 0).Select(s => s.Key).ToList();
            //    remainingLabKeys = labDictionary.Where(s => s.Value > 0).Select(s => s.Key).ToList();
            //    remainingTutorKeys = tutorDictionary.Where(s => s.Value > 0).Select(s => s.Key).ToList();
            //}

            //return child;  
            #endregion
            Random rand = new Random();

            //if (rand.NextDouble() <= GlobalConfig.CROSSOVER_RATE)
            //{
            //    var child = new Schedule(true)
            //    {
            //        Section = this.Section,
            //        scheduleEntries = this.scheduleEntries
            //    };

            //    var parentA = this;



            //    var parentAOrBMwfDays = DetermineWhichParentIsBetterForMwfDays(this, parentB);
            //    var parentAOrBTthDays = DetermineWhichParentIsBetterForTthDays(this, parentB);

            //    switch (parentAOrBMwfDays)
            //    {
            //        case 0:
            //            child.Days[0] = parentA.Days[0];
            //            child.Days[2] = parentA.Days[2];
            //            child.Days[4] = parentA.Days[4];
            //            break;
            //        case 1:
            //            child.Days[0] = parentB.Days[0];
            //            child.Days[2] = parentB.Days[2];
            //            child.Days[4] = parentB.Days[4];
            //            break;
            //    }

            //    switch (parentAOrBTthDays)
            //    {
            //        case 0:
            //            child.Days[1] = parentA.Days[1];
            //            child.Days[3] = parentA.Days[3];
            //            break;
            //        case 1:
            //            child.Days[1] = parentB.Days[1];
            //            child.Days[3] = parentB.Days[3];
            //            break;
            //    }

            //    return child; 
            //}
            //else
            //{
            //    return this;
            //}

            var child = new Schedule(true)
            {
                Section = this.Section,
                scheduleEntries = this.scheduleEntries
            };

            var parentA = this;

            var parentAOrB = rand.Next(2);

            switch (parentAOrB)
            {
                case 0:
                    child.Days[0] = parentA.Days[0];
                    child.Days[2] = parentA.Days[2];
                    child.Days[4] = parentA.Days[4];
                    child.Days[1] = parentB.Days[1];
                    child.Days[3] = parentB.Days[3];
                    break;
                case 1:
                    child.Days[0] = parentB.Days[0];
                    child.Days[2] = parentB.Days[2];
                    child.Days[4] = parentB.Days[4];
                    child.Days[1] = parentA.Days[1];
                    child.Days[3] = parentA.Days[3];
                    break; 
            }

            return child;
        }

        private byte DetermineWhichParentIsBetterForMwfDays(Schedule parentA, Schedule parentB)
        {
            var mwfDays = new byte[] { 0, 2, 4 };
            

            var parentAMwfConflicts = 0;
            var parentBMwfConflicts = 0;
            

            foreach (var day in mwfDays)
            {
                parentAMwfConflicts += CalculateFitnessForADay(parentA.Days[day]);
                parentBMwfConflicts += CalculateFitnessForADay(parentB.Days[day]);
            }

            if (parentAMwfConflicts > parentBMwfConflicts)
            {
                return 1;
            } 
            else
            {
                return 0;
            }
        }

        private byte DetermineWhichParentIsBetterForTthDays(Schedule parentA, Schedule parentB)
        {
            var tthDays = new byte[] { 1, 3 };

            var parentATthConflicts = 0;
            var parentBTthConflicts = 0;

            foreach (var day in tthDays)
            {
                parentATthConflicts += CalculateFitnessForADay(parentA.Days[day]);
                parentBTthConflicts += CalculateFitnessForADay(parentB.Days[day]);
            }

            if (parentATthConflicts > parentBTthConflicts)
            {
                return 1; 
            }
            else
            {
                return 0;
            }
        }

        public void CalculateFitness()
        {
            #region Old Fitness function
            //int score = 0;
            //int maximumScore = 0;

            //byte totalLectureHours = 0;
            //byte totalHours = 0;

            //byte threeLabHourCoursesCount = 0;


            //foreach (var courseOffering in Section.CourseOfferings)
            //{
            //    totalLectureHours += courseOffering.Course.Lecture;
            //    totalHours += courseOffering.Course.Lecture;
            //    totalHours += courseOffering.Course.Laboratory;
            //    totalHours += courseOffering.Course.Tutor;

            //    if (courseOffering.Course.Laboratory >= 3)
            //    {
            //        threeLabHourCoursesCount++; 
            //    }
            //}

            //foreach (var day in Days)
            //{
            //    score += AssignScoreBasedOnScheduleStartingOnFirstPeriod(day);
            //    maximumScore += 2;

            //    //score += AssignScoreBasedOnLunchBreakGaps(day);
            //    //maximumScore += 5;

            //    score += AssignScoreBasedOnGapBetweenEntries(day);
            //    maximumScore += 2;

            //    //if (totalLectureHours < 30)
            //    //{
            //    //    score += AssignScoreBasedOnFreeEntryInTheLastPeriod(day);
            //    //    maximumScore += 2;
            //    //}

            //    //if (totalLectureHours < 20)
            //    //{
            //    //    score += AssignScoreBasedOnLectureBeingInTheMorning(day);
            //    //}

            //    score += AssignScoreBasedOnPerforation(day);
            //    maximumScore += 2;

            //    score += AssignScoreBasedOnThreeConsecutiveLabEntries(day); 
            //}
            //// Increase maximumScore by the number of courses that have 3 lab hours 
            //maximumScore += 2*threeLabHourCoursesCount;

            //score += AssignScoreBasedOnInstructorBeingFree();
            //maximumScore += totalHours;

            //score += AssignScoreBasedOnRoomBeingFree();
            //maximumScore += totalHours; 

            ////if (totalLectureHours < 20)
            ////{
            ////    maximumScore += totalLectureHours;
            ////}








            //Fitness = Math.Pow(score, 2);
            //MaximumScore = Math.Pow(maximumScore, 2);
            //Fitness = Fitness / MaximumScore;
            #endregion

            #region New Fitness Function
            int numOfConflicts = 0;

            foreach (var day in Days)
            {
                //// This checks if the schedule starts first thing in the morning
                //if (!ScheduleHelper.DoesScheduleStartOnFirstPeriodInTheMorning(day))
                //{
                //    numOfConflicts++;
                //}

                //// This checks if the schedule starts first thing in the afternoon
                //if (!ScheduleHelper.DoesScheduleStartOnFirstPeriodInTheAfternoon(day))
                //{
                //    numOfConflicts++;
                //}

                // This checks if there is a free gap between periods in the morning
                if (ScheduleHelper.IsThereAGapBetweenEntriesInTheMorning(day))
                {
                    numOfConflicts++;
                }

                // This checks if there is a free gap between periods in the afternoon
                if (ScheduleHelper.IsThereAGapBetweenEntriesInTheAfternoon(day))
                {
                    numOfConflicts++;
                }

                // This checks if the lecture entries spans more than 2 periods
                if (ScheduleHelper.AreThereMoreThanTwoConsecutiveLectureEntriesInTheMorning(day))
                {
                    numOfConflicts++;
                }
                if (ScheduleHelper.AreThereMoreThanTwoConsecutiveLectureEntriesInTheAfternoon(day))
                {
                    numOfConflicts++;
                }


                //Does the instructor want this day
                //numOfConflicts += ScheduleHelper.CountInstructorPreferenceConflictsForThisDay(day);


                numOfConflicts += ScheduleHelper.HowManyTimesIsSchedulePerforatedInTheMorning(day);
                numOfConflicts += ScheduleHelper.HowManyTimesIsSchedulePerforatedInTheAfternoon(day);

                numOfConflicts += ScheduleHelper.CountConflictsBasedOnThreeConsecutiveLabEntriesInTheMorning(day);
                numOfConflicts += ScheduleHelper.CountConflictsBasedOnThreeConsecutiveLabEntriesInTheAfternoon(day);


            }

            numOfConflicts += CountConflictsBasedOnInstructorBeingFree();
            numOfConflicts += CountConflictsBasedOnRoomBeingFree();

            Fitness = 1.0 / (1 + numOfConflicts);
            #endregion
        }

        private byte CountConflictsBasedOnInstructorBeingFree()
        {
            byte conflicts = 0;

            for (int i = 0; i < GlobalConfig.NUM_OF_DAYS; i++)
            {
                byte day = this.Days[i].DayNumber;

                for (int j = 0; j < GlobalConfig.NUM_OF_PERIODS; j++)
                {
                    byte period = this.Days[i].Periods[j].Period;

                    var currentEntry = this.Days[i].Periods[j];

                    if (currentEntry.Course != null)
                    {
                        bool isThereAClash = scheduleEntries.Any(s => s.Day.DayNumber == day
                                                                    && s.Period == period
                                                                    && s.InstructorId == currentEntry.Instructor.Id);
                        if (isThereAClash)
                        {
                            conflicts++;
                        }
                    }
                }
            }
            return conflicts;
        }

        private byte CountConflictsBasedOnRoomBeingFree()
        {
            byte conflicts = 0;

            for (int i = 0; i < GlobalConfig.NUM_OF_DAYS; i++)
            {
                byte day = this.Days[i].DayNumber;

                for (int j = 0; j < GlobalConfig.NUM_OF_PERIODS; j++)
                {
                    byte period = this.Days[i].Periods[j].Period;

                    var currentEntry = this.Days[i].Periods[j];

                    if (currentEntry.Course != null)
                    {
                        bool isThereAClash = scheduleEntries.Any(s => s.Day.DayNumber == day
                                                                    && s.Period == period
                                                                    && s.RoomId == currentEntry.Room.Id);
                        if (isThereAClash)
                        {
                            conflicts++;
                        }
                    }
                }
            }
            return conflicts;
        }
        private byte AssignScoreBasedOnRoomBeingFree()
        {
            byte score = 0;

            

            for (int i = 0; i < GlobalConfig.NUM_OF_DAYS; i++)
            {
                byte day = this.Days[i].DayNumber;

                for (int j = 0; j < GlobalConfig.NUM_OF_PERIODS; j++)
                {
                    byte period = this.Days[i].Periods[j].Period;

                    var currentEntry = this.Days[i].Periods[j];

                    if (currentEntry.Course != null)
                    {
                        bool isThereAClash = scheduleEntries.Any(s => s.Day.DayNumber == day
                                                                    && s.Period == period
                                                                    && s.RoomId == currentEntry.Room.Id);
                        if (!isThereAClash)
                        {
                            score++;
                        }
                    }
                }
            }
            return score;
        }

        private byte AssignScoreBasedOnInstructorBeingFree()
        {
            byte score = 0;

            for (int i = 0; i < GlobalConfig.NUM_OF_DAYS; i++)
            {
                byte day = this.Days[i].DayNumber;

                for (int j = 0; j < GlobalConfig.NUM_OF_PERIODS; j++)
                {
                    byte period = this.Days[i].Periods[j].Period; 

                    var currentEntry = this.Days[i].Periods[j];

                    if (currentEntry.Course != null)
                    {
                        bool isThereAClash = scheduleEntries.Any(s => s.Day.DayNumber == day
                                                                    && s.Period == period
                                                                    && s.InstructorId == currentEntry.Instructor.Id);
                        if (!isThereAClash)
                        {
                            score++;
                        }
                    }
                }
            }
            return score;  
        }

        private byte AssignScoreBasedOnThreeConsecutiveLabEntries(Day day)
        {
            byte score = 0;
            var labPeriods = day.Periods.Where(s => s.IsLab).ToList();

            var dictionary = CreatePeriodDictionary(labPeriods);

            foreach (var courseTitle in dictionary.Keys)
            {
                int labPeriodsCount = dictionary[courseTitle].Count;
                if (labPeriodsCount == 3)
                {
                    int difference = dictionary[courseTitle][labPeriodsCount - 1] - dictionary[courseTitle][0];
                    if (difference < labPeriodsCount)
                    {
                        score+=2;
                    }
                }
            }
            return score; 
        }
        private byte AssignScoreBasedOnGapBetweenEntries(Day day)
        {
            byte score = 0;

            //byte morningStartIndex = 0;
            //byte afternoonStartIndex = 4;

            //var morningPeriods = day.Periods.GetRange(morningStartIndex, 4).ToList();
            //var afternoonPeriods = day.Periods.GetRange(afternoonStartIndex, 4).ToList();

            //var morningPeriodsCount = morningPeriods.Where(s => s.Course != null).ToList().Count;
            //var afternoonPeriodsCount = afternoonPeriods.Where(s => s.Course != null).ToList().Count;

            //if (morningPeriodsCount > 1)
            //{
            //    var first = morningPeriods.First(x => x.Course != null);
            //    var last = morningPeriods.Last(x => x.Course != null);

            //    var firstIndex = morningPeriods.IndexOf(first);
            //    var lastIndex = morningPeriods.IndexOf(last);

            //    if (!isPerforated(morningPeriods.GetRange(firstIndex, (lastIndex - firstIndex) + 1)))
            //    {
            //        score++;
            //    }
            //}
            //else
            //{
            //    score++;
            //}

            //if (afternoonPeriodsCount > 1)
            //{
            //    var first = afternoonPeriods.First(x => x.Course != null);
            //    var last = afternoonPeriods.Last(x => x.Course != null);

            //    var firstIndex = afternoonPeriods.IndexOf(first);
            //    var lastIndex = afternoonPeriods.IndexOf(last);

            //    if (!isPerforated(afternoonPeriods.GetRange(firstIndex, (lastIndex - firstIndex) + 1)))
            //    {
            //        score++;
            //    }
            //}
            //else
            //{
            //    score++;
            //}


            return score;
        }
        
        private byte AssignScoreBasedOnFreeEntryInTheLastPeriod()
        {
            byte score = 0;

            foreach (var day in Days)
            {
                if (day.Periods[3].Course == null)
                {
                    score++;    
                }

                if (day.Periods[7].Course == null)
                {
                    score++; 
                }
            }
            return score; 
        }
        private byte AssignScoreBasedOnLectureBeingInTheMorning()
        {
            
            byte score = 0;

            foreach (var day in Days)
            {
                foreach (var item in day.Periods.GetRange(0, 4))
                {
                    if (item.Course != null && item.IsLecture)
                    {
                        score++; 
                    }
                }
            }

            return score; 
        }
        private byte AssignScoreBasedOnPerforation(Day day)
        {
            byte score = 0;

            byte morningStartIndex = 0;
            byte afternoonStartIndex = 4;

            
            var morningPeriods = day.Periods.GetRange(morningStartIndex, 4).Where(s => s.Course != null).ToList();
            var afternoonPeriods = day.Periods.GetRange(afternoonStartIndex, 4).Where(s => s.Course != null).ToList();

            Dictionary<string, List<byte>> dictionary = CreatePeriodDictionary(morningPeriods);

            bool perforated = IsPerforated(dictionary);
            if (!perforated)
            {
                score++;
            }

            dictionary = CreatePeriodDictionary(afternoonPeriods);

            perforated = IsPerforated(dictionary);
            if (!perforated)
            {
                score++;
            }
            

            return score; 
        }
        private bool IsTheSameCourseScheduledOnFourthAndFithPeriods(Day day)
        {
            if (day.Periods[3].Course != null && day.Periods[4].Course != null && day.Periods[3].Course.Title == day.Periods[4].Course.Title)
            {
                return true;
            }
 
            return false; 
        }
        private byte AssignScoreBasedOnLunchBreakGaps(Day day)
        {
            byte score = 0;

            
            if (day.Periods[3].Course != null && day.Periods[4].Course != null && day.Periods[3].Course.Title != day.Periods[4].Course.Title)
            {
                score++; 
            }
            else if (day.Periods[3].Course == null || day.Periods[4].Course == null)
            {
                score++; 
            }
            
            return score; 
        }
        
        private byte AssignScoreBasedOnScheduleStartingOnFirstPeriod(Day day)
        {
            byte morningStartIndex = 0;
            byte afternoonStartIndex = 4;

            byte score = 0;

           
            if (day.Periods[morningStartIndex].Course != null)
            {
                score++;  
            }
            if (day.Periods[afternoonStartIndex].Course != null)
            {
                score++; 
            }
            

            return score; 
        }
        private static Dictionary<string, List<byte>> CreatePeriodDictionary(List<ScheduleEntry> periods)
        {
            Dictionary<string, List<byte>> dictionary = new Dictionary<string, List<byte>>();
            
            foreach (var period in periods)
            {
                if (dictionary.Keys.Contains(period.Course.Title))
                {
                    dictionary[period.Course.Title].Add((byte)period.Period);
                }
                else
                {
                    var list = new List<byte>();
                    list.Add((byte)period.Period);
                    dictionary.Add(period.Course.Title, list);
                }

            }

            return dictionary;
        }
        private static bool IsPerforated(Dictionary<string, List<byte>> dictionary)
        {
            bool isPerforated = true;
            foreach (var courseTitle in dictionary.Keys)
            {
                var periods = dictionary[courseTitle];

                int difference = periods[periods.Count - 1] - periods[0];

                if (difference < periods.Count)
                {
                    isPerforated = false;
                }
                else
                {
                    isPerforated = true;
                    break;
                }
            }

            return isPerforated;
        }
        private void InitializeSchedule()
        {
            for (byte i = 0; i < GlobalConfig.NUM_OF_DAYS; i++)
            {
                var day = new Day
                {
                    DayNumber = i,
                    DayName = GetDayName(i),
                    //Schedule = this
                };
                for (byte j = 0; j < GlobalConfig.NUM_OF_PERIODS; j++)
                {
                    var scheduleEntry = new ScheduleEntry
                    {
                        Period = j
                    };
                    day.Periods.Add(scheduleEntry); 
                }

                Days.Add(day); 
            }
        }
        private string GetDayName(int dayNumber)
        {
            switch (dayNumber)
            {
                case 0:
                    return "Monday";
                case 1:
                    return "Tuesday";
                case 2:
                    return "Wednesday";
                case 3:
                    return "Thursday";
                case 4:
                    return "Friday";
                default:
                    return "Other";
            }
        }
        private byte[] FindConsecutiveSlots(byte randDay, byte randPeriod, byte size)
        {
            byte[] slots = new byte[size];

            byte min = 0;
            byte max = GlobalConfig.NUM_OF_PERIODS - 1;

            if (randPeriod == min)
            {
                var scheduleEntries = Days[randDay].Periods.GetRange(randPeriod, size);

                if (scheduleEntries.Where(s => s.Course == null).Count() == size)
                {
                    for (byte i = 0; i < size; i++)
                    {
                        slots[i] = (byte)(randPeriod + i);
                    }

                    return slots; 
                }
            }
            else if (randPeriod == max)
            {
                var scheduleEntries = Days[randDay].Periods.GetRange(randPeriod - (size - 1), size);

                if (scheduleEntries.Where(s => s.Course == null).Count() == size)
                {
                    for (byte i = 0; i < size; i++)
                    {
                        slots[i] = (byte)(randPeriod - i);
                    }

                    return slots; 
                }
            }
            else
            {
                if (size > 2)
                {
                    if (randPeriod - (size - 1) >= min && randPeriod + (size - 1) <= max)
                    {
                        var scheduleEntriesLeft = Days[randDay].Periods.GetRange(randPeriod - (size - 1), size);
                        var scheduleEntriesRight = Days[randDay].Periods.GetRange(randPeriod, size);

                        if (scheduleEntriesLeft.Where(s => s.Course == null).Count() == size)
                        {
                            for (byte i = 0; i < size; i++)
                            {
                                slots[i] = (byte)(randPeriod - (size - 1 - i));
                            }
                            return slots; 
                        }
                        else if (scheduleEntriesRight.Where(s => s.Course == null).Count() == size)
                        {
                            for (byte i = 0; i < size; i++)
                            {
                                slots[i] = (byte)(randPeriod + i);
                            }
                            return slots; 
                        }

                    }
                    else if (randPeriod - (size - 2) >= min && randPeriod + (size - 2) <= max)
                    {
                        var range = Days[randDay].Periods.GetRange(randPeriod - 1, size);

                        if (range.Where(s => s.Course == null).Count() == 3)
                        {
                            for (byte i = 0; i < size; i++)
                            {
                                slots[i] = (byte)(randPeriod - 1 + i);
                            }

                            return slots; 
                        }
                    }
                }
                else
                {
                    var scheduleEntriesLeft = Days[randDay].Periods.GetRange(randPeriod - (size - 1), size);
                    var scheduleEntriesRight = Days[randDay].Periods.GetRange(randPeriod, size);

                    if (scheduleEntriesLeft.Where(s => s.Course == null).Count() == size)
                    {
                        for (byte i = 0; i < size; i++)
                        {
                            slots[i] = (byte)(randPeriod - i);
                        }

                        return slots; 
                    }
                    else if (scheduleEntriesRight.Where(s => s.Course == null).Count() == size)
                    {
                        for (byte i = 0; i < size; i++)
                        {
                            slots[i] = (byte)(randPeriod + i);
                        }

                        return slots; 
                    }
                }
            }


            return slots; 
        }
        private byte[] FindConsecutiveSlotsForChild(Schedule child, byte randDay, byte randPeriod, byte size)
        {
            byte[] slots = new byte[size];

            byte min = 0;
            byte max = GlobalConfig.NUM_OF_PERIODS - 1;

            if (randPeriod == min)
            {
                var scheduleEntries = child.Days[randDay].Periods.GetRange(randPeriod, size);

                if (scheduleEntries.Where(s => s.Course == null).Count() == size)
                {
                    for (byte i = 0; i < size; i++)
                    {
                        slots[i] = (byte)(randPeriod + i);
                    }

                    return slots;
                }
            }
            else if (randPeriod == max)
            {
                var scheduleEntries = child.Days[randDay].Periods.GetRange(randPeriod - (size - 1), size);

                if (scheduleEntries.Where(s => s.Course == null).Count() == size)
                {
                    for (byte i = 0; i < size; i++)
                    {
                        slots[i] = (byte)(randPeriod - i);
                    }

                    return slots;
                }
            }
            else
            {
                if (size > 2)
                {
                    if (randPeriod - (size - 1) >= min && randPeriod + (size - 1) <= max)
                    {
                        var scheduleEntriesLeft = child.Days[randDay].Periods.GetRange(randPeriod - (size - 1), size);
                        var scheduleEntriesRight = child.Days[randDay].Periods.GetRange(randPeriod, size);

                        if (scheduleEntriesLeft.Where(s => s.Course == null).Count() == size)
                        {
                            for (byte i = 0; i < size; i++)
                            {
                                slots[i] = (byte)(randPeriod - (size - 1 - i));
                            }
                            return slots;
                        }
                        else if (scheduleEntriesRight.Where(s => s.Course == null).Count() == size)
                        {
                            for (byte i = 0; i < size; i++)
                            {
                                slots[i] = (byte)(randPeriod + i);
                            }
                            return slots;
                        }

                    }
                    else if (randPeriod - (size - 2) >= min && randPeriod + (size - 2) <= max)
                    {
                        var range = child.Days[randDay].Periods.GetRange(randPeriod - 1, size);

                        if (range.Where(s => s.Course == null).Count() == 3)
                        {
                            for (byte i = 0; i < size; i++)
                            {
                                slots[i] = (byte)(randPeriod - 1 + i);
                            }

                            return slots;
                        }
                    }
                }
                else
                {
                    var scheduleEntriesLeft = child.Days[randDay].Periods.GetRange(randPeriod - (size - 1), size);
                    var scheduleEntriesRight = child.Days[randDay].Periods.GetRange(randPeriod, size);

                    if (scheduleEntriesLeft.Where(s => s.Course == null).Count() == size)
                    {
                        for (byte i = 0; i < size; i++)
                        {
                            slots[i] = (byte)(randPeriod - i);
                        }

                        return slots;
                    }
                    else if (scheduleEntriesRight.Where(s => s.Course == null).Count() == size)
                    {
                        for (byte i = 0; i < size; i++)
                        {
                            slots[i] = (byte)(randPeriod + i);
                        }

                        return slots;
                    }
                }
            }


            return slots;
        }
        public void PrintSchedule()
        {
            foreach (var day in Days)
            {
                foreach (var period in day.Periods)
                {
                    if (period.Course != null)
                    {
                        Debug.Write(period.Course.Title + "(" + period.Period + ")");
                        if (period.IsLab)
                            Debug.Write("(Lab)");
                        if (period.IsLecture)
                            Debug.Write("(Lecture)");
                        if (period.IsTutor)
                            Debug.Write("(Tutor)");
                        Debug.Write("|");
                    }
                    else
                    {
                        Debug.Write("\t|");
                    }
                        


                    

                }
                Debug.WriteLine("");
            }
            Debug.WriteLine(""); 
        }

        public bool IsThereAnyClash()
        {

            for (int i = 0; i < GlobalConfig.NUM_OF_DAYS; i++)
            {
                byte day = this.Days[i].DayNumber;

                for (int j = 0; j < GlobalConfig.NUM_OF_PERIODS; j++)
                {
                    byte period = this.Days[i].Periods[j].Period;

                    var currentEntry = this.Days[i].Periods[j];

                    if (currentEntry.Course != null)
                    {
                        bool isThereAnInstructorClash = scheduleEntries.Any(s => s.Day.DayNumber == day
                                                                    && s.Period == period
                                                                    && s.InstructorId == currentEntry.Instructor.Id);

                        bool isThereARoomClash = scheduleEntries.Any(s => s.Day.DayNumber == day
                                                                    && s.Period == period
                                                                    && s.RoomId == currentEntry.Room.Id);

                        if (isThereAnInstructorClash || isThereARoomClash)
                        {
                            return true; 
                        }
                    }
                }
            }
            return false;
        }

        public byte CalculateFitnessForADay(Day day)
        {
            byte numOfConflicts = 0;


            {
                // This checks if the schedule starts first thing in the morning
                if (!ScheduleHelper.DoesScheduleStartOnFirstPeriodInTheMorning(day))
                {
                    numOfConflicts++;
                }

                // This checks if the schedule starts first thing in the afternoon
                if (!ScheduleHelper.DoesScheduleStartOnFirstPeriodInTheAfternoon(day))
                {
                    numOfConflicts++;
                }

                // This checks if there is a free gap between periods in the morning
                if (ScheduleHelper.IsThereAGapBetweenEntriesInTheMorning(day))
                {
                    numOfConflicts++;
                }

                // This checks if there is a free gap between periods in the afternoon
                if (ScheduleHelper.IsThereAGapBetweenEntriesInTheAfternoon(day))
                {
                    numOfConflicts++;
                }

                // This checks if the lecture entries spans more than 2 periods
                if (ScheduleHelper.AreThereMoreThanTwoConsecutiveLectureEntriesInTheMorning(day))
                {
                    numOfConflicts++;
                }
                if (ScheduleHelper.AreThereMoreThanTwoConsecutiveLectureEntriesInTheAfternoon(day))
                {
                    numOfConflicts++;
                }


                // Does the instructor want this day
                //numOfConflicts += ScheduleHelper.CountInstructorPreferenceConflictsForThisDay(day);


                numOfConflicts += ScheduleHelper.HowManyTimesIsSchedulePerforatedInTheMorning(day);
                numOfConflicts += ScheduleHelper.HowManyTimesIsSchedulePerforatedInTheAfternoon(day);

                numOfConflicts += ScheduleHelper.CountConflictsBasedOnThreeConsecutiveLabEntriesInTheMorning(day);
                numOfConflicts += ScheduleHelper.CountConflictsBasedOnThreeConsecutiveLabEntriesInTheAfternoon(day);


            }

            numOfConflicts += CountConflictsBasedOnInstructorBeingFreeForThisDay(day);
            numOfConflicts += CountConflictsBasedOnRoomBeingFreeForThisDay(day);

            return numOfConflicts;
        }

        private byte CountConflictsBasedOnRoomBeingFreeForThisDay(Day day)
        {
            byte conflicts = 0;


            byte dayNumber = day.DayNumber;

            for (int j = 0; j < GlobalConfig.NUM_OF_PERIODS; j++)
            {
                byte period = day.Periods[j].Period;

                var currentEntry = day.Periods[j];

                if (currentEntry.Course != null)
                {
                    bool isThereAClash = scheduleEntries.Any(s => s.Day.DayNumber == dayNumber
                                                                && s.Period == period
                                                                && s.RoomId == currentEntry.Room.Id);
                    if (isThereAClash)
                    {
                        conflicts++;
                    }
                }
            }
            
            return conflicts;
        }

        private byte CountConflictsBasedOnInstructorBeingFreeForThisDay(Day day)
        {
            byte conflicts = 0;

            byte dayNumber = day.DayNumber;

            for (int j = 0; j < GlobalConfig.NUM_OF_PERIODS; j++)
            {
                byte period = day.Periods[j].Period;

                var currentEntry = day.Periods[j];

                if (currentEntry.Course != null)
                {
                    bool isThereAClash = scheduleEntries.Any(s => s.Day.DayNumber == dayNumber
                                                                && s.Period == period
                                                                && s.InstructorId == currentEntry.Instructor.Id);
                    if (isThereAClash)
                    {
                        conflicts++;
                    }
                }
            }
            
            return conflicts;
        }
    }
}
