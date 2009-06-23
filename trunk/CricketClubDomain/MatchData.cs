using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CricketClubDomain
{
    public class MatchData
    {
        public MatchData()
        {

        }

        public int ID { get; set; }

        public DateTime Date { get; set; }

        public int OppositionID { get; set; }

        public string HomeOrAway { get; set; }

        public int MatchType { get; set; }

        public int VenueID { get; set; }

        public bool WonToss { get; set; }

        public bool Batted { get; set; }

        public bool Abandoned { get; set; }

        public bool WasDeclarationGame
        {
            get;
            set;
        }

        public int CaptainID { get; set; }
        public int WicketKeeperID { get; set; }

        public int Overs
        {

            get;
            set;
        }

        public bool WeDeclared { get; set; }
        public bool TheyDeclared { get; set; }

        public double OurInningsLength
        {
            get;
            set;
        }

        public double TheirInningsLength
        {
            get;
            set;
        }

    }
}
