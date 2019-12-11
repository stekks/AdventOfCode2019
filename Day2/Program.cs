using System;
using System.Collections.Generic;

namespace Day2
{
    class Program
    {
        static void Main(string[] args)
        {
            var intCodeComputer = new IntCodeComputer(@"input.txt");

            //intCodeComputer.DoRepaceBefore();
            //var list = intCodeComputer.ParseList();

            //PrintList(list);

            //Console.WriteLine(list[0]);
            //intCodeComputer.FindNounAndVerb(19690720);
            intCodeComputer.Part2();
        }

        static void PrintList<T>(List<T> list)
        {
            Console.WriteLine(string.Join(',', list));
        }
    }
}
