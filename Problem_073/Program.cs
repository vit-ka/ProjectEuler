using System;

namespace Problem_073
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            const int maxD = 10000;

            const double etalonLower = 1.0 / 3.0;
            const double etalonUpper = 1.0 / 2.0;

            int count = 0;

            for (int d = 2; d <= maxD; ++d)
            {
                for (int n = 1; n < d; ++n)
                {
                    double value = n / (double)d;

                    if (value > etalonLower && value < etalonUpper)
                    {
                        if (GetHCF(n, d) == 1)
                        {
                            ++count;
                            //Console.WriteLine("{0}/{1} = {2:F16}", n, d, value);
                        }
                    }
                }
            }

            Console.WriteLine("Answer: {0}", count);
        }

        private static int GetHCF(int a, int b)
        {
            while (a > 0)
            {
                int temp = a;
                a = b % a;
                b = temp;
            }

            return b;
        }
    }
}