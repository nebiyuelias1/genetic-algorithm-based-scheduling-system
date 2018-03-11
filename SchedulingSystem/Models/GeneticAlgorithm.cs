using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchedulingSystem.Models
{
    public class GeneticAlgorithm
    {
        SchedulingContext _context; 
        public GeneticAlgorithm()
        {
            _context = new SchedulingContext();
            var section = _context.Sections.SingleOrDefault(s => s.Id == 1); 

            Schedule schedule = new Schedule(section); 
        }

        
    }
}
