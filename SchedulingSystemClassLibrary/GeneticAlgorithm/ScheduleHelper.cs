using SchedulingSystemClassLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchedulingSystemClassLibrary.GeneticAlgorithm
{
    public class ScheduleHelper
    {
        private const byte MORNING_START_INDEX = 0;
        private const byte AFTERNOON_START_INDEX = 4;

        public static bool DoesScheduleStartOnFirstPeriodInTheMorning(Day day)
        {
            if (day.Periods.GetRange(MORNING_START_INDEX, 4).Count > 0)
            {
                if (day.Periods[MORNING_START_INDEX].Course == null)
                {
                    return false;
                }
            }
            return true;
        }
        public static bool DoesScheduleStartOnFirstPeriodInTheAfternoon(Day day)
        {
            if (day.Periods.GetRange(AFTERNOON_START_INDEX, 4).Count > 0)
            {
                if (day.Periods[AFTERNOON_START_INDEX].Course == null)
                {
                    return false;
                }
            }
            return true;
        }

        public static bool IsThereAGapBetweenEntriesInTheMorning(Day day)
        {

            var morningPeriods = day.Periods.GetRange(MORNING_START_INDEX, 4).ToList();

            var morningPeriodsCount = morningPeriods.Where(s => s.Course != null).ToList().Count;

            if (morningPeriodsCount > 1)
            {
                var first = morningPeriods.First(x => x.Course != null);
                var last = morningPeriods.Last(x => x.Course != null);

                var firstIndex = morningPeriods.IndexOf(first);
                var lastIndex = morningPeriods.IndexOf(last);

                if (IsThereAGap(morningPeriods.GetRange(firstIndex, (lastIndex - firstIndex) + 1)))
                {
                    return true;
                }
            }
            else
            {
                return false;
            }

            return false;
        }

        public static bool IsThereAGapBetweenEntriesInTheAfternoon(Day day)
        {
            var afternoonPeriods = day.Periods.GetRange(AFTERNOON_START_INDEX, 4).ToList();

            var afternoonPeriodsCount = afternoonPeriods.Where(s => s.Course != null).ToList().Count;

            if (afternoonPeriodsCount > 1)
            {
                var first = afternoonPeriods.First(x => x.Course != null);
                var last = afternoonPeriods.Last(x => x.Course != null);

                var firstIndex = afternoonPeriods.IndexOf(first);
                var lastIndex = afternoonPeriods.IndexOf(last);

                if (IsThereAGap(afternoonPeriods.GetRange(firstIndex, (lastIndex - firstIndex) + 1)))
                {
                    return true; 
                }
            }
            else
            {
                return false; 
            }

            return false; 
        }

        private static bool IsThereAGap(List<ScheduleEntry> entries)
        {


            foreach (var entry in entries)
            {
                if (entry.Course == null)
                {
                    return true;
                }
            }

            return false;
        }

        public static byte HowManyTimesIsSchedulePerforatedInTheMorning(Day day)
        {
            byte perforationCount = 0;

            var morningPeriods = day.Periods.GetRange(MORNING_START_INDEX, 4).Where(s => s.Course != null).ToList();
            

            Dictionary<string, List<byte>> dictionary = CreatePeriodDictionary(morningPeriods);

            foreach (var courseTitle in dictionary.Keys)
            {
                var periods = dictionary[courseTitle];

                if (IsCoursePerforated(periods))
                {
                    perforationCount++;
                }
            }

            return perforationCount;
        }

        public static byte HowManyTimesIsSchedulePerforatedInTheAfternoon(Day day)
        {
            byte perforationCount = 0;

            var afternoonPeriods = day.Periods.GetRange(AFTERNOON_START_INDEX, 4).Where(s => s.Course != null).ToList();

            Dictionary<string, List<byte>> dictionary = CreatePeriodDictionary(afternoonPeriods);

            foreach (var courseTitle in dictionary.Keys)
            {
                var periods = dictionary[courseTitle];

                if (IsCoursePerforated(periods))
                {
                    perforationCount++;
                }
            }

            return perforationCount;
        }

        public static byte CountConflictsBasedOnThreeConsecutiveLabEntriesInTheMorning(Day day)
        {
            byte conflicts = 0;

            var labPeriods = day.Periods.GetRange(MORNING_START_INDEX, 4).Where(s => s.IsLab).ToList();

            if (labPeriods.Count > 0)
            {
                var dictionary = CreatePeriodDictionary(labPeriods);

                foreach (var courseTitle in dictionary.Keys)
                {

                    var labCourse = day.Periods.First(s => s.Course != null && s.Course.Title == courseTitle);

                    if (labCourse.Course.Laboratory >= 3)
                    {
                        int labPeriodsCount = dictionary[courseTitle].Count;

                        if (labPeriodsCount < 3)
                        {
                            conflicts++;
                        }

                    }
                }
            }
            
            return conflicts;
        }

        public static byte CountConflictsBasedOnThreeConsecutiveLabEntriesInTheAfternoon(Day day)
        {
            byte conflicts = 0;

            var labPeriods = day.Periods.GetRange(AFTERNOON_START_INDEX, 4).Where(s => s.IsLab).ToList();

            if (labPeriods.Count > 0)
            {
                var dictionary = CreatePeriodDictionary(labPeriods);

                foreach (var courseTitle in dictionary.Keys)
                {
                    var labCourse = day.Periods.First(s => s.Course != null && s.Course.Title == courseTitle);

                    if (labCourse.Course.Laboratory >= 3)
                    {
                        int labPeriodsCount = dictionary[courseTitle].Count;

                        if (labPeriodsCount < 3)
                        {
                            conflicts++;
                        }

                    }
                }
            }
            
            return conflicts;
        }

        public static bool AreThereMoreThanTwoConsecutiveLectureEntriesInTheMorning(Day day)
        {
           
            var lecturePeriods = day.Periods.GetRange(MORNING_START_INDEX, 4).Where(s => s.IsLecture).ToList();

            if (lecturePeriods.Count > 2)
            {
                var dictionary = CreatePeriodDictionary(lecturePeriods);

                foreach (var courseTitle in dictionary.Keys)
                {
                    var lectureCourse = day.Periods.First(s => s.Course != null && s.Course.Title == courseTitle);

                    if (lectureCourse.Course.Lecture > 2)
                    {
                        int lecturePeriodsCount = dictionary[courseTitle].Count;

                        if(lecturePeriodsCount > 2)
                        {
                            return true; 
                        }
                    }
                }
            }
            return false; 
        }

        public static bool AreThereMoreThanTwoConsecutiveLectureEntriesInTheAfternoon(Day day)
        {

            var lecturePeriods = day.Periods.GetRange(AFTERNOON_START_INDEX, 4).Where(s => s.IsLecture).ToList();

            if (lecturePeriods.Count > 2)
            {
                var dictionary = CreatePeriodDictionary(lecturePeriods);

                foreach (var courseTitle in dictionary.Keys)
                {
                    var lectureCourse = day.Periods.First(s => s.Course != null && s.Course.Title == courseTitle);

                    if (lectureCourse.Course.Lecture > 2)
                    {
                        int lecturePeriodsCount = dictionary[courseTitle].Count;

                        if (lecturePeriodsCount > 2)
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
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

        public static bool ShouldSectionBeSplitted(Section section)
        {
            if (section.StudentCount <= GlobalConfig.LAB_SPLIT_SIZE)
            {
                return false; 
            }
            else
            {
                var totalHours = 0;
                foreach (var courseOffering in section.CourseOfferings)
                {
                    totalHours += courseOffering.Course.Lecture;
                    totalHours += courseOffering.Course.Tutor;
                    totalHours += 2 * courseOffering.Course.Laboratory;
                }

                if (totalHours > GlobalConfig.MAX_SCHEDULE_ENTRIES)
                {
                    return false;
                }
                else
                {
                    return true; 
                }
            }
        }

        private static bool IsCoursePerforated(List<byte> periods)
        {
            bool isPerforated = true;

            int difference = periods[periods.Count - 1] - periods[0];

            if (difference < periods.Count)
            {
                isPerforated = false;
            }
            else
            {
                isPerforated = true;
            }
            
            return isPerforated;
        }

        public static byte CountInstructorPreferenceConflictsForThisDay(Day day)
        {
            byte conflicts = 0; 

            foreach (var period in day.Periods)
            {
                var instructor = period.Instructor;
                if (instructor != null)
                {
                    if (instructor.InstructorPreference != null && !instructor.InstructorPreference.Preference[day.DayNumber])
                    {
                        conflicts++;
                    }
                }
            }

            return conflicts; 
        }


        
    }
}
