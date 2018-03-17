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
            var section = _context.Sections.SingleOrDefault(s => s.Id == 1);

            for (int i=0; i < GlobalConfig.POPULATION_SIZE; i++)
            {
                var s = new Schedule(section)
                {
                    
                };

                s.CalculateFitness();
                s.Print();
                Population.Add(s); 
            }
            
        }

        
    }
}
