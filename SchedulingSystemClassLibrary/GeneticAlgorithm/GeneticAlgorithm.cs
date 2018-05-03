using SchedulingSystemClassLibrary.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity; 
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchedulingSystemClassLibrary.GeneticAlgorithm
{
    public class GeneticAlgorithm
    {
        private SchedulingContext _context;

        private List<Schedule> Population = new List<Schedule>(GlobalConfig.POPULATION_SIZE);

        public GeneticAlgorithm()
        {
            _context = new SchedulingContext();

            
            Random rand = new Random(); 

            IntializePopulation();

            for (int j = 0; j < 100; j++)
            {
                //for (int i = 0; i < Population.Count; i++)
                //{
                //    var parentA = PickRandomParent();
                //    var parentB = PickRandomParent();

                //    parentA.PrintSchedule();
                //    parentB.PrintSchedule(); 
                //    var child = parentA.Crossover(parentB);


                //    if (rand.NextDouble() < GlobalConfig.MUTATION_RATE)
                //    {
                //        //child.Mutate();
                //    }
                //    child.PrintSchedule();
                //    child.CalculateFitness();
                //    Population[i] = child;
                //}
                Console.WriteLine($"Generation - {j+1}");
                var noClashCount = 0;
                Population.ForEach(s => {

                    if (!s.IsThereAnyClash())
                    {
                        noClashCount++; 
                    }
                });
                Console.WriteLine($"No Clash count - {noClashCount}");

                int distinctElementsCount = Population.GroupBy(s => s.Fitness).Count();
                Console.WriteLine($"from {distinctElementsCount} distinct elements");

                Console.WriteLine($"Max Fitness - {Population.Max(s => s.Fitness)} Min Fitness - {Population.Min(s => s.Fitness)}");
                //CreateNextGeneration();
                var matingPool = NaturalSelection();
                CreateNextGeneration(matingPool);


            }


            var best = Population.OrderByDescending(p => p.Fitness).First();
            
            best.PrintSchedule();
            best.CalculateFitness();
            var isThereClash = best.IsThereAnyClash();

            
            _context.Schedules.Add(best);
            _context.SaveChanges();

        }

        private void CreateNextGeneration()
        {
            var population = new List<Schedule>(GlobalConfig.POPULATION_SIZE);
            var fitnessSum = Population.Sum(s => s.Fitness);

            Random rand = new Random();

            Population = Population.OrderByDescending(s => s.Fitness).ToList();

            for (int i = 0; i < GlobalConfig.POPULATION_SIZE; i++)
            {
                //var parentA = RouletteWheelSelection(fitnessSum);
                var parentA = TournamentSelection();

                //while (parentA == null)
                //{
                //    parentA = RouletteWheelSelection(fitnessSum);
                //}

                //var parentB = RouletteWheelSelection(fitnessSum);
                var parentB = TournamentSelection();

                //while (parentB == null)
                //{
                //    parentB = RouletteWheelSelection(fitnessSum);
                //}

                var child = parentA.Crossover(parentB);
                if (rand.NextDouble() <= GlobalConfig.MUTATION_RATE)
                {
                    child.Mutate();
                }

                child.CalculateFitness();
                population.Add(child); 
            }

            Population = population;
        }

        public Schedule PickRandomParent()
        {
            Random rand = new Random();
            while (true)
            {
                int randSlot = rand.Next(Population.Count);

                if(rand.NextDouble() < Population[randSlot].Fitness)
                {
                    return Population[randSlot];  
                }
            }
            
        }
        public void IntializePopulation()
        {
            var section = _context.Sections.Include(s => s.CourseOfferings.Select(c => c.Course))
                                            .Include(s => s.CourseOfferings.Select(c => c.Instructor).Select(c => c.InstructorPreference))
                                            .Include(s => s.AssignedLectureRoom)
                                            .Include(s => s.AssignedLabRoom)
                                            .SingleOrDefault(s => s.Id == 4);

            var scheduleEntries = _context.ScheduleEntries
                                    .Include(s => s.Instructor)
                                    .Include(s => s.Room)
                                    .Include(s => s.Day)
                                    .ToList();

            var mwfDays = new byte[] { 0, 2, 4 };
            var tthDays = new byte[] { 1, 3 };

            var dictionary = new Dictionary<string, byte[]>();

            var randomizedCourseOffering = section.CourseOfferings
                                .OrderByDescending(c => c.Course.Laboratory);

            int mwfPeriodsCount = 0;
            int tthPeriodsCount = 0; 

            byte counter = 0;
            foreach (var courseOffering in randomizedCourseOffering)
            {
                int sum = courseOffering.Course.Lecture + courseOffering.Course.Laboratory + courseOffering.Course.Tutor; 
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
                var s = new Schedule(section, dictionary, scheduleEntries);

                s.CalculateFitness();

                //Console.WriteLine("Fitness: {0}", s.Fitness);
                //s.PrintSchedule();

                Population.Add(s);
            }
        }
        public Schedule RouletteWheelSelection(double fitnessSum)
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

        public Schedule TournamentSelection()
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
        public List<Schedule> NaturalSelection()
        {
            List<Schedule> matingPool = new List<Schedule>();

            foreach (var item in Population)
            {
                int normalizedFitness = (int)Math.Floor(item.Fitness * 100);

                for (int i = 0; i < normalizedFitness; i++)
                {
                    matingPool.Add(item); 
                }
            }
            return matingPool; 
        }

        public void CreateNextGeneration(List<Schedule> matingPool)
        {
            Random rand = new Random();

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

                var child = parentA.Crossover(parentB);
                if (rand.NextDouble() <= GlobalConfig.MUTATION_RATE)
                {
                    child.Mutate();
                }
                child.CalculateFitness();
                //Console.WriteLine("Child: {0}", child.Fitness);
                //child.PrintSchedule();
                Population[i] = child; 
            }
        }
    }
}
