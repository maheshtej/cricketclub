using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using CricketClubDAL;
using CricketClubDomain;
using CricketClubMiddle.Stats;

namespace CricketClubMiddle
{
    public class Match
    {
        private MatchData _data;
        private DAO myDao = new DAO();

        public Match(int MatchID)
        {
                _data = myDao.GetMatchData(MatchID);
        }

        private Match(MatchData data)
        {
            _data = data;
        }

        public static Match CreateNewMatch(Team opposition, DateTime matchDay, Venue venue, MatchType type, HomeOrAway HomeAway)
        {
            DAO myDao = new DAO();
            int id = myDao.CreateNewMatch(opposition.ID, matchDay, venue.ID, (int)type, HomeAway.ToString().Substring(0, 1).ToUpper());
            return new Match(id);
        }

        public static Match GetNextMatch()
        {
            DAO myDao = new DAO();
            int MatchID = myDao.GetNextMatch(DateTime.Today);
            if (MatchID >= 0)
            {
                return new Match(MatchID);
            }
            else
            {
                return null;
            }
        }

        public static Match GetLastMatch()
        {
            DAO myDao = new DAO();
            int MatchID = myDao.GetPreviousMatch(DateTime.Today);
            if (MatchID >= 0)
            {
                return new Match(MatchID);
            }
            else
            {
                return null;
            }
        }

        public Match GetPreviousMatch()
        {
            DAO myDao = new DAO();
            int MatchID = myDao.GetPreviousMatch(this.MatchDate);
            if (MatchID >= 0)
            {
                return new Match(MatchID);
            }
            else
            {
                return null;
            }
        }

        public static IList<Match> GetFixtures()
        {
            return (from a in GetAll() where a.MatchDate > DateTime.Today select a).OrderBy(a=>a.MatchDate).ToList();
        }

        public static IList<Match> GetResults()
        {
            return (from a in GetAll() where a.MatchDate < DateTime.Today select a).OrderBy(a => a.MatchDate).ToList();
        }

        public static IList<Match> GetResults(DateTime startDate, DateTime endDate)
        {
            return (from a in GetResults() where a.MatchDate > startDate && a.MatchDate < endDate select a).ToList();
        }

        private static IList<Match> GetAll()
        {
            DAO myDAO = new DAO();
            List<MatchData> data = myDAO.GetAllMatches();
            return (from a in data select new Match(a)).ToList();
        }

        public void Save()
        {
            ClearCache();
            if (_data.ID != 0)
            {
                DAO myDao = new DAO();
                myDao.UpdateMatch(_data);
            }
            else
            {
                throw new InvalidOperationException("Match has no Match ID");
            }
        }

        public int ID
        {
            get
            {
                return _data.ID;
            }
        }

        public int VenueID
        {
            get { return _data.VenueID; }
            set { _data.VenueID = value; }
        }

        public Venue Venue
        {
            get
            {
                Venue v = new Venue(this.VenueID);
                return v;
            }
        }

        public string VenueName
        {
            get
            {
                return Venue.Name;
            }
        }

        public DateTime MatchDate
        {
            get { return _data.Date; }
            set { _data.Date = value; }

        }

        public string MatchDateString
        {
            get
            {
                return MatchDate.DayOfWeek.ToString().Substring(0,3) + " " + MatchDate.ToLongDateString();
            }
        }
        
        public MatchType Type
        {
            get { return (MatchType)_data.MatchType; }
            set
            {
                _data.MatchType = (int)value;
            }
        }

        public int OppositionID
        {
            get { return _data.OppositionID; }
            set { _data.OppositionID = value; }
        }

        public Team Opposition
        {
            get { return new Team(this.OppositionID); }
        }

        public Team Us
        {
            get { return new Team(0); }
        }

        public bool Abandoned
        {
            get { return _data.Abandoned; }

            set { _data.Abandoned = value; }
        }

        public Team TossWinner
        {
            get
            {
                if (_data.WonToss == true)
                {
                    //Team 0 is the special case this team
                    return new Team(0);
                }
                else
                {
                    return new Team(this.OppositionID);
                }
            }
        }

        public Team TeamBattingFirst()
        {
            Team teamBattingFirst;
            if (this.TossWinnerBatted)
            {
                teamBattingFirst = this.TossWinner;
            }
            else
            {
                if (0 == TossWinner.ID)
                {
                    teamBattingFirst = Opposition;
                }
                else
                {
                    teamBattingFirst = new Team(0);
                }
            }
            return teamBattingFirst;
        }

        public Team TeamBattingSecond()
        {
            if (TeamBattingFirst().ID == 0)
            {
                return Opposition;
            }
            else
            {
                return new Team(0);
            }
        }

        /// <summary>
        /// Did the special case team (The Village) win the toss - try not to use - prefer TossWinner
        /// </summary>
        public bool WonToss
        {
            get { return _data.WonToss; }
            set { _data.WonToss = value; }
        }

