using System;
using System.Collections.Generic;

namespace Problem_060
{
    internal class Program
    {
        private static int[] _primes;

        private static void Main(string[] args)
        {
            Console.WriteLine("Generating primes...");
            _primes = GeneratePrimes(100000000);
            Console.WriteLine("Generating primes finished");

            const int familySize = 6;
            const int maxNumber = 10000;

            int lowestSum = int.MaxValue;

            IList<int[]> oldIndexes = new List<int[]>(maxNumber);

            for (int i = 0; i < maxNumber; ++i)
            {
                oldIndexes.Add(
                    new[]
                        {
                            i
                        });
            }

            for (int count = 2; count < familySize; ++count)
            {
                Console.WriteLine("Generating pairs with familySize: {0}...", count);

                IList<int[]> newIndexes = new List<int[]>();

                foreach (var oldIndex in oldIndexes)
                {
                    var newIndex = new int[oldIndex.Length + 1];

                    for (int i = 0; i < newIndex.Length - 1; ++i)
                        newIndex[i] = oldIndex[i];

                    newIndex[newIndex.Length - 1] = newIndex[newIndex.Length - 2] + 1;

                    while (newIndex[newIndex.Length - 1] < maxNumber)
                    {
                        if (CheckIndexes(newIndex))
                        {
                            int[] newIndexForList = new int[newIndex.Length];
                            Array.Copy(newIndex, newIndexForList, newIndex.Length);
                            newIndexes.Add(newIndexForList);
                        }

                        ++newIndex[newIndex.Length - 1];
                    } 
                }

                Console.WriteLine("Found total {0} families.", newIndexes.Count);

                //PrintIndexes(newIndexes);

                oldIndexes = newIndexes;
            }

            int ap = 0;
            foreach (var oldIndex in oldIndexes)
            {
                var newIndex = new int[oldIndex.Length + 1];

                for (int i = 0; i < newIndex.Length - 1; ++i)
                    newIndex[i] = oldIndex[i];

                newIndex[newIndex.Length - 1] = newIndex[newIndex.Length - 2] + 1;

                while (newIndex[newIndex.Length - 1] < maxNumber)
                {
                    if (CheckIndexes(newIndex))
                    {
                        ap++;

                        int sum = 0;
                        foreach (var index in newIndex)
                            sum += _primes[index];

                        if (sum < lowestSum)
                        {
                            lowestSum = sum;

                            string numbers = "";
                            foreach (var index in newIndex)
                                numbers += string.Format("{0}, ", _primes[index]);

                            Console.WriteLine("Sum: {0}. {1}", lowestSum, numbers);
                        }
                    }

                    ++newIndex[newIndex.Length - 1];
                }
            }
        }

        private static void PrintIndexes(IEnumerable<int[]> indexes)
        {
            foreach (var oldIndex in indexes)
            {
                string numbers = "";
                foreach (var index in oldIndex)
                    numbers += string.Format("{0}, ", _primes[index]);

                Console.WriteLine(numbers);
            }

            Console.WriteLine();
        }

        private static bool CheckIndexes(int[] indexes)
        {
            bool flag = true;
            for (int k = 0; k < indexes.Length; ++k)
            {
                for (int l = k + 1; l < indexes.Length; ++l)
                {
                    if (k == l)
                        continue;

                    int firstPrime = _primes[indexes[k]];
                    int secondPrime = _primes[indexes[l]];
                    var firstPow = (int) Math.Pow(10, GetLengthInDecimalDigits(secondPrime));
                    var secondPow = (int) Math.Pow(10, GetLengthInDecimalDigits(firstPrime));

                    int firstComplexPrime = secondPrime * secondPow + firstPrime;
                    int secondComplexPrime = firstPrime * firstPow + secondPrime;

                    flag &= IsPrime(_primes, firstComplexPrime) && IsPrime(_primes, secondComplexPrime);

                    if (!flag)
                        return false;
                }
            }

            return true;
        }

        private static bool IsPrime(int[] primes, int number)
        {
            return Array.BinarySearch(primes, number) >= 0;
        }

        private static int GetLengthInDecimalDigits(int number)
        {
            return (int) (Math.Log10(number) + 1);
        }

        private static int[] GeneratePrimes(int maxNumber)
        {
            var result = new List<int>(maxNumber);

            int currentIndex = 1;
            int currentNumber = 3;

            result.Add(2);

            while (currentNumber <= maxNumber)
            {
                bool flag = true;
                for (int i = 0; i < currentIndex && result[i] < Math.Sqrt(currentNumber) + 1; ++i)
                {
                    if (currentNumber % result[i] == 0)
                    {
                        flag = false;
                        break;
                    }
                }

                if (flag)
                {
                    result.Add(currentNumber);
                    ++currentIndex;

                    if (currentIndex % 10000 == 0)
                        Console.WriteLine("Number {0} is prime at index {1}", currentNumber, currentIndex);
                }

                currentNumber += 2;
            }

            return result.ToArray();
        }
    }
}