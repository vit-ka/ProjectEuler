using System;

namespace Problem_072
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            const int maxD = 1000000;

            long count = 0;

            for (int d = 2; d <= maxD; ++d)
            {
                for (int n = 1; n < d; ++n)
                {
                    if (GetHCF(n, d) == 1)
                    {
                        ++count;

                        if (count % 1000000 == 0)
                            Console.WriteLine("{1}/{2}: Found {0:#,#}th fraction.", count, n, d);
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