        /// <summary>
        /// Did the side that won the toss choose to Bat?
        /// </summary>
        public bool TossWinnerBatted
        {
            get { return _data.Batted; }
            set { _data.Batted = value; }
        }

        /// <summary>
        /// What did the toss winner do? returns "bat" or "field"
        /// </summary>
        public string TossWinnerElectedTo
        {
            get
            {
                if (TossWinnerBatted == true)
                {
                    return "bat";
                }
                else
                {
                    return "field";
                }
            }
        }

        public HomeOrAway HomeOrAway
        {
            get
            {
                if (_data.HomeOrAway.ToUpper() == "H")
                {
                    return HomeOrAway.Home;
                }
                else
                {
                    return HomeOrAway.Away;
                }
            }

            set
            {
                _data.HomeOrAway = value.ToString().Substring(0, 1);
            }
        }

        public Team HomeTeam
        {
            get
            {
                if (HomeOrAway == HomeOrAway.Home)
                {
                    return Us;
                }
                else
                {
                    return Opposition;
                }
            }
        }

        public Team AwayTeam
        {
            get
            {
                if (HomeOrAway == HomeOrAway.Away)
                {
                    return Us;
                }
                else
                {
                    return Opposition;
                }
            }
        }

        public string HomeTeamName
        {
            get
            {
                return HomeTeam.Name;
            }

        }

        public string AwayTeamName
        {
            get
            {
                return AwayTeam.Name;
            }

        }

        private BattingCard _ourBatting = null;
        public BattingCard GetOurBattingScoreCard()
        {
            if (_ourBatting == null)
            {
                _ourBatting = new BattingCard(this.ID, ThemOrUs.Us);
            }
            return _ourBatting;
        }

        private BattingCard _theirBatting = null;
        public BattingCard GetThierBattingScoreCard()
        {
            if (_theirBatting == null)
            {
                _theirBatting = new BattingCard(this.ID, ThemOrUs.Them);
            }
            return _theirBatting;
        }

        private BowlingStats _ourBowling = null;
        public BowlingStats GetOurBowlingStats()
        {
            if (_ourBowling == null)
            {
                _ourBowling = new BowlingStats(this.ID, ThemOrUs.Us);
            }
            return _ourBowling;
        }

        private BowlingStats _theirBowling = null;
        public BowlingStats GetThierBowlingStats()
        {
            if (_theirBowling == null)
            {
                _theirBowling = new BowlingStats(this.ID, ThemOrUs.Them);
            }
            return _theirBowling;
        }


        /// <summary>
        /// Get the score for the home or away team
        /// </summary>
        /// <param name="HomeAway"></param>
        /// <returns></returns>
        public int GetTeamScore(Team team)
        {
            BattingCard sc = null;
            if (team.ID == Us.ID )
            {
                sc = GetOurBattingScoreCard();
            }
            else if (team.ID == Opposition.ID)
            {
                sc = GetThierBattingScoreCard();
            }
            else
            {
                throw new Exception("The team you specified didn't play in this game");
            }

            var score = (from a in sc.ScorecardData
                         select a.Score).Sum();
            score = score + sc.Extras;
            return score;
        }

        public int GetTeamWicketsDown(Team team)
        {
            BattingCard sc = null;
            if (team.ID == Us.ID)
            {
                sc = GetOurBattingScoreCard();
            }
            else if (team.ID == Opposition.ID)
            {
                sc = GetThierBattingScoreCard();
            }
            else
            {
                throw new Exception("The team you specified didn't play in this game");
            }

            var wickets = (from a in sc.ScorecardData
                          where a.Dismissal != ModesOfDismissal.NotOut
                          where a.Dismissal != ModesOfDismissal.DidNotBat
                          where a.Dismissal != ModesOfDismissal.RetiredHurt
                          where a.BattingAt != 12
                          select a).Count();

            return wickets;
        }

        public decimal GetTeamOversBowled(Team team)
        {
            return 0;
        }

        public string HomeTeamScore
        {
            get
            {
                string result = GetTeamScore(HomeTeam) + " for " + GetTeamWicketsDown(HomeTeam);
                result = result.Replace("for 10", "all out");
                if (HomeOrAway == HomeOrAway.Home && WeDeclared)
                {
                    result = result + " dec";
                }
                if (HomeOrAway == HomeOrAway.Away && TheyDeclared)
                {
                    result = result + " dec";
                }
                return result;
            }
        }

