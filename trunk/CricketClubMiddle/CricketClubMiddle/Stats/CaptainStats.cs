using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CricketClubDomain;

namespace CricketClubMiddle.Stats
{
    public class CaptainStats
    {
        private Player _player;
        private List<Match> FilteredMatchData;

        public Player Player
        {
            get
            {
                return _player;
            }
        }

        private int ID
        {
            get;
            set;
        }

        public CaptainStats(Player player, DateTime fromDate, DateTime toDate, List<MatchType> matchTypes, Venue venue)
        {
            _player = player;
            ID = player.ID;
            FilteredMatchData = MatchData.Where(a => a.MatchDate > fromDate).Where(a => a.MatchDate < toDate).Where(a => matchTypes.Contains(a.Type)).Where(a => venue==null || a.VenueID == venue.ID).ToList();
        }

        private List<Match> MatchData
        {
            get
            {
                InternalCache cache = InternalCache.GetInstance();
                if (cache.Get("CaptainsMatchData_" + Player.ID) == null)
                {
                    List<Match> allMatches;
                    if (Player.ID != 0)
                    {
                        allMatches = Match.GetResults().Where(a => a.OppositionID == Player.ID).ToList();
                    }
                    else
                    {
                        allMatches = Match.GetResults().ToList();
                    }
                    cache.Insert("TeamMatchData_" + Player.ID, allMatches, new TimeSpan(365, 0, 0, 0));
                    return allMatches;
                }
                else
                {
                    return (List<Match>)cache.Get("TeamMatchData_" + Player.ID);
                }
            }
        }

    }
}
