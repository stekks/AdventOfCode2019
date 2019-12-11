using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Day5
{
    public enum ParamMode
    {
        Position = 0,
        Immediate = 1
    }

    public class IntCodeComputerV2
    {
        private List<int> originalList;

        public IntCodeComputerV2(string path)
        {
            this.ReadList(path);
        }

        internal void InitNounAndVerb(int noun, int verb)
        {
            originalList[1] = noun;
            originalList[2] = verb;
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

            while ((instructionPointer >= 0) && (instructionPointer < workList.Count))
            {
                instructionPointer = ExcecuteOpcode(workList, instructionPointer);
            }

            return workList;
        }

        public int ExcecuteOpcode(List<int> workList, int instructionPointer)
        {
            var opcode = workList[instructionPointer];
            if (opcode == 99)
            {
                return -1;
            }

            int param1;
            int param2;
            int param3;
            int value1;
            int value2;
            ParamMode param1Mode = ParamMode.Position;
            ParamMode param2Mode = ParamMode.Position;
            ParamMode param3Mode = ParamMode.Position;
            
            if (opcode > 99)
            {
                int tempOpcode = opcode;

                // get the opcode
                opcode = tempOpcode % 100;

                // get the param modes
                tempOpcode /= 100;
                if ((tempOpcode % 2) == 1)
                {
                    param1Mode = ParamMode.Immediate;
                }

                tempOpcode /= 10;
                if ((tempOpcode % 2) == 1)
                {
                    param2Mode = ParamMode.Immediate;
                }

                tempOpcode /= 10;
                if ((tempOpcode % 2) == 1)
                {
                    param3Mode = ParamMode.Immediate;
                }
            }

            switch (opcode)
            {
                case 1:
                    param1 = workList[instructionPointer + 1];
                    param2 = workList[instructionPointer + 2];
                    param3 = workList[instructionPointer + 3];

                    value1 = param1Mode == ParamMode.Position ? workList[param1] : param1;
                    value2 = param2Mode == ParamMode.Position ? workList[param2] : param2;
                    workList[param3] = value1 + value2;
                    instructionPointer += 4;
                    break;
                case 2:
                    param1 = workList[instructionPointer + 1];
                    param2 = workList[instructionPointer + 2];
                    param3 = workList[instructionPointer + 3];

                    value1 = param1Mode == ParamMode.Position ? workList[param1] : param1;
                    value2 = param2Mode == ParamMode.Position ? workList[param2] : param2;
                    workList[param3] = value1 * value2;
                    instructionPointer += 4;
                    break;
                case 3:
                    param1 = workList[instructionPointer + 1];
                    Console.Write($"Enter a value for position {param1}: ");
                    var input = Console.ReadLine();
                    workList[param1] = int.Parse(input);
                    instructionPointer += 2;
;                   break;
                case 4:
                    param1 = workList[instructionPointer + 1];
                    Console.WriteLine($"Value for position {param1}= {workList[param1]}");
                    instructionPointer += 2;
                    break;
                case 5:
                    param1 = workList[instructionPointer + 1];
                    param2 = workList[instructionPointer + 2];

                    value1 = param1Mode == ParamMode.Position ? workList[param1] : param1;
                    value2 = param2Mode == ParamMode.Position ? workList[param2] : param2;
                    if (value1 != 0)
                    {
                        instructionPointer = value2;
                    }
                    else
                    {
                        instructionPointer += 3;
                    }
                    break;
                case 6:
                    param1 = workList[instructionPointer + 1];
                    param2 = workList[instructionPointer + 2];

                    value1 = param1Mode == ParamMode.Position ? workList[param1] : param1;
                    value2 = param2Mode == ParamMode.Position ? workList[param2] : param2;
                    if (value1 == 0)
                    {
                        instructionPointer = value2;
                    }
                    else
                    {
                        instructionPointer += 3;
                    }
                    break;
                case 7:
                    param1 = workList[instructionPointer + 1];
                    param2 = workList[instructionPointer + 2];
                    param3 = workList[instructionPointer + 3];

                    value1 = param1Mode == ParamMode.Position ? workList[param1] : param1;
                    value2 = param2Mode == ParamMode.Position ? workList[param2] : param2;

                    workList[param3] = value1 < value2 ? 1 : 0;
                    instructionPointer += 4;
                    break;
                case 8:
                    param1 = workList[instructionPointer + 1];
                    param2 = workList[instructionPointer + 2];
                    param3 = workList[instructionPointer + 3];

                    value1 = param1Mode == ParamMode.Position ? workList[param1] : param1;
                    value2 = param2Mode == ParamMode.Position ? workList[param2] : param2;

                    workList[param3] = value1 == value2 ? 1 : 0;
                    instructionPointer += 4;
                    break;
                default: throw new ArgumentException("Oops, this isn't suppose to happen.");
            }

            return instructionPointer;
        }
    }
}
