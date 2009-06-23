using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CricketClubMiddle;
using CricketClubDomain;
using CricketClubMiddle.Stats;


namespace TestHarness
{
    class Program
    {
        static void Main(string[] args)
        {
            Player player = new Player(1);
            //player.Height = "10000";
            //player.Get4s(DateTime.Now.AddYears(-10), DateTime.Now, MatchType.All);
            //player.Save();

            //Match test = Match.CreateNewMatch(Team.CreateNewTeam("test123"), DateTime.Today, Venue.CreateNewVenue("a venue"), MatchType.Tour, HomeOrAway.Home); 

            Match test = new Match(53);
            BattingCard sc = new BattingCard(152, ThemOrUs.Them);
            BowlingStats bs = test.GetOurBowlingStats();

            ChatItem a = new ChatItem();
            a.Comment = "This is a test 1234567890 ";
            a.Date = DateTime.Now;
            a.Name = "Me. Me me me";
            Chat.PostItem(a);

            var b =  Chat.GetAllChatSince(DateTime.Now.AddHours(-1));
            MatchReport test12 = new MatchReport(91, "");
            test12.Report = "Tes test teslkkajhdkjash askjdask aksjdhas ";
            test12.HasPhotos= true;
            test12.Password = "testpassword";
            test12.Save();
        }
    }
}
