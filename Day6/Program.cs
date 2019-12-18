using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Day6
{
    class Program
    {
        private static Dictionary<string, Planet> planets = new Dictionary<string, Planet>();

        static void Main(string[] args)
        {
            ReadPlanets();
            Part1();
            Part2();
        }

        private static void Part1()
        {
            ReadPlanets();

            int distanceFromCoM = 0;

            Console.WriteLine($"Number of planets found: {planets.Count}");

            foreach (KeyValuePair<string, Planet> item in planets)
            {
                distanceFromCoM += item.Value.GetDistanceToCom();
            }

            Console.WriteLine();
            Console.WriteLine($"Total number of orbits: {distanceFromCoM}");
        }

        private static void Part2()
        {
            // make chain of planets to CoM
            planets.TryGetValue("YOU", out Planet planetYou);
            planets.TryGetValue("SAN", out Planet planetSan);

            var pathYou = GetPathToCom(planetYou);
            var pathSan = GetPathToCom(planetSan);

            // find last common planet
            string lastCommonPlanet = string.Empty;
            int i=0;
            while(pathYou[i]==pathSan[i])
            {
                lastCommonPlanet = pathYou[i++];
            }

            // calculate distans from last common planets between YOU and SAN
            var distanceYou = planetYou.GetDistanceToPlanet(lastCommonPlanet);
            var distanceSan = planetSan.GetDistanceToPlanet(lastCommonPlanet);

            Console.WriteLine($"Total number of orbits: {distanceYou}+{distanceSan}={distanceYou + distanceSan}. LastCommonPlanet = {lastCommonPlanet}");
        }

        private static void ReadPlanets()
        {
            var fileStream = new FileStream(@"input.txt", FileMode.Open, FileAccess.Read);

            using (var streamReader = new StreamReader(fileStream, Encoding.UTF8))
            {
                string line;

                while ((line = streamReader.ReadLine()) != null)
                {
                    var orbitDescriptor = line.Split(')');
                    var centerPlanet = GetOrCreatePlanet(orbitDescriptor[0]);
                    var orbitingPlanet = GetOrCreatePlanet(orbitDescriptor[1]);

                    centerPlanet.AddOrbitingPlanet(orbitingPlanet);
                };
            }
        }

        private static Planet GetOrCreatePlanet(string planetName)
        {
            planets.TryGetValue(planetName, out Planet planet);
            if (planet == null)
            {
                planet = new Planet(planetName);
                planets.Add(planetName, planet);
            }

            return planet;
        }

        private static List<string> GetPathToCom(Planet planet)
        {
            var path = new List<string>();

            Planet parent = planet.Center;
            while (parent != null) // Center of mass has no 'center'/parent planet
            {
                path.Add(parent.Name);
                parent = parent.Center;
            }

            path.Reverse();
            return path;
        }
    }
}
