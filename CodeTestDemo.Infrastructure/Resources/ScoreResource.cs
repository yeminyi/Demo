using System;
using System.Collections.Generic;
using System.Text;

namespace CodeTestDemo.Infrastructure.Resources
{
    public class ScoreResource
    {
        public int Id { get; set; }
        public string GameTitle { get; set; }
        public string TeamA { get; set; }
        public string TeamB { get; set; }
        public int TeamAScore { get; set; }
        public int TeamBScore { get; set; }
        public string Employee { get; set; }
        public DateTime UpdateTime { get; set; }

     
    }
}
