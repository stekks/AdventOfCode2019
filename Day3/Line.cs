using System;
using System.Drawing;

namespace Day3
{
    public enum Orientation
    {
        Vertical = 0,
        Horizontal
    }

    public class Line
    {
        private Point start;
        private Point end;
        private Line previousLine;

        public Line(Point start, Point end, Line previousLine)
        {
            this.start = start;
            this.end = end;
            this.previousLine = previousLine;
        }

        public int StartY => start.Y;
        public int StartX => start.X;
        public int EndY => end.Y;
        public int EndX => end.X;
        public int MaxY => Math.Max(start.Y, end.Y);
        public int MinY => Math.Min(start.Y, end.Y);
        public int MaxX => Math.Max(start.X, end.X);
        public int MinX => Math.Min(start.X, end.X);
        public bool IsHorizontal => StartY == EndY;
        public bool IsVertical => StartX == EndX;
        public Orientation Orientation => IsVertical ? Orientation.Vertical : Orientation.Horizontal;
        public int Length => Math.Abs(EndY - StartY) + Math.Abs(EndX - StartX);
        public Line PreviousLine => previousLine;
        public int LengthToPoint(Point point)
        {
            if (Orientation == Orientation.Vertical)
            {
                return Math.Abs(point.Y - StartY);
            }

            return Math.Abs(point.X - StartX);
        }

        public override string ToString()
        {
            return $"({StartX},{StartY})-({EndX},{EndY}), Length={Length}";
        }
    }
}
