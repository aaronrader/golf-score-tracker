using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace scoretracker
{
    internal class Score
    {
        public string? CourseName { get; set; }
        public string? Player { get; set; }
        public int Strokes { get; set; }
        public string? Date { get; set; }
        public int TotalScore { get; set; }

        public Score() { }

        public Score(string courseName, string date, string player, int strokes)
        {
            this.CourseName = courseName;
            this.Date = date;
            this.Player = player;
            this.Strokes = strokes;
        }
    }
}
