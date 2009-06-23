using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CricketClubMiddle;

namespace CricketClubFantasyFives
{
    public class Controler
    {
        public static void UpdateGame(Match match) 
        {
            foreach (User user in User.GetAll()) 
            {
                FantasyTeam team = new FantasyTeam(user, match);
                int score = team.GetScore();
                user.CurrentScore = user.CurrentScore + score;
                user.ScoreForLastMatch = score;
                user.Save();
            }
        }
    }
}
