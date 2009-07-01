using System;

namespace Problem_100
{
    internal class Program
    {

        private static void Main(string[] args)
        {
            const long maxNumber = 1000000000000L;

            long b = 85;
            long n = 120;

            while (n < maxNumber)
            {
                long newB = 3 * b + 2 * n - 2;
                n = 4 * b + 3 * n - 3;
                b = newB;
            }

            Console.WriteLine("Answer: {0}", b);
        }
    }
}