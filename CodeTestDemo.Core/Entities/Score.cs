using System;
using System.Collections.Generic;
using System.Text;

namespace CodeTestDemo.Core.Entities
{
    public class Score : Entity
    {
        public string GameTitle { get; set; }
        public string TeamA { get; set; }
        public string TeamB { get; set; }

        public int TeamAScore { get; set; }
        public int TeamBScore { get; set; }

        public string Employee { get; set; }
        public DateTime LastModified { get; set; }

     
    }
}
