using System;
using System.IO;

namespace Problem_081
{
    internal class Program
    {
        private const int _matrixSize = 80;
        private const int _size = 80;

        private static readonly int[,] _matrix = new int[_matrixSize,_matrixSize];
        private static readonly long[,] _memory = new long[_matrixSize,_matrixSize];

        private static void Main(string[] args)
        {
            LoadMatrix("matrix.txt");

            for (int y = 0; y < _size; ++y)
            {
                for (int x = 0; x < _size; ++x)
                {
                    _memory[x, y] =
                        (x == 0 && y == 0
                             ? 0
                             : Math.Min(
                                   x != 0 ? _memory[x - 1, y] : int.MaxValue,
                                   y != 0 ? _memory[x, y - 1] : int.MaxValue)
                        ) +
                        _matrix[x, y];
                }
            }

            Console.WriteLine("Answer: {0}", _memory[79, 79]);
        }

        private static void LoadMatrix(string path)
        {
            string[] strings = File.ReadAllLines(path);

            int index1 = 0;
            foreach (string str in strings)
            {
                string[] numbers = str.Split(
                    new[]
                        {
                            ','
                        });

                int index2 = 0;
                foreach (string number in numbers)
                {
                    _matrix[index1, index2] = int.Parse(number);
                    ++index2;
                }

                ++index1;
            }
        }
    }
}