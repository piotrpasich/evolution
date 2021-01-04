using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Evolution
{
    class Chromosom 
    {
        private List<double> gens;
        private List<double> strategy;
        public double fitness;

        public Chromosom()
        {
            gens = new List<double>(Program.ProblemSize);
            strategy = new List<double>(Program.ProblemSize);
            fitness = 0;
        }

        public Chromosom(Chromosom item)
        {
            gens = new List<double>(Program.ProblemSize);
            strategy = new List<double>(Program.ProblemSize);
            this.fitness = item.fitness;
            gens.AddRange(item.gens);
            strategy.AddRange(item.strategy);
        }

        internal void InitRandom(Random random)
        {
            InitVector(gens, Program.SearchSpaceMin, Program.SearchSpaceMax, random);
            InitVector(strategy, 0.0, 0.5, random);
        }

        private void InitVector(List<double> vector, double v1, double v2, Random random)
        {
            for (int i = 0; i < Program.ProblemSize; i++)
            {
                vector.Add(RandomExtensions.NextDouble(random, v1, v2));
            }
        }

        internal void Mutate(Random random)
        {
            MutateProblem(random);
            MutateStrategy(random);
        }

        private void MutateProblem(Random random)
        {
            List<double> child = new List<double>(Program.ProblemSize);
            int i = 0;
            foreach (var item in gens)
            {
                double val = item + strategy[i++] * RandomExtensions.NextGaussian(random);
                val = val > Program.SearchSpaceMax ? Program.SearchSpaceMax : val;
                val = val < Program.SearchSpaceMin ? Program.SearchSpaceMin : val;
                child.Add(val);
            }
            gens = child;
        }

        private void MutateStrategy(Random rand)
        {
            double tau = -1.0 / Math.Sqrt(2.0 * strategy.Count);
            double tau_p = -1.0 / Math.Sqrt(2.0 * Math.Sqrt(strategy.Count));
            List<double> child = new List<double>(Program.ProblemSize);
            foreach (var item in strategy)
            {
                child.Add(item * Math.Exp(tau_p * RandomExtensions.NextGaussian(rand) + tau * RandomExtensions.NextGaussian(rand)));
            }
            strategy = child;
        }

        internal void CalculateFitness()
        {
            double y = gens[0] * gens[0] + gens[1] * gens[1];
            fitness = y;
        }

        public override string ToString()
        {
            return String.Format("x1={0:0.##}, x2={1:0.##}, Value {2:0.##}", gens[0], gens[1], fitness);
        }
    }
}
