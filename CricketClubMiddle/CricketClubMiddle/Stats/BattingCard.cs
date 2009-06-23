using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CricketClubDAL;
using CricketClubDomain;

namespace CricketClubMiddle.Stats
{
    public class BattingCard
    {
        /// <summary>
        /// Get a scorecard for a game
        /// </summary>
        /// <param name="match_id">The id of the match to fetch the scorecard for</param>
        /// <param name="BatOrBowl">Do you want the website team's batting or bowling innings?</param>
        public BattingCard(int MatchID, ThemOrUs us)
        {
            DAO myDAO = new DAO();
            ScorecardData = (from a in myDAO.GetBattingCard(MatchID, us).Where(a=>a.PlayerID != -1).Where(a=>a.PlayerName != "(Frank) Extras")
                                select new BattingCardLine(a)).ToList();
            this.MatchID = MatchID;
            try
            {
                if (ThemOrUs.Us == us)
                {
                    this.Extras = myDAO.GetBattingCard(MatchID, us).Where(a => a.PlayerID == -1).FirstOrDefault().Score;
                }
                else
                {
                    this.Extras = myDAO.GetBattingCard(MatchID, us).Where(a => a.ModeOfDismissal == -1).FirstOrDefault().Score;
                }
            }
            catch
            {
                this.Extras = 0;
            }
        }
        /// <summary>
        /// Create a brand new scorecard
        /// </summary>
        public static BattingCard CreateNewScorecard(int MatchID, BattingOrBowling BattingBowling)
        {
            return null;
        }

        public int MatchID
        {
            get;
            private set;
        }

        public List<BattingCardLine> ScorecardData
        {
            get;
            set;
        }

        public int Extras
        {
            get;
            set;
        }

        public int Total
        {
            get
            {
                var score = (from a in ScorecardData select a.Score).Sum();
                score = score + Extras;
                return score;
            }
        }

        public void Save(BattingOrBowling BatOrBowl)
        {
            DAO myDao = new DAO();
            List<BattingCardLineData> data = new List<BattingCardLineData>();
            foreach (BattingCardLine line in this.ScorecardData)
            {
                BattingCardLineData item = new BattingCardLineData();
                item.Fours = line.Fours;
                item.BattingAt = line.BattingAt;
                item.BowlerName = line.Bowler.Name;
                item.BowlerID = line.Bowler.ID;
                item.FielderID = line.Fielder.ID;
                item.FielderName = line.Fielder.Name;
                item.MatchID = this.MatchID;
                item.MatchDate = new Match(MatchID).MatchDate;
                item.ModeOfDismissal = (int)line.Dismissal;
                item.PlayerID = line.Batsman.ID;
                item.PlayerName = line.PlayerName;
                item.Runs = line.Score;
                item.Score = line.Score;
                item.Sixes = line.Sixes;
                data.Add(item);
            }
            myDao.UpdateScoreCard(data, this.Extras, BatOrBowl);
        }




    }
}
