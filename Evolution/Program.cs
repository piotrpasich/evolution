using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Evolution
{
    class Program
    {
// algorithm configuration
        public const int ProblemSize = 2;
        public const int MaxGens = 100;
        public const int PopSize = 30;
        public const int NumChildren = 20;
        public const int SearchSpaceMin = -5;
        public const int SearchSpaceMax = 5;
        static void Main(string[] args)
        {
            AlgMiLamda algMiLamda = new AlgMiLamda();
            algMiLamda.Search();
            Console.ReadKey();
        }
    }
}