        public string AwayTeamScore
        {
            get
            {
                string result = GetTeamScore(AwayTeam) + " for " + GetTeamWicketsDown(AwayTeam);
                result = result.Replace("for 10", "all out");
                
                if (HomeOrAway == HomeOrAway.Away && WeDeclared)
                {
                    result = result + " dec";
                }
                if (HomeOrAway == HomeOrAway.Home && TheyDeclared)
                {
                    result = result + " dec";
                }
                return result;
            }
        }
        public bool ResultDrawn
        {
            get
            {
                if (this.Type == MatchType.Declaration)
                {
                    if ((GetTeamScore(TeamBattingSecond()) < GetTeamScore(TeamBattingFirst())) && GetTeamWicketsDown(TeamBattingSecond()) < 10)
                    {
                        return true;
                    }
                }
                return false;
            }
        }

        public bool ResultTied
        {
            get
            {
                if (GetTeamScore(Us) == GetTeamScore(Opposition) && GetTeamScore(Us) > 0 && GetTeamScore(Opposition) > 0)
                {
                    return true;
                }
                return false;
            }
        }

        public Team Winner
        {
            get
            {
                if (GetTeamScore(Us) > GetTeamScore(Opposition) && !ResultDrawn)
                {
                    return Us;
                }
                else if (GetTeamScore(Us) < GetTeamScore(Opposition) && !ResultDrawn)
                {
                    return Opposition;
                }
                return null;
            }
        }

        public Team Loser
        {
            get
            {
                if (GetTeamScore(Us) < GetTeamScore(Opposition) && !ResultDrawn)
                {
                    return Us;
                }
                else if (GetTeamScore(Us) > GetTeamScore(Opposition) && !ResultDrawn)
                {
                    return Opposition;
                }
                return null;
            }
        }

        public string ResultText
        {
            get 
            {
                if (Abandoned)
                {
                    return "abandoned";
                } 
                else if (Winner != null && Winner.ID == Us.ID)
                {
                    if (HomeOrAway == HomeOrAway.Home)
                    {
                        return "beat";
                    }
                    else
                    {
                        return "lost to";
                    }
                }
                else if(Winner != null && Winner.ID == Opposition.ID)
                {
                    if (HomeOrAway == HomeOrAway.Away)
                    {
                        return "beat";
                    }
                    else
                    {
                        return "lost to";
                    }
                }
                else if (ResultDrawn)
                {
                    return "drew with";
                }
                else if (ResultTied)
                {
                    return "tied with";
                }
                else
                {
                    return "vs";
                }

            }
        }

        public string ResultMargin
        {
            get
            {
                
                if (!(null==this.Winner))
                {
                    if (TeamBattingFirst().ID == Winner.ID)
                    {
                        int margin = GetTeamScore(Winner) - GetTeamScore(Loser);
                        string resultText = "by " + margin + " runs";
                        if (margin == 1)
                        {
                            resultText = resultText.Substring(0, resultText.Length - 1);
                        }
                        return resultText;
                    }
                    else
                    {
                        int margin = 10 - GetTeamWicketsDown(Winner);
                        string resultText = "by " + margin + " wickets";
                        if (margin == 1)
                        {
                            resultText = resultText.Substring(0, resultText.Length - 1);
                        }
                        return resultText;
                    }
                }
                else
                {
                    if (!Abandoned && !ResultDrawn)
                    {
                        return "result not yet in";
                    }
                    else if (ResultDrawn)
                    {
                        return "";
                    }
                    else
                    {
                        return "no result";
                    }
                }
            }
        }

        public Player Captain
        {
            get
            {
                return new Player(_data.CaptainID);
            }
            set
            {
                _data.CaptainID = value.ID;
            }
        }

        public Player WicketKeeper
        {
            get
            {
                return new Player(_data.WicketKeeperID);
            }
            set
            {
                _data.WicketKeeperID = value.ID;
            }
        }

        public int Overs
        {
            get
            {
                return _data.Overs;
            }
            set
            {
                _data.Overs = value;
            }
        }

        public bool WasDeclaration
        {
            get
            {
                return _data.WasDeclarationGame;
            }
            set
            {
                _data.WasDeclarationGame = value;
            }
        }

        public bool WeDeclared
        {
            get
            {
                return _data.WeDeclared;
            }
            set
            {
                _data.WeDeclared = value;
            }
        }

        public bool TheyDeclared
        {
            get
            {
                return _data.TheyDeclared;
            }
            set
            {
                _data.TheyDeclared = value;
            }
        }

        public double OurInningsLength
        {
            get
            {
                return _data.OurInningsLength;
            }
            set
            {
                _data.OurInningsLength = value;
            }
        }
        public double TheirInningsLength
        {
            get
            {
                return _data.TheirInningsLength;
            }
            set
            {
                _data.TheirInningsLength = value;
            }
        }

        public override string ToString()
        {
            return this.Opposition.Name + " (" + this.MatchDate.ToShortDateString() + ")";
        }

        public void ClearCache()
        {
            _ourBatting = null;
            _ourBowling = null;
            _theirBatting = null;
            _theirBowling = null;
        }

        public MatchReport GetMatchReport(string folder)
        {
            return new MatchReport(ID, folder);
        }

    }



       
}
