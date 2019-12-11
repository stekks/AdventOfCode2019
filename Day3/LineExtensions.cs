using System;
using System.Drawing;

namespace Day3
{
    public static class LineExtensions
    {
        public static Point? Intersection(this Line lineA, Line lineB)
        {
            if (lineA.Orientation == lineB.Orientation)
            {
                return null;
            }

            Line vertical = lineA.Orientation == Orientation.Vertical ? lineA : lineB;
            Line horizontal = lineA.Orientation == Orientation.Horizontal ? lineA : lineB;

            if ((horizontal.StartY > vertical.MinY) && (horizontal.StartY < vertical.MaxY) && (horizontal.MinX < vertical.StartX) && (horizontal.MaxX > vertical.StartX))
            {
                var intersection = new Point(vertical.StartX, horizontal.StartY);

                Console.WriteLine($"Intersection found at {intersection.X}, {intersection.Y}");
                Console.WriteLine("LineA " + lineA);
                Console.WriteLine("LineB " + lineB);
               
                return intersection;
            }

            return null;
        }
    }
}
