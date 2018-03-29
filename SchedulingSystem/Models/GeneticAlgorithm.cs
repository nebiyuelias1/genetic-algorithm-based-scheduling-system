using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchedulingSystem.Models
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

            for (int j = 0; j < 20; j++)
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

                var matingPool = NaturalSelection();
                CreateNextGeneration(matingPool);
            }
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
            var section = _context.Sections.SingleOrDefault(s => s.Id == 1);

            var mwfDays = new byte[] { 0, 2, 4 };
            var tthDays = new byte[] { 1, 3 };

            var dictionary = new Dictionary<string, byte[]>();

            var randomizedCourseOffering = section.CourseOfferings
                                .OrderByDescending(c => c.Course.Laboratory)
                                .ThenByDescending(c => c.Course.Lecture);

            byte counter = 0;
            foreach (var courseOffering in randomizedCourseOffering)
            {
                if (counter % 2 == 0)
                {
                    dictionary.Add(courseOffering.Course.Title, mwfDays);
                }
                else
                {
                    dictionary.Add(courseOffering.Course.Title, tthDays); 
                }

                counter++; 
            }

            for (int i = 0; i < GlobalConfig.POPULATION_SIZE; i++)
            {
                var s = new Schedule(section, dictionary);
                
                //s.PrintSchedule(); 
                s.CalculateFitness();
                //Console.WriteLine(s.Fitness);
                Population.Add(s);
            }
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

                Console.WriteLine("Parent A: {0}", parentA.Fitness);
                parentA.PrintSchedule();
                Console.WriteLine("Parent B: {0}", parentB.Fitness);
                parentB.PrintSchedule();

                var child = parentA;
                child.Mutate();
                child.CalculateFitness();
                Console.WriteLine("Child: {0}", child.Fitness);
                child.PrintSchedule();
                Population[i] = child; 
            }
        }
    }
}
