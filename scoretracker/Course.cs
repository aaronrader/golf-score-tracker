using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace scoretracker
{
    internal class Course
    {
        internal class Hole
        {
            public int Par;
            public int Distance;

            public Hole(int par, int distance)
            {
                this.Par = par;
                this.Distance = distance;
            }
        }

        public readonly string Name;
        public int NumHoles { get; set; }
        public string? Section { get; set; }
        public int Par { get; set; }
        public List<Hole> Holes { get;  set; }

        public Course(string name)
        {
            this.Holes = new List<Hole>();
            this.Name = name;
            this.Par = 0;
        }

        /// <summary>
        /// Adds a hole to the course.
        /// </summary>
        /// <param name="par"></param>
        /// <param name="distance"></param>
        /// <exception cref="Exception"></exception>
        public void AddHole(int par, int distance)
        {
            if (this.Holes.Count >= this.NumHoles)
            {
                throw new Exception("Hole list exceeded the number of holes");
            }
            this.Par += par;
            this.Holes.Add(new Hole(par, distance));
        }

        public override string ToString()
        {
            int totalDistance = 0;
            foreach (Hole hole in this.Holes)
            {
                totalDistance += hole.Distance;
            }

            return $" Course - {this.Name} ({this.Section})\n" +
                $" Holes - {this.NumHoles}\n" +
                $" Par - {this.Par}\n" +
                $" Distance - {totalDistance.ToString("N0")} yds";
        }
    }
}
