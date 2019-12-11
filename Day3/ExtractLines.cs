using System.Collections.Generic;
using System.Drawing;

namespace Day3
{
    public class ExtractLines
    {
        private readonly List<Line> lines = new List<Line>();
        private readonly string[] routes;

        public ExtractLines(string path)
        {
            routes = path.Split(',');
            PreviousLine = null;
        }

        public Line PreviousLine { get; set; }

        public List<Line> Lines()
        {
            var origin = new Point(0, 0);

            foreach (string route in routes)
            {
                var direction = route[0];
                var length = int.Parse(route.Substring(1));
                var end = new Point(origin.X, origin.Y);

                switch (direction)
                {
                    case 'L':
                        end.X -= length;
                        break;
                    case 'U':
                        end.Y += length;
                        break;
                    case 'R':
                        end.X += length;
                        break;
                    case 'D':
                        end.Y -= length;
                        break;
                }

                var newLine = new Line(new Point(origin.X, origin.Y), new Point(end.X, end.Y), PreviousLine);
                lines.Add(newLine);
                origin = end;
                PreviousLine = newLine;
            }

            return lines;
        }
    }
}
