using SchedulingSystemClassLibrary.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchedulingSystemClassLibrary.GeneticAlgorithm
{
    public class GeneticAlgorithm
    {
        private SchedulingContext _context;

        private List<Schedule> Population = new List<Schedule>(GlobalConfig.POPULATION_SIZE);

        private int SectionId; 
        public GeneticAlgorithm(int id)
        {
            _context = new SchedulingContext();
            this.SectionId = id;
            IntializePopulation(SectionId);
        }
        public Schedule FindBestSchedule()
        {

            var generation = 0;
            while (true)
            {
                if (generation >= 50)
                {
                    break;
                }
                //CreateNextGenerationUsingTournamentSelection();
                var matingPool = NaturalSelection();
                CreateNextGeneration(matingPool);

                Population.ForEach(s => s.CalculateFitness());

                Debug.WriteLine("----------------------");
                Debug.WriteLine($"Generation - {generation + 1}");


                int distinctElementsCount = Population.GroupBy(s => s.Fitness).Count();
                Debug.WriteLine($"from {distinctElementsCount} distinct elements");

                Debug.WriteLine($"Max Fitness - {Population.Max(s => s.Fitness)} Min Fitness - {Population.Min(s => s.Fitness)}");

                var topSchedules = Population.Where(p => p.Fitness == 1d);


                var noClashCount = 0;
                foreach (var population in Population)
                {
                    if (!population.IsThereAnyClash())
                    {
                        noClashCount++; 
                    }
                }
                Debug.WriteLine($"No Clash count - {noClashCount}");




                if ((Population.Any(s => s.Fitness >= 1.0)) && (Population.Any(s => !s.IsThereAnyClash())))
                {
                    return Population.Where(s => s.Fitness >= 1.0).First();
                }

                generation++; 
            }



            return null;
        }

        private void CreateNextGenerationUsingTournamentSelection()
        {
            var population = new List<Schedule>(GlobalConfig.POPULATION_SIZE);

            Random rand = new Random();

            for (int i = 0; i < GlobalConfig.POPULATION_SIZE; i++)
            {
                var parentA = TournamentSelection();

                var parentB = TournamentSelection();

                Schedule child = null; 

                if (rand.NextDouble() <= GlobalConfig.CROSSOVER_RATE)
                {
                    child = parentA.Crossover(parentB);
                }
                else if (parentA.Fitness > parentB.Fitness)
                {
                    child = parentA;
                }
                else if (parentA.Fitness < parentB.Fitness)
                {
                    child = parentB;
                }
                else
                {
                    var parentAOrB = rand.Next(2);
                    switch (parentAOrB)
                    {
                        case 0:
                            child = parentA;
                            break;
                        case 1:
                            child = parentB;
                            break;
                    }
                }
                
                if (rand.NextDouble() <= GlobalConfig.MUTATION_RATE)
                {
                    child.Mutate();
                }

                child.CalculateFitness();
                population.Add(child); 
            }

            Population = population;
        }

        private void CreateNextGenerationUsingRouletteWheelSelection()
        {
            var population = new List<Schedule>(GlobalConfig.POPULATION_SIZE);
            var fitnessSum = Population.Sum(s => s.Fitness);
            Random rand = new Random();

            Population = Population.OrderByDescending(s => s.Fitness).ToList();

            for (int i = 0; i < GlobalConfig.POPULATION_SIZE; i++)
            {
                var parentA = RouletteWheelSelection(fitnessSum);

                while (parentA == null)
                {
                    parentA = RouletteWheelSelection(fitnessSum);
                }

                var parentB = RouletteWheelSelection(fitnessSum);

                while (parentB == null)
                {
                    parentB = RouletteWheelSelection(fitnessSum);
                }

                Schedule child = null;

                if (rand.NextDouble() <= GlobalConfig.CROSSOVER_RATE)
                {
                    child = parentA.Crossover(parentB);
                }
                else if (parentA.Fitness > parentB.Fitness)
                {
                    child = parentA;
                }
                else if (parentA.Fitness < parentB.Fitness)
                {
                    child = parentB;
                }
                else
                {
                    var parentAOrB = rand.Next(2);
                    switch (parentAOrB)
                    {
                        case 0:
                            child = parentA;
                            break;
                        case 1:
                            child = parentB;
                            break;
                    }
                }

                if (rand.NextDouble() <= GlobalConfig.MUTATION_RATE)
                {
                    child.Mutate();
                }

                child.CalculateFitness();
                population.Add(child);
            }

            Population = population;
        }
        private void IntializePopulation(int id)
        {
            var section = _context.Sections.Include(s => s.CourseOfferings.Select(c => c.Course))
                                            .Include(s => s.CourseOfferings.Select(c => c.Instructor).Select(c => c.InstructorPreference))
                                            .Include(s => s.AssignedLectureRoom.Building)
                                            .Include(s => s.LabGroups
                                            .Select(x => x.Room.Building))
                                            .SingleOrDefault(s => s.Id == id);

            var currentSemester = _context
                                    .AcademicSemesters
                                    .SingleOrDefault(s => s.CurrentSemester);

            var scheduleEntries = _context.ScheduleEntries
                                    .Include(s => s.Instructor)
                                    .Include(s => s.Room)
                                    .Include(s => s.Day)
                                    .Include(s => s.Day.Schedule)
                                    .Where(s => s.Day.Schedule.AcademicSemesterId == currentSemester.Id)
                                    .ToList();

            

            var mwfDays = new byte[] { 0, 2, 4 };
            var tthDays = new byte[] { 1, 3 };

            var dictionary = new Dictionary<string, byte[]>();

            var randomizedCourseOffering = section.CourseOfferings
                                .OrderByDescending(c => c.Course.Laboratory);

            int mwfPeriodsCount = 0;
            int tthPeriodsCount = 0;

            var shouldSectionBeSplitted = ScheduleHelper.ShouldSectionBeSplitted(section);
            byte counter = 0;
            foreach (var courseOffering in randomizedCourseOffering)
            {
                int sum = shouldSectionBeSplitted  
                            ? courseOffering.Course.Lecture + 2*courseOffering.Course.Laboratory + courseOffering.Course.Tutor
                            : courseOffering.Course.Lecture + courseOffering.Course.Laboratory + courseOffering.Course.Tutor;

                

                if (counter  % 2 == 0)
                {
                    if ((mwfPeriodsCount + sum) <= 24)
                    {
                        dictionary.Add(courseOffering.Course.Title, mwfDays);
                        mwfPeriodsCount += sum; 
                    }
                    else if((tthPeriodsCount + sum)<= 16)
                    {
                        dictionary.Add(courseOffering.Course.Title, tthDays);
                        tthPeriodsCount += sum;
                    }

                }
                else
                {
                    if ((tthPeriodsCount + sum) <= 16)
                    {
                        dictionary.Add(courseOffering.Course.Title, tthDays);
                        tthPeriodsCount += sum;
                    }
                    else if (mwfPeriodsCount + sum <= 24)
                    {
                        dictionary.Add(courseOffering.Course.Title, mwfDays);
                        mwfPeriodsCount += sum;
                    }
                }

                counter++; 
            }

            

            for (int i = 0; i < GlobalConfig.POPULATION_SIZE; i++)
            {
                var s = new Schedule(section, dictionary, scheduleEntries, shouldSectionBeSplitted);

                s.CalculateFitness();

                //Console.WriteLine("Fitness: {0}", s.Fitness);
                //s.PrintSchedule();

                Population.Add(s);
            }
        }
        private Schedule RouletteWheelSelection(double fitnessSum)
        {
            Random rand = new Random(); 

            var r = rand.NextDouble() * fitnessSum;

            double partialSum = 0.0; 
            for (int j = 0; j < GlobalConfig.POPULATION_SIZE; j++)
            {
                partialSum += Population[j].Fitness; 

                if (partialSum >= r)
                {
                    return Population[j];
                }
            }

            return null; 
        }

        private Schedule TournamentSelection()
        {
            Random rand = new Random();

            var schedules = new List<Schedule>(GlobalConfig.TOURNAMENT_SIZE); 

            for (int i = 0; i < GlobalConfig.TOURNAMENT_SIZE; i++)
            {
                int randIndex = rand.Next(GlobalConfig.POPULATION_SIZE);

                schedules.Add(Population[randIndex]);
            }

            return schedules.OrderByDescending(s => s.Fitness).First(); 
        }
        private List<Schedule> NaturalSelection()
        {
            List<Schedule> matingPool = new List<Schedule>();
            var fitnessSum = Population.Sum(p => p.Fitness);  

            foreach (var item in Population)
            {
                //int normalizedFitness = (int)Math.Floor(item.Fitness * 100);

                // Temp code 
                var normalizedFitness = (item.Fitness*100 / fitnessSum) * GlobalConfig.POPULATION_SIZE;

                for (int i = 0; i < normalizedFitness; i++)
                {
                    matingPool.Add(item); 
                }
            }
            return matingPool; 
        }

        private void CreateNextGeneration(List<Schedule> matingPool)
        {
            Random rand = new Random();
            var population = new List<Schedule>(GlobalConfig.POPULATION_SIZE); 

            for (int i = 0; i < GlobalConfig.POPULATION_SIZE; i++)
            {
                int a = rand.Next(matingPool.Count);
                int b = rand.Next(matingPool.Count);

                var parentA = matingPool[a];
                var parentB = matingPool[b];

                //Console.WriteLine("Parent A: {0}", parentA.Fitness);
                //parentA.PrintSchedule();
                //Console.WriteLine("Parent B: {0}", parentB.Fitness);
                //parentB.PrintSchedule();

                Schedule child = null;

                if (rand.NextDouble() <= GlobalConfig.CROSSOVER_RATE)
                {
                    child = parentA.Crossover(parentB);
                }
                else if (parentA.Fitness > parentB.Fitness)
                {
                    child = parentA; 
                }
                else if ( parentA.Fitness < parentB.Fitness)
                {
                    child = parentB;
                }
                else
                {
                    var parentAOrB = rand.Next(2);
                    switch (parentAOrB)
                    {
                        case 0:
                            child = parentA;
                            break;
                        case 1:
                            child = parentB;
                            break; 
                    }
                }

                if (rand.NextDouble() <= GlobalConfig.MUTATION_RATE)
                {
                    child.Mutate();
                }
                child.CalculateFitness();

                //Console.WriteLine("Child: {0}", child.Fitness);
                //child.PrintSchedule();
                population.Add(child);
            }
            Population = population; 
        }
    }
}
