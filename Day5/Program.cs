using System;

namespace Day5
{
    class Program
    {
        static void Main(string[] args)
        {
            Part1();
        }

        static void Part1()
        {
            var intCodeComputer = new IntCodeComputerV2(@"input.txt");

            var list = intCodeComputer.ParseList();
        }
    }
}
