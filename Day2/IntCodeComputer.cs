using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Day2
{
    public class IntCodeComputer
    {
        private List<int> originalList;

        public IntCodeComputer(string path)
        {
            this.ReadList(path);
        }

        /// <summary>
        /// only for the 'real' assignment
        /// </summary>
        internal void DoRepaceBefore()
        {
            originalList[1] = 12;
            originalList[2] = 2;
        }

        internal void ReadList(string path)
        {
            this.originalList = new List<int>();
            string[] array;
            var fileStream = new FileStream(path, FileMode.Open, FileAccess.Read);

            using (var streamReader = new StreamReader(fileStream, Encoding.UTF8))
            {
                var line = streamReader.ReadLine();
                array = line.Split(',');
            }

            originalList.AddRange(from string number in array
                                  select int.Parse(number));

        }

        public List<int> ParseList()
        {
            int instructionPointer = 0;
            var workList = new List<int>(originalList);

            while (ExcecuteOpcode(workList, instructionPointer) && instructionPointer < workList.Count)
            {
                instructionPointer += 4;
            }

            return workList;
        }

        public bool ExcecuteOpcode(List<int> workList, int instructionPointer)
        {
            var opcode = workList[instructionPointer];
            if (opcode == 99)
            {
                return false;
            }

            var input1pointer = workList[instructionPointer + 1];
            var input2pointer = workList[instructionPointer + 2];
            var resultpointer = workList[instructionPointer + 3];
            var waarde1 = workList[input1pointer];
            var waarde2 = workList[input2pointer];

            switch (opcode)
            {
                case 1:
                    workList[resultpointer] = waarde1 + waarde2;
                    break;
                case 2:
                    workList[resultpointer] = waarde1 * waarde2;
                    break;
                case 99: return false;
                default: throw new ArgumentException("Oops, this isn't suppose to happen.");
            }

            return true;
        }

        public void Part2()
        {
            for (int noun = 0; noun < 100; noun++)
            {
                for (int verb = 0; verb < 100; verb++)
                {
                    var worklist = new List<int>(originalList);
                    worklist[1] = noun;
                    worklist[2] = verb;

                    int instructionPointer = 0;
                    while (worklist[instructionPointer] != 99)
                    {
                        int opcode = worklist[instructionPointer];

                        if (opcode == 1)
                        {
                            worklist[worklist[instructionPointer + 3]] = worklist[worklist[instructionPointer + 2]] + worklist[worklist[instructionPointer + 1]];
                        }
                        else if (opcode == 2)
                        {
                            worklist[worklist[instructionPointer + 3]] = worklist[worklist[instructionPointer + 2]] * worklist[worklist[instructionPointer + 1]];
                        }

                        instructionPointer += 4;
                    }

                    if (worklist[0] == 19690720)
                    {
                        Console.WriteLine(100 * noun + verb);
                        return;
                    }
                }
            }
        }
    }
}
