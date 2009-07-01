using System;

namespace Problem_071
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            const int maxD = 1000000;

            const double etalon = 3.0 / 7.0;
            double minLengthValue = double.MaxValue;
            int minNumerator = 0;

            for (int d = 2; d <= maxD; ++d)
            {
                int center = (int) (3 / 7.0 * d);
                for (int n = center - 1; n < center + 1; ++n)
                {
                    double value = n / (double) d;

                    if (value < etalon && etalon - value < minLengthValue)
                    {
                        if (GetHCF(n, d) == 1)
                        {
                            minLengthValue = etalon - value;
                            minNumerator = n;
                            //Console.WriteLine("{0}/{1} = {2:F16}, d(3/7, x) = {3:F16}", n, d, value, etalon - value);
                        }
                    }
                }
            }

            Console.WriteLine("Answer: {0}", minNumerator);
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