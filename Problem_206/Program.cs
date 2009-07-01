using System;

namespace Problem_206
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            for (long number = 100000000L; number < 138902662L; ++number)
            {
                bool result = CheckNumber(number * number * 100);

                if (result)
                {
                    Console.WriteLine("Answer: {0}", number * 10);
                    break;
                }
            }
        }

        private static bool CheckNumber(long number)
        {
            byte[] digits = new byte[(int) (Math.Log10(number) + 1)];

            for (int i = 0; i < digits.Length; ++i)
            {
                digits[i] = (byte) (number % 10);
                number /= 10;
            }

            for(int i = 0; i <= 9; ++i)
            {
                if (digits[digits.Length - 2 * i - 1] != (i + 1) % 10)
                    return false;
            }

            return true;
        }
    }
}