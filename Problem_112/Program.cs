using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Problem_112
{
    class Program
    {
        private static bool IsNumberIncreasing(long n)
        {
            byte prevDigit = 10;
            var number = n;

            while (number > 0)
            {
                var currDigit = (byte) (number%10);
                number = number/10;

                if (currDigit > prevDigit)
                    return false;
                prevDigit = currDigit;
            }

            return true;
        }

        private static bool IsNumberDecreasing(long n)
        {
            byte prevDigit = 0;
            var number = n;

            while (number > 0)
            {
                var currDigit = (byte)(number % 10);
                number = number / 10;

                if (currDigit < prevDigit)
                    return false;
                prevDigit = currDigit;
            }

            return true;
        }

        static void Main(string[] args)
        {
            long bouncyCount = 0;
            for (long i = 1; i < 10000000; ++i)
            {
                if (!(IsNumberIncreasing(i) || IsNumberDecreasing(i)))
                {
                    ++bouncyCount;
                }

//                if (i % 1 == 0)
                    //Console.WriteLine("{0}:{1:P10}", i, (bouncyCount)/((double) i));

                if (Math.Abs((bouncyCount) / ((double)i) - 0.99) < 1e-6)
                {
                    Console.WriteLine("{0}:{1:P10}", i, (bouncyCount) / ((double)i));
                    //break;
                }
            }
        }
    }
}
