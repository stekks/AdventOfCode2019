using System;
using System.Collections.Generic;

namespace Day4
{
    class Program
    {
        static void Main(string[] args)
        {
            var min = 235741;
            var max = 706948;
            var password = min;
            var possiblePasswords = new List<int>();

            while (password < max)
            {
                var next = CalculateNext(password);
                password = int.Parse(new string(next));

                if (HasTwoAdjecentDigitsButNoMore(next))
                {
                    possiblePasswords.Add(password);
                }
            }

            Console.WriteLine($"Possible passwords: {possiblePasswords.Count}");
        }

        static char[] CalculateNext(int start)
        {
            var password = (start + 1).ToString().ToCharArray();

            for (int i = 0; i < 5; i++)
            {
                if (password[i] > password[i + 1])
                {
                    password[i + 1] = password[i];
                }
            }

            return password;
        }

        private static bool HasTwoAdjecentDigits(char[] password)
        {
            for (int i = 0; i < 5; i++)
            {
                if (password[i] == password[i + 1])
                {
                    return true;
                }
            }

            return false;
        }

        private static bool HasTwoAdjecentDigitsButNoMore(char[] password)
        {
            int twoAdjecent = 0;
            var matchingAdjecent = 0;

            for (int i = 0; i < 5; i++)
            {
                while ((i < 5) && (password[i] == password[i + 1]))
                {
                    matchingAdjecent++;
                    i++;
                } 

                if(matchingAdjecent == 1)
                {
                    twoAdjecent++;
                }

                matchingAdjecent = 0;
            }

            return twoAdjecent > 0;
        }
    }
}
