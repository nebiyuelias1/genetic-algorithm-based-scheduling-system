using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchedulingSystemClassLibrary
{
    public static class GlobalConfig
    {
        public const byte NUM_OF_DAYS = 5;
        public const byte NUM_OF_PERIODS = 8;
        public const int POPULATION_SIZE = 5000;
        public const float MUTATION_RATE = 0.4f;
        public const float CROSSOVER_RATE = 0.8f; 
        public const byte TOURNAMENT_SIZE = 2;
        public const byte LAB_SPLIT_SIZE = 40; 
    }
}
