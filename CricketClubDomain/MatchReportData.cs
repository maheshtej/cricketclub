using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CricketClubDomain
{
    public class MatchReportData
    {
        public MatchReportData() { }

        public string ReportFilename {get; set;}
        public string Password { get; set; }
        public bool HasPhotos { get; set; }
        public int MatchID { get; set; }
    }
}
