using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CricketClubMiddle;
using CricketClubMiddle.Interactive;

namespace CricketClubFantasyFives
{
    public class FantasyTeam
    {
        private Match match;
        private User user;

        public FantasyTeam(User user, Match match)
        {
            this.match = match;
            this.user = user;

        }

        public Player Batsman1
        {
            get;
            set;
        }

        public Player Batsman2
        {
            get;
            set;
        }

        public Player AllRounder
        {
            get;
            set;
        }

        public Player Bowler1
        {
            get;
            set;
        }

        public Player Bowler2
        {
            get;
            set;
        }

        public void Save()
        {
            //save it.
        }

        public int GetScore()
        {
            int score = 0;
                
            if (match.MatchDate <= DateTime.Today)
            {
                if (Batsman1 == null)
                {
                    //No team has been created use last team entered.
                    FantasyTeam lastTeam = new FantasyTeam(user, match.GetPreviousMatch());
                    Batsman1 = lastTeam.Batsman1;
                    Batsman2 = lastTeam.Batsman2;
                    AllRounder = lastTeam.AllRounder;
                    Bowler1 = lastTeam.Bowler1;
                    Bowler2 = lastTeam.Bowler2;
                    this.Save();

                }
                score = new FantasyPlayer(Batsman1).GetScore(match, PlayerType.Batsman);
                score += new FantasyPlayer(Batsman2).GetScore(match, PlayerType.Batsman);
                score += new FantasyPlayer(AllRounder).GetScore(match, PlayerType.AllRounder);
                score += new FantasyPlayer(Bowler1).GetScore(match, PlayerType.Bowler);
                score += new FantasyPlayer(Bowler2).GetScore(match, PlayerType.Bowler);
            }
            else
            {
                //match hasn't happened yet
                score = 0;
            }

            return score;
        }

        
    }
}
