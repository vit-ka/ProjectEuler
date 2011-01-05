using System;
using System.Collections.Generic;
using System.Linq;

namespace Problem_076
{
    class Program
    {
        static void Main(string[] args)
        {
            const int startNumber = 100;
            long waysCount = EvaluateWaysCount(startNumber, 0).Count;

            Console.WriteLine(waysCount);
        }

        private static IList<IList<int>> EvaluateWaysCount(int startNumber, int level)
        {
            if (startNumber == 1)
                return new List<IList<int>> {new List<int> {1}};

            var result = new List<IList<int>>();

            for (int i = 1; i < startNumber; ++i)
            {
                var first = EvaluateWaysCount(i, level + 1);
                var second = EvaluateWaysCount(startNumber - i, level + 1);

                var firstAndSecond = first.Concat(second);

                result = result.Concat(firstAndSecond).ToList();
            }

            Console.WriteLine(level + " : " + startNumber + " = " + result.Count);

            return result;
        }
    }
}
