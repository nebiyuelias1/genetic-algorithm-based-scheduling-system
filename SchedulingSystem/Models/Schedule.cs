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
        public double MaximumScore { get; set; }
        public virtual Section Section { get; set; }
        public int SectionId { get; set; }
        public virtual List<Day> Days { get; set; }

        public Schedule()
        {

            if (Days == null)
            {
                Days = new List<Day>(GlobalConfig.NUM_OF_DAYS);
            }

            InitializeSchedule();
        }
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
            }
        }

        public Schedule(Section section, bool anotherDataStructure)
        {
            Section = section;

            if (Days == null)
            {
                Days = new List<Day>(GlobalConfig.NUM_OF_DAYS); 
            }

            InitializeSchedule();

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

                    // pick a random slot to index into the Day list
                    byte randDay = (byte)rand.Next(0, GlobalConfig.NUM_OF_DAYS);
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
                    else
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

                while (lab > 0)
                {
                    var room = Section.AssignedRooms.SingleOrDefault(r => r.IsLabRoom == true);

                    // pick a random slot to index into the ScheduleDNA list 
                    byte randDay = (byte)rand.Next(0, GlobalConfig.NUM_OF_DAYS);
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
                    else if (lab > 1)
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
                    else
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

                while (tutor > 0)
                {
                    var room = Section.AssignedRooms.SingleOrDefault(r => r.IsLectureRoom == true);

                    // pick a random slot to index into the ScheduleDNA list 
                    byte randDay = (byte)rand.Next(0, GlobalConfig.NUM_OF_DAYS);
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
                    else
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
            byte minMorning = 0;
            byte minAfternoon = 4;
            foreach (var day in Days)
            {
                var morningPeriods = day.Periods.GetRange(minMorning, 4);
                var moringPeriodsCount = morningPeriods.Where(s => s.Course != null).Count();

                if (moringPeriodsCount > 0 && day.Periods[minMorning].Course == null)
                {
                    var period = morningPeriods.FirstOrDefault(s => s.Course != null);
                    var index = day.Periods.IndexOf(period);

                    for (int i = index; i <= moringPeriodsCount; i++)
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
                    } 
                }
            }
        }

        public Schedule Crossover(Schedule parentB)
        {
            var lectureDictionary = new Dictionary<string, byte>();
            var labDictionary = new Dictionary<string, byte>();
            var tutorDictionary = new Dictionary<string, byte>();

            List<CourseOffering> courseOfferings = _context.CourseOfferings.Where(c => c.SectionId == Section.Id).ToList();

            foreach (var courseOffering in courseOfferings)
            {
                lectureDictionary.Add(courseOffering.Course.Title, courseOffering.Course.Lecture);
                labDictionary.Add(courseOffering.Course.Title, courseOffering.Course.Laboratory);
                tutorDictionary.Add(courseOffering.Course.Title, courseOffering.Course.Tutor); 
            }
            var child = new Schedule();

            Random rand = new Random();

            for (int i = 0; i < GlobalConfig.NUM_OF_DAYS; i++)
            {
                for (int j = 0; j < GlobalConfig.NUM_OF_PERIODS; j++)
                {
                    if (this.Days[i].Periods[j].Course != null)
                    {
                        if (this.Days[i].Periods[j].IsLecture && lectureDictionary[this.Days[i].Periods[j].Course.Title] > 0)
                        {
                            child.Days[i].Periods[j] = this.Days[i].Periods[j];
                            lectureDictionary[this.Days[i].Periods[j].Course.Title]--; 
                        }
                        else if (this.Days[i].Periods[j].IsLab && labDictionary[this.Days[i].Periods[j].Course.Title] > 0)
                        {
                            child.Days[i].Periods[j] = this.Days[i].Periods[j];
                            labDictionary[this.Days[i].Periods[j].Course.Title]--;
                        }
                        else if (this.Days[i].Periods[j].IsTutor && tutorDictionary[this.Days[i].Periods[j].Course.Title] > 0)
                        {
                            child.Days[i].Periods[j] = this.Days[i].Periods[j];
                            tutorDictionary[this.Days[i].Periods[j].Course.Title]--;
                        }
                        
                    }
                    else if (parentB.Days[i].Periods[j].Course != null)
                    {
                        if (parentB.Days[i].Periods[j].IsLecture && lectureDictionary[parentB.Days[i].Periods[j].Course.Title] > 0)
                        {
                            child.Days[i].Periods[j] = parentB.Days[i].Periods[j];
                            lectureDictionary[parentB.Days[i].Periods[j].Course.Title]--;
                        }
                        else if (parentB.Days[i].Periods[j].IsLab && labDictionary[parentB.Days[i].Periods[j].Course.Title] > 0)
                        {
                            child.Days[i].Periods[j] = parentB.Days[i].Periods[j];
                            labDictionary[parentB.Days[i].Periods[j].Course.Title]--;
                        }
                        else if (parentB.Days[i].Periods[j].IsTutor && tutorDictionary[parentB.Days[i].Periods[j].Course.Title] > 0)
                        {
                            child.Days[i].Periods[j] = parentB.Days[i].Periods[j];
                            tutorDictionary[parentB.Days[i].Periods[j].Course.Title]--;
                        }
                    }
                        
                }
            }

            return child; 
        }

        public void CalculateFitness()
        {
            int score = 0;
            int maximumScore = 0; 

            byte totalLectureHours = 0;
            byte totalHours = 0; 

            foreach (var courseOffering in Section.CourseOfferings)
            {
                totalLectureHours += courseOffering.Course.Lecture;
                totalHours += courseOffering.Course.Lecture;
                totalHours += courseOffering.Course.Laboratory;
                totalHours += courseOffering.Course.Tutor;
            }

            score += AssignScoreBasedOnScheduleStartingOnFirstPeriod();
            maximumScore += 10; 
            score += AssignScoreBasedOnLunchBreakGaps();
            maximumScore += 5;
            score += AssignScoreBasedOnPerforation();
            maximumScore += 10;
            if (totalLectureHours < 20)
            {
                score += AssignScoreBasedOnLectureBeingInTheMorning();
                maximumScore += totalLectureHours; 
            }

            if (totalLectureHours < 30)
            {
                score += AssignScoreBasedOnFreeEntryInTheLastPeriod();
                maximumScore += 10; 
            }

            Fitness = Math.Pow(score, 4);
            MaximumScore = Math.Pow(maximumScore, 4); 
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
        private byte AssignScoreBasedOnPerforation()
        {
            byte score = 0;

            byte morningStartIndex = 0;
            byte afternoonStartIndex = 4;

            foreach (var day in Days)
            {
                var morningPeriods = day.Periods.GetRange(morningStartIndex, 4).Where(s => s.Course != null).ToList();
                var afternoonPeriods = day.Periods.GetRange(afternoonStartIndex, 4).Where(s => s.Course != null).ToList();

                int morningPeriodsCount = morningPeriods.Count(s => s.Course != null);
                int afternoonPeriodsCount = afternoonPeriods.Count(s => s.Course != null);

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
            }

            return score; 
        }
        private byte AssignScoreBasedOnLunchBreakGaps()
        {
            byte score = 0;

            foreach (var day in Days)
            {
                if (day.Periods[3].Course != null && day.Periods[4].Course != null && day.Periods[3].Course.Title != day.Periods[4].Course.Title)
                {
                    score++; 
                }
                else if (day.Periods[3].Course == null || day.Periods[4].Course == null)
                {
                    score++; 
                }
            }
            return score; 
        }
        private byte AssignScoreBasedOnScheduleStartingOnFirstPeriod()
        {
            byte morningStartIndex = 0;
            byte afternoonStartIndex = 4;

            byte score = 0;

            foreach (var day in Days)
            {
                if (day.Periods[morningStartIndex].Course != null)
                {
                    score++;  
                }
                if (day.Periods[afternoonStartIndex].Course != null)
                {
                    score++; 
                }
            }

            return score; 
        }
        private static Dictionary<string, List<byte>> CreatePeriodDictionary(List<ScheduleEntry> periods)
        {
            Dictionary<string, List<byte>> dictionary = new Dictionary<string, List<byte>>();

            foreach (var period in periods)
            {
                if (dictionary[period.Course.Title] == null)
                {
                    var list = new List<byte>();
                    list.Add((byte)period.Period);
                    dictionary.Add(period.Course.Title, list);
                }
                else
                {
                    dictionary[period.Course.Title].Add((byte)period.Period);
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

        private void InitializeScheduleDna()
        {
            //for (byte i = 0; i < GlobalConfig.NUM_OF_DAYS; i++)
            //{
            //    for (byte j = 0; j < GlobalConfig.NUM_OF_PERIODS; j++)
            //    {
            //        ScheduleDNA.Add(new ScheduleEntry
            //        {
            //            Day = i,
            //            Period = j
            //        });
            //    }
            //}
        }

        private void InitializeSchedule()
        {
            for (byte i = 0; i < GlobalConfig.NUM_OF_DAYS; i++)
            {
                var day = new Day
                {
                    DayNumber = i,
                    DayName = GetDayName(i),
                    Schedule = this
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

        private byte[] FindConsecutiveSlots(byte randSlot, byte size)
        {
            byte[] slots = new byte[size];

            byte min = 0;
            byte max = GlobalConfig.NUM_OF_DAYS * GlobalConfig.NUM_OF_PERIODS - 1;

            if (randSlot == min)
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
        public void PrintSchedule()
        {
            foreach (var day in Days)
            {
                foreach (var period in day.Periods)
                {
                    if (period.Course != null)
                        Console.Write(period.Course.Title + "(" + period.Period + ")");
                    if (period.IsLab)
                        Console.Write("(Lab)");
                    if (period.IsLecture)
                        Console.Write("(Lecture)");
                    if (period.IsTutor)
                        Console.Write("(Tutor)");

                    Console.Write("|");

                }
                Console.WriteLine();
            }
            Console.WriteLine(); 
        }
    }
}
