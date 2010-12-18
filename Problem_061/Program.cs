using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Problem_061
{
    class Program
    {
        static void Main(string[] args)
        {
            // Generates all triangle numbers.
            var numbersArray = new[]
                                   {
                                       GenerateNumbers(x => x*(x + 1)/2),
                                       GenerateNumbers(x => x*x),
                                       GenerateNumbers(x => x*(3*x - 1)/2),
                                       GenerateNumbers(x => x*(2*x - 1)),
                                       GenerateNumbers(x => x*(5*x - 3)/2),
                                       GenerateNumbers(x => x*(3*x - 2))
                                   };

            var result = TryToComposeLine(numbersArray, 0, new int[] {}, new int[] {}, -1);
            Console.WriteLine(result);
        }

        private static int TryToComposeLine(IEnumerable<int>[] numbersArray, int level,
                                            IEnumerable<int> restrictedArrays, IEnumerable<int> restrictedNumbers, int mustStartWith)
        {
            if (level == 0)
            {
                for (int i = 0; i < numbersArray.Length; ++i )
                {
                    foreach (var number in numbersArray[i])
                    {
                        var result = TryToComposeLine(numbersArray, 1, new[] { i }, new[] { number }, number % 100);
                        if (result > 0)
                            return result;
                    }
                }
            }

            for (int i = 0; i < numbersArray.Length; ++i)
            {
                if (restrictedArrays.Contains(i))
                    continue;

                foreach (var number in numbersArray[i])
                {
                    if (restrictedNumbers.Contains(number) || number / 100 != mustStartWith)
                        continue;

                    var result = TryToComposeLine(numbersArray, level + 1, restrictedArrays.Union(new[] {i}),
                                                  restrictedNumbers.Union(new[] {number}),
                                                  number % 100);
                    if (result > 0)
                        return result;
                }
            }

            int count = 0;
            int[] ar = restrictedArrays.ToArray();
            foreach (var restrictedNumber in restrictedNumbers)
            {
                Console.Write(ar[count] + ":" + restrictedNumber + "=>");
                ++count;
            }

            if (level > 5)
            {
                // Check.
                var a = restrictedNumbers.ToArray();
                if (a[0] / 100 != a[5] % 100)
                {
                    Console.WriteLine("EPIC FAIL");
                    return -1;
                }

                Console.WriteLine();
                return restrictedNumbers.Sum();
            }

            Console.WriteLine("FAIL");

            return -1;
        }

        private static IEnumerable<int> GenerateNumbers(Func<int, int> func)
        {
            int currentIndex = 1;

            // Search first number.
            while (func(++currentIndex) < 1000)
            {
            }

            currentIndex--;
            int currentNumber = func(currentIndex);
            while (currentNumber < 10000)
            {
                if (currentNumber >= 1000 && currentNumber < 10000)
                    yield return currentNumber;
                currentNumber = func(currentIndex++);
            }
        }
    }
}
