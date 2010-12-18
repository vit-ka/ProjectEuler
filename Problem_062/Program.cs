using System;
using System.Collections.Generic;
using System.Linq;

namespace Problem_062
{
    class Program
    {
        static void Main()
        {
            DateTime d = DateTime.Now;
            IEnumerable<long> cubes = GenerateCubes();

            IEnumerable<IList<byte>> splittedCubes = SplitCubes(cubes);

            var groupedCubes =
                splittedCubes.GroupBy(digits => digits, new AComparer()).Where(
                    groupedDigits => groupedDigits.Count() == 5).OrderBy(groupedDigits => groupedDigits.Count()).Select(
                        groupedDigits => groupedDigits).First();

            var en = groupedCubes.GetEnumerator();
            en.MoveNext();
            var seq = en.Current;

            var comp = new AComparer();
            var index = splittedCubes.TakeWhile(a => !comp.Equals(a, seq)).Count();

            Console.WriteLine(cubes.ToArray()[index]);
            Console.WriteLine("Time: " + (DateTime.Now - d).TotalMilliseconds);
        }

        private static IEnumerable<IList<byte>> SplitCubes(IEnumerable<long> cubes)
        {
            return cubes.Select(cube => SplitCube(cube));
        }

        private static IList<byte> SplitCube(long cube)
        {
            var result = new List<byte>();

            while (cube > 0)
            {
                result.Add((byte) (cube % 10));
                cube = cube/10;
            }

            result.Sort();
            return result;
        }

        private static IEnumerable<long> GenerateCubes()
        {
            long index = 1;

            while (index * index * index < 1000000000000L)
            {
                yield return index*index*index;
                index++;
            }
        }
    }
}
