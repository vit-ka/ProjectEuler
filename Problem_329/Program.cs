using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Problem_329
{
    class Program
    {
        private readonly int[] _sample = new[]
                                             {
                                                 1, 1, 1, 1, 0, 0, 1, 1, 1, 0, 1, 1, 0, 1, 0
                                             };


        static void Main(string[] args)
        {
            var result = EvalResultForSquare(300, 0, new List<int[]>());
        }

        private static List<int[]> EvalResultForSquare(int squareNumber, int level, List<int[]> prevResult)
        {
            var isSimpleNumber = IsSimpleNumber(squareNumber) ? 1 : 0;

            if (level == 14)
            {
                prevResult.Add(new[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 });
                prevResult.Add(new[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 });
                prevResult.Add(new[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, isSimpleNumber });
                return prevResult;
            }

            var stepResult = new List<int[]>();
            var leftResuls = EvalResultForSquare(squareNumber - 1, level + 1, stepResult);
            var rightResuls = EvalResultForSquare(squareNumber + 1, level + 1, stepResult);
            var additionalResult = new List<int[]>();


            foreach (var item in leftResuls)
            {
                var firstArray = new int[15];
                var secondArray = new int[15];
                
                Array.Copy(item, firstArray, 15);
                Array.Copy(item, secondArray, 15);
                
                firstArray[level] = 1;
                secondArray[level] = isSimpleNumber;

                additionalResult.Add(firstArray);
                additionalResult.Add(secondArray);
            }

            foreach (var item in rightResuls)
            {
                var firstArray = new int[15];
                var secondArray = new int[15];

                Array.Copy(item, firstArray, 15);
                Array.Copy(item, secondArray, 15);

                firstArray[level] = 1;
                secondArray[level] = isSimpleNumber;

                additionalResult.Add(firstArray);
                additionalResult.Add(secondArray);
            }

            leftResuls.AddRange(rightResuls);
            leftResuls.AddRange(additionalResult);

            return leftResuls;
        }

        private static bool IsSimpleNumber(int squareNumber)
        {
            return true;
        }
    }
}
