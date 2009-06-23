﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CricketClubDomain;

namespace CricketClubMiddle.Stats
{
    public class BowlingStatsLine
    {
        internal BowlingStatsEntryData _data;

        public BowlingStatsLine(BowlingStatsEntryData data)
        {
            _data = data;
        }

        public Player Bowler
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

        public string BowlerName
        {
            get { return Bowler.Name; }
        }

        public string ReadableOvers
        {
            get
            {
                return Helpers.ReadableOversString(_data.Overs);
            }
        }

        public decimal Overs
        {
            get
            {
                return _data.Overs;
            }
        }
        
        public int Runs
        {
            get
            {
                return _data.Runs;
            }
        }

        public int Maidens
        {
            get
            {
                return _data.Maidens;
            }
        }

        public int Wickets
        {
            get
            {
                return _data.Wickets;
            }
        }

    }
}
