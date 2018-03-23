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
        private List<Schedule> MatingPool = new List<Schedule>(); 

        public GeneticAlgorithm()
        {
            _context = new SchedulingContext();


            IntializePopulation();

            //int i = 0;
            //while (i < 10)
            //{
            //    var matingPool = NaturalSelection();
            //    CreateNextGeneration(matingPool);
            //    Console.WriteLine(i);
            //    i++; 
            //}

            foreach (var item in Population)
            {
                Console.WriteLine(item.Fitness);
            }
        }

        public void IntializePopulation()
        {
            var section = _context.Sections.SingleOrDefault(s => s.Id == 1);

            for (int i = 0; i < GlobalConfig.POPULATION_SIZE; i++)
            {
                var s = new Schedule(section, true);
                
                s.PrintSchedule(); 
                s.CalculateFitness();
                Console.WriteLine(s.Fitness);
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

                var child = parentA.Crossover(parentB);
                child.Mutate();
                child.CalculateFitness(); 
                Population[i] = child; 
            }
        }
    }
}
