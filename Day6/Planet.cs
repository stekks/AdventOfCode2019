using System;
using System.Collections.Generic;

namespace Day6
{
    public class Planet
    {
        public Planet(string name)
        {
            this.Name = name;
            this.Orbitees = new List<Planet>();
        }

        public void AddOrbitingPlanet(Planet orbitingPlanet)
        {
            this.Orbitees.Add(orbitingPlanet);
            orbitingPlanet.Center = this;
        }

        public int GetDistanceToCom()
        {
            var distanceToCom = 0;
            Planet parent = this.Center;
            while (parent != null) // Center of mass has no 'center'/parent planet
            {
                distanceToCom++;
                parent = parent.Center;
            }

            return distanceToCom;
        }

        public int GetDistanceToPlanet(string name)
        {
            var distanceToPlanet = 0;
            Planet parent = this.Center;
            while (parent.Name != name)
            {
                distanceToPlanet++;
                parent = parent.Center;

                if(parent==null)
                {
                    throw new ArgumentException($"Planet with '{name}' not found.");
                }
            }

            return distanceToPlanet;
        }

        public Planet Center { get; set; }

        public List<Planet> Orbitees { get; }

        public string Name { get; }
    }
}
