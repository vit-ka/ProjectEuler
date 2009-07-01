using System;

namespace Problem_058
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            const long maxSize = 1000000;

            long dx = 0;
            long dy = 0;

            long vectorX = 1;
            long vectorY = 0;

            long totalCountAtRow = 2;
            long currentCountAtRow = 0;
            bool isXRowFilled = false;

            long currentNumber = 1;
            long primesCount = 0;
            long totalCount = 0;

            for (long size = 3; size < maxSize; size += 2)
            {
                while (currentNumber <= size * size)
                {
                    if (Math.Abs(dx) == Math.Abs(dy))
                    {
                        ++totalCount;
                        primesCount += IsPrime(currentNumber) ? 1 : 0;
                    }

                    ++currentCountAtRow;

                    // Перевычисляем вектора.
                    if (currentCountAtRow == totalCountAtRow)
                    {
                        // Если ряд по X еще не был заполнен,
                        // то переходим к заполнению Y.
                        if (!isXRowFilled)
                        {
                            vectorY = totalCountAtRow % 2 == 0 ? -1 : 1;
                            vectorX = 0;
                            currentCountAtRow = 1;
                            isXRowFilled = true;
                        }
                        else
                        {
                            vectorY = 0;
                            vectorX = totalCountAtRow % 2 == 0 ? -1 : 1;
                            ++totalCountAtRow;
                            currentCountAtRow = 1;
                            isXRowFilled = false;
                        }
                    }

                    ++currentNumber;
                    dx += vectorX;
                    dy += vectorY;
                }


                double percent = primesCount / (double)totalCount;

                Console.WriteLine("Side: {0}. Prime's percent: {1:F16}", size, percent * 100);

                if (percent < 0.1)
                    break;
            }
        }

        private static bool IsPrime(long number)
        {
            if (number == 1)
                return false;

            if (number % 2 == 0)
                return false;

            for (long i = 3; i < Math.Sqrt(number) + 1; i += 2)
            {
                if (number % i == 0)
                    return false;
            }

            return true;
        }
    }
}