using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;

namespace Day3
{
    class Program
    {
        static void Main(string[] args)
        {
            var fileStream = new FileStream("input.txt", FileMode.Open, FileAccess.Read);
            string pathA;
            string pathB;

            using (var streamReader = new StreamReader(fileStream, Encoding.UTF8))
            {
                pathA = streamReader.ReadLine();
                pathB = streamReader.ReadLine();
            }

            var linesA = new ExtractLines(pathA).Lines();
            var linesB = new ExtractLines(pathB).Lines();

            var intersections = new List<Point>();
            var totalLengthToOrigin = int.MaxValue;

            foreach (Line lineA in linesA)
            {
                foreach (Line lineB in linesB)
                {
                    Point? intersection = lineA.Intersection(lineB);

                    if (intersection != null)
                    {
                        intersections.Add(intersection.Value);
                        totalLengthToOrigin = CalculateTotalLengthToOrigin(totalLengthToOrigin, lineA, lineB, intersection);
                    }
                }
            }

            Console.WriteLine($"Number of intersections: {intersections.Count}");

            var distance = CalculateIntersectionClosestToOrigin(intersections);
            Console.WriteLine(distance);

            Console.WriteLine($"Minimum total wirelength to origin: {totalLengthToOrigin}");
        }

        private static int CalculateIntersectionClosestToOrigin(List<Point> intersections)
        {
            var distance = int.MaxValue;

            foreach (Point intersection in intersections)
            {
                Console.WriteLine($"({intersection.X},{intersection.Y})");

                var distanceFromOrigin = Math.Abs(intersection.X) + Math.Abs(intersection.Y);

                if ((distanceFromOrigin < distance) && (distanceFromOrigin != 0))
                {
                    distance = distanceFromOrigin;
                }
            }

            return distance;
        }

        private static int CalculateTotalLengthToOrigin(int totalLengthToOrigin, Line lineA, Line lineB, Point? intersection)
        {
            var totalLength = lineA.LengthToPoint(intersection.Value) + lineB.LengthToPoint(intersection.Value);
            var previousLine = lineA.PreviousLine;
            while (previousLine != null)
            {
                totalLength += previousLine.Length;
                previousLine = previousLine.PreviousLine;
            }

            previousLine = lineB.PreviousLine;
            while (previousLine != null)
            {
                totalLength += previousLine.Length;
                previousLine = previousLine.PreviousLine;
            }

            return totalLength < totalLengthToOrigin ? totalLength : totalLengthToOrigin;
        }
    }
}
