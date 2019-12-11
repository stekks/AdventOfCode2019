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
                default: throw new ArgumentException("Oops, this isn't suppose to happen.");
            }

            return true;
        }
    }
}
