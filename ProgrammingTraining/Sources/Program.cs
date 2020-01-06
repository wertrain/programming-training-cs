using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgrammingTraining
{
    class Program
    {
        static void Main(string[] args)
        {
            var tests = new List<IPracticeTest>();
            tests.Add(new SorobanTest());

            foreach (var test in tests)
            {
                Console.WriteLine("######## START - {0} ########", test.GetType().Name);
                test.Test();
                Console.WriteLine("########  END  - {0} ########", test.GetType().Name);
            }
        }
    }
}
