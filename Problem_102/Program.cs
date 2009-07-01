using System;
using System.Collections.Generic;
using System.IO;

namespace Problem_102
{
    internal class Program
    {
        private static readonly Point _origin = new Point
            {
                X = 0,
                Y = 0
            };

        private static void Main(string[] args)
        {
            IList<Triangle> triangles = LoadTriangles("triangles.txt");

            int count = 0;

            foreach (Triangle triangle in triangles)
            {
                bool isContainOrigin = IsContainOrigin(triangle);
                Console.WriteLine("Triangle {0}: {1}", triangle, (isContainOrigin ? "YES" : "NO"));

                if (isContainOrigin)
                    ++count;
            }

            Console.WriteLine("Count of triangles which contains origin: {0}", count);
        }

        private static bool IsContainOrigin(Triangle triangle)
        {
            double lengthFromP1P2 = GetLength(triangle.P1, triangle.P2, _origin);
            double lengthFromP2P3 = GetLength(triangle.P2, triangle.P3, _origin);
            double lengthFromP3P1 = GetLength(triangle.P3, triangle.P1, _origin);

            return ((lengthFromP1P2 > 0 && lengthFromP2P3 > 0 && lengthFromP3P1 > 0) ||
                    (lengthFromP1P2 < 0 && lengthFromP2P3 < 0 && lengthFromP3P1 < 0));
        }

        private static double GetLength(Point p1, Point p2, Point origin)
        {
            double a = p2.Y - p1.Y;
            double b = p2.X - p1.X;
            double c = p1.X * p2.Y - p2.X * p1.Y;

            return a * origin.X + b * origin.Y + c;
        }

        private static IList<Triangle> LoadTriangles(string path)
        {
            IList<string> strings = File.ReadAllLines(path);

            IList<Triangle> result = new List<Triangle>();

            foreach (string triangleInStr in strings)
            {
                string[] pointsInStr = triangleInStr.Split(
                    new[]
                        {
                            ','
                        },
                    StringSplitOptions.RemoveEmptyEntries);

                var triangle = new Triangle
                    {
                        P1 = new Point
                            {
                                X = int.Parse(pointsInStr[0]),
                                Y = int.Parse(pointsInStr[1])
                            },
                        P2 = new Point
                            {
                                X = int.Parse(pointsInStr[2]),
                                Y = int.Parse(pointsInStr[3])
                            },
                        P3 = new Point
                            {
                                X = int.Parse(pointsInStr[4]),
                                Y = int.Parse(pointsInStr[5])
                            },
                    };

                result.Add(triangle);
            }

            return result;
        }

        #region Nested type: Point

        private class Point
        {
            public int X { get; set; }
            public int Y { get; set; }

            public override string ToString()
            {
                return string.Format("X: {0}, Y: {1}", X, Y);
            }
        }

        #endregion

        #region Nested type: Triangle

        private class Triangle
        {
            public Point P1 { get; set; }
            public Point P2 { get; set; }
            public Point P3 { get; set; }

            public override string ToString()
            {
                return string.Format("P1: [{0}], P2: [{1}], P3: [{2}]", P1, P2, P3);
            }
        }

        #endregion
    }
}