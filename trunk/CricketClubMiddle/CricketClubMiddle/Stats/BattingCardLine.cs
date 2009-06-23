using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CricketClubDomain;
using CricketClubDAL;

namespace CricketClubMiddle.Stats
{
    public class BattingCardLine
    {
        private BattingCardLineData _data;

        public BattingCardLine(BattingCardLineData data)
        {
            _data = data;
        }

        public string PlayerName
        {
            get
            {
                if (_data.PlayerID == 0)
                {
                    return _data.PlayerName;
                }
                else
                {
                    Player player = new Player(_data.PlayerID);
                    return player.Name;
                }
            }
            
        }

        public Player Batsman
        {
            get
            {
                if (_data.PlayerID != 0)
                {

                    return new Player(_data.PlayerID);
                }
                else
                {
                    return new Player(_data.PlayerName);
                }
            }
        }

        public Player Fielder
        {
            get
            {
                if (_data.FielderID != 0)
                {
                    return new Player(_data.FielderID);
                }
                else
                {
                    return new Player(_data.FielderName);
                }
            }
        }

        public Player Bowler
        {
            get
            {
                if (_data.BowlerID != 0)
                {
                    return new Player(_data.BowlerID);
                }
                else
                {
                    return new Player(_data.BowlerName);
                }
            }
        }

        public int Score
        {
            get
            {
                return _data.Score;
            }
        }

        public int Fours
        {
            get
            {

                return _data.Fours;

            }
        }

        public int Sixes
        {
            get
            {

                return _data.Sixes;

            }
        }

        public ModesOfDismissal Dismissal
        {
            get
            {
                return (ModesOfDismissal)_data.ModeOfDismissal;
            }
        }

        public int BattingAt
        {
            get
            {
                //DB is zero based!
                return _data.BattingAt;
            }
            set
            {
                if (value >= 1 && value <= 11)
                {
                    _data.BattingAt = value;
                }
                else
                {
                    throw new InvalidOperationException("Batting AT is outside allowed range (1 - 11)");
                }
            }
        }

        public string BowlingDismissalText
        {
            get
            {
                string name = Bowler.Name;
                if (PlayerName == null || PlayerName.Length == 0)
                {
                    name = "unknown";
                }
                ModesOfDismissal howout = (ModesOfDismissal)_data.ModeOfDismissal;
                
                if (howout == ModesOfDismissal.CaughtAndBowled)
                {
                    return "c&b " + name;
                }
                
                if (howout == ModesOfDismissal.Bowled || howout == ModesOfDismissal.Caught)
                {
                    return "b " + name;
                }
                if (howout == ModesOfDismissal.LBW)
                {
                    return "lbw b " + name;
                
                }
                return "";

            }
        }

        public string FieldingDismissalText
        {
            get
            {
                string name = Fielder.Name;
                if (name == null || name.Length==0) {
                    name = "unknown";
                }
                ModesOfDismissal howout = (ModesOfDismissal)_data.ModeOfDismissal;
                if (howout == ModesOfDismissal.Caught)
                {
                    return "c " + name;
                }
                if (howout == ModesOfDismissal.RunOut)
                {
                    return "run out ("+name+")";
                }
                if (howout == ModesOfDismissal.Stumped)
                {
                    return "stumped (" + name + ")";
                }
                if (howout == ModesOfDismissal.HitWicket)
                {
                    return "hit wicket";
                }
                if (howout == ModesOfDismissal.Retired)
                {
                    return "retired";
                }
                if (howout == ModesOfDismissal.RetiredHurt)
                {
                    return "retired hurt";
                }
                if (howout == ModesOfDismissal.NotOut)
                {
                    return "not out";
                }
                return "";
            }
        }

    }
}
