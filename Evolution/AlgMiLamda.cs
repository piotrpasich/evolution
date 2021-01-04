using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Evolution
{
    class AlgMiLamda
    {
        private List<Chromosom> _population;
        private Random _random;
        public AlgMiLamda()
        {
            _population = new List<Chromosom>(Program.PopSize);
            _random = new Random((int)DateTime.Now.Ticks);
        }

        internal void Search()
        {
            InitPopulation();
            PrintObjectiveValues(_population);
            for (int i = 0; i < 10; i++) // just repeat some more times
            {
                double fitValue = Double.MinValue;
                while (_population[0].fitness > fitValue)
                {
                    var bestPop = CreateChildPopulation(_population);
                    fitValue = bestPop[0].fitness;
                    PrintObjectiveValues(bestPop);
                    Mutate(bestPop);
                    EvaluateObjective(bestPop);
                    _population = CreatePopulation(_population, bestPop);
                    Console.WriteLine("Pop {0}, FitVal {1}", _population[0].fitness, fitValue);
                }
            }
        }

        private List<Chromosom> CreatePopulation(List<Chromosom> population, List<Chromosom> bestPop)
        {
            List<Chromosom> allPopulation = new List<Chromosom>(Program.PopSize + Program.NumChildren);
            allPopulation.AddRange(population);
            allPopulation.AddRange(bestPop);
            return allPopulation.OrderBy(x => x.fitness).Take(Program.PopSize).ToList();
        }

        private List<Chromosom> CreateChildPopulation(List<Chromosom> population)
        {
            List<Chromosom> bestPopChild = new List<Chromosom>(Program.NumChildren); // minimalization
            var bestPop = population.OrderBy(x => x.fitness).Take(Program.NumChildren).ToList<Chromosom>();
            foreach (var item in bestPop)
            {
                bestPopChild.Add(new Chromosom(item));
            }
            return bestPopChild;
        }

        private void Mutate(List<Chromosom> bestPop)
        {
            foreach (var item in bestPop)
            {
                item.Mutate(_random);
            }
        }

        private void InitPopulation()
        {
            for (int i = 0; i < Program.PopSize; i++)
            {
                Chromosom cr = new Chromosom();
                cr.InitRandom(_random);
                _population.Add(cr);
            }
            EvaluateObjective(_population);
        }

        /// <summary>
        /// Fitness function
        /// </summary>
        /// <param name="population"></param>
        private void EvaluateObjective(List<Chromosom> population)
        {
            foreach (var item in population)
            {
                item.CalculateFitness();
            }
        }

        private void PrintObjectiveValues(List<Chromosom> population)
        {
            foreach (var item in population)
            {
                Console.WriteLine(item.ToString());
            }
            Console.WriteLine();
        }
    }
}
