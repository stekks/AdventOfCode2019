using System;
using System.IO;
using System.Text;

namespace Day1
{
    class Program
    {
        static void Main(string[] args)
        {
            Part1();
            Part2();
        }

        private static void Part1()
        {
            var fileStream = new FileStream(@"test.txt", FileMode.Open, FileAccess.Read);
            int sum = 0;

            using (var streamReader = new StreamReader(fileStream, Encoding.UTF8))
            {
                string line;
                while ((line = streamReader.ReadLine()) != null)
                {
                    int.TryParse(line, out int mass);
                    sum += (mass / 3) - 2;
                }
            }

            Console.WriteLine($"Part 1 Sum: {sum}");
        }

        private static void Part2()
        {
            var fileStream = new FileStream(@"input.txt", FileMode.Open, FileAccess.Read);
            int sum = 0;

            using (var streamReader = new StreamReader(fileStream, Encoding.UTF8))
            {
                string line;
                while ((line = streamReader.ReadLine()) != null)
                {
                    int.TryParse(line, out int mass);
                    int add = 0;

                    do
                    {
                        add = (mass / 3) - 2;
                        if (add > 0)
                        {
                            sum += add;
                        }
                        mass = add;
                    }
                    while (add > 0);
                }
            }

            Console.WriteLine($"Part 2 Sum: {sum}");
        }
    }
}
