using System;
using System.Collections.Generic;

namespace Day2
{
    class Program
    {
        static void Main(string[] args)
        {
            Part2();
        }

        static void Part1()
        {
            var intCodeComputer = new IntCodeComputer(@"input.txt");

            intCodeComputer.InitNounAndVerb(12, 2);
            var list = intCodeComputer.ParseList();

            PrintList(list);

            Console.WriteLine(list[0]);
        }

        static void Part2()
        {
            var intCodeComputer = new IntCodeComputer(@"input.txt");

            for (int noun = 0; noun < 100; noun++)
            {
                for (int verb = 0; verb < 100; verb++)
                {
                    intCodeComputer.InitNounAndVerb(noun, verb);
                    var list = intCodeComputer.ParseList();

                    if (list[0] == 19690720)
                    {
                        Console.WriteLine(100 * noun + verb);
                        return;
                    }
                }
            }
        }

        static void PrintList<T>(List<T> list)
        {
            Console.WriteLine(string.Join(',', list));
        }
    }
}
