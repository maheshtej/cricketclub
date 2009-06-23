using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CricketClubMiddle;
using CricketClubMiddle.Stats;

namespace CricketClubFantasyFives
{
    public class FantasyPlayer
    {
        private Player _player;

        public FantasyPlayer(Player player)
        {
            _player = player;
        }

        public int GetScore(Match match, PlayerType role)
        {
            int score = 0;
            ScoringModel sm = ScoringModel.GetCurrentScoringModel();
            BattingCard card = match.GetOurBattingScoreCard();
            foreach (var line in card.ScorecardData.Where(a=> a.Batsman.ID == _player.ID))
            {
                score += line.Score * sm.PointsPerRun;
                //... etc
            }
            return score;
        }

    }

    public enum PlayerType {
        Batsman, Bowler, AllRounder
    }
}
