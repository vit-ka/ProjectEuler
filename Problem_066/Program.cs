using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace Problem_066
{
    class Program
    {
        private const long _maxY = long.MaxValue;
        private const double _eps = 1e-10;
        private const long _maxSquare = 1000;

        private static readonly HashSet<BigInteger> _squares = new HashSet<BigInteger>();

        private struct Frac
        {
            public BigInteger x, y;

            public Frac(BigInteger x, BigInteger y)
            {
                this.x = x;
                this.y = y;
            }

        }

        static void Main(string[] args)
        {
            InitSquares();

            IList<BigInteger> answers = new List<BigInteger>();

            for (long D = 2; D <= 1000; ++D)
                answers.Add(FindXForDiophantineEquation(D));

//            var result = answers.Max();
//
//            Console.WriteLine(result.x + " " + result.y);
        }

        private static void InitSquares()
        {
            for (long i = 2; i < _maxSquare; ++i)
                _squares.Add(i*i);
        }

        private static BigInteger FindXForDiophantineEquation(long D)
        {
            Console.Write("D: {0}", D);
            if (_squares.Contains(D))
            {
                Console.WriteLine(", X: {0}, Y: {1}", -1, -1);
                return -1;
            }

            var sqrtD = Math.Sqrt(D);

            int currentPrecission = 0;
            var frac = ApproximateFraction(sqrtD, currentPrecission);
            while (frac.x * frac.x + D * frac.y * frac.y != 1)
            {
                frac = ApproximateFraction(sqrtD, ++currentPrecission);
            }

            Console.WriteLine(", X: {0}, Y: {1}", frac.x, frac.y);

            return frac.x;
        }

        private static Frac ApproximateFraction(double d, int precision)
        {
            if (precision > 18)
                precision = 18;

            var decimator = new BigInteger((long)Math.Pow(10, precision));
            var numenator = new BigInteger((long) (d*Math.Pow(10, precision)) + 0.5);
            var gcd = BigInteger.GreatestCommonDivisor(numenator, decimator);

            Console.WriteLine("Sqrt(D): {0}, precision: {1}, numenator: {2}, decimator: {3}, gcd: {4}", d, precision,
                              numenator, decimator, gcd);
            
            decimator /= gcd;
            numenator /= gcd;

            return new Frac(numenator, decimator);
        }
    }
}
