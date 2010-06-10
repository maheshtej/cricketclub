using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using CricketClubDomain;
using System.Globalization;

namespace CricketClubDAL
{
    public class DAO
    {
        private DB scorebook = new DB();

        #region Players

        public PlayerData GetPlayerData(int PlayerID)
        {
            string sql = "select * from Players where player_id = " + PlayerID.ToString();
            
            DataRow dr =  scorebook.ExecuteSQLAndReturnFirstRow(sql);
            PlayerData newPlayer = new PlayerData();
            newPlayer.ID = (int)dr["player_id"];
            newPlayer.EmailAddress = dr["email_address"].ToString();
            newPlayer.Name = dr["player_name"].ToString();
            newPlayer.FullName = dr["full_name"].ToString();
            newPlayer.BattingStyle = dr["batting_style"].ToString();
            newPlayer.BowlingStyle = dr["bowling_style"].ToString();
            newPlayer.FirstName = dr["first_name"].ToString();
            newPlayer.Surname = dr["last_name"].ToString();
            newPlayer.MiddleInitials = dr["middle_initials"].ToString();
            try
            {
                newPlayer.RingerOf = (int)dr["ringer_of"];
            }
            catch { }

            try
            {
                newPlayer.DateOfBirth = (DateTime)dr["dob"];
            }
            catch
            {
                newPlayer.DateOfBirth = new DateTime(1, 1, 1);
            }
            newPlayer.Education = dr["education"].ToString();
            newPlayer.Location = dr["location"].ToString();
            newPlayer.Height = dr["height"].ToString();
            newPlayer.NickName = dr["nickname"].ToString();
            try
            {
                newPlayer.IsActive = Convert.ToBoolean((int)dr["Active"]);
            }
            catch
            {
                newPlayer.IsActive = true;
            }

            return newPlayer;

        }

        public List<PlayerData> GetAllPlayers()
        {
            string sql = "select * from players";
            DataSet ds = scorebook.ExecuteSQLAndReturnAllRows(sql);
            List<PlayerData> players = new List<PlayerData>();
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                PlayerData newPlayer = new PlayerData();
                newPlayer.ID = (int)dr["player_id"];
                newPlayer.EmailAddress = dr["email_address"].ToString();
                newPlayer.Name = dr["player_name"].ToString();
                newPlayer.FullName = dr["full_name"].ToString();
                newPlayer.BattingStyle = dr["batting_style"].ToString();
                newPlayer.BowlingStyle = dr["bowling_style"].ToString();
                newPlayer.FirstName = dr["first_name"].ToString();
                newPlayer.Surname = dr["last_name"].ToString();
                newPlayer.MiddleInitials = dr["middle_initials"].ToString();
                try
                {
                    newPlayer.RingerOf = (int)dr["ringer_of"];
                }
                catch { }
                try
                {
                    newPlayer.DateOfBirth = (DateTime)dr["dob"];
                }
                catch
                {
                    newPlayer.DateOfBirth = new DateTime(1, 1, 1);
                }
                newPlayer.Education = dr["education"].ToString();
                newPlayer.Location = dr["location"].ToString();
                newPlayer.Height = dr["height"].ToString();
                newPlayer.NickName = dr["nickname"].ToString();
                try
                {
                    newPlayer.IsActive = Convert.ToBoolean((int)dr["Active"]);
                }
                catch
                {
                    newPlayer.IsActive = true;
                }
                players.Add(newPlayer);
            }
            return players;
        }

        public int CreateNewPlayer(string name)
        {
            int NewPlayerID = (int)scorebook.ExecuteSQLAndReturnSingleResult("select max(player_id) from players") + 1;
            int rowsAffected = scorebook.ExecuteInsertOrUpdate("insert into players(player_id, player_name) select " + NewPlayerID + 
                ", '" + name + "'");
            if (rowsAffected == 1)
            {
                return NewPlayerID;
            }
            else
            {
                return 0;
            }
        }

        public void UpdatePlayer(PlayerData playerData)
        {
            string sql = "update players set {0} = {1} where player_id = " + playerData.ID;
            int rowsAffected = scorebook.ExecuteInsertOrUpdate(string.Format(sql, new string[] { "player_name", "'" + playerData.Name + "'" }));
            rowsAffected = scorebook.ExecuteInsertOrUpdate(string.Format(sql, new string[] { "full_name", "'" + playerData.FullName + "'" }));
            rowsAffected = scorebook.ExecuteInsertOrUpdate(string.Format(sql, new string[] { "dob", "'" + playerData.DateOfBirth.ToString() + "'" }));
            rowsAffected = scorebook.ExecuteInsertOrUpdate(string.Format(sql, new string[] { "location", "'" + playerData.Location + "'" }));
            rowsAffected = scorebook.ExecuteInsertOrUpdate(string.Format(sql, new string[] { "height", "'" + playerData.Height + "'" }));
            rowsAffected = scorebook.ExecuteInsertOrUpdate(string.Format(sql, new string[] { "nickname", "'" + playerData.NickName + "'" }));
            rowsAffected = scorebook.ExecuteInsertOrUpdate(string.Format(sql, new string[] { "education", "'" + playerData.Education + "'" }));
            rowsAffected = scorebook.ExecuteInsertOrUpdate(string.Format(sql, new string[] { "batting_style", "'" + playerData.BattingStyle + "'" }));
            rowsAffected = scorebook.ExecuteInsertOrUpdate(string.Format(sql, new string[] { "bowling_style", "'" + playerData.BowlingStyle + "'" }));
            rowsAffected = scorebook.ExecuteInsertOrUpdate(string.Format(sql, new string[] { "email_address", "'" + playerData.EmailAddress + "'" }));
            rowsAffected = scorebook.ExecuteInsertOrUpdate(string.Format(sql, new string[] { "first_name", "'" + playerData.FirstName + "'" }));
            rowsAffected = scorebook.ExecuteInsertOrUpdate(string.Format(sql, new string[] { "last_name", "'" + playerData.Surname + "'" }));
            rowsAffected = scorebook.ExecuteInsertOrUpdate(string.Format(sql, new string[] { "middle_initials", "'" + playerData.MiddleInitials + "'" }));
            rowsAffected = scorebook.ExecuteInsertOrUpdate(string.Format(sql, new string[] { "active", Convert.ToInt16(playerData.IsActive).ToString() }));
            rowsAffected = scorebook.ExecuteInsertOrUpdate(string.Format(sql, new string[] { "ringer_of", playerData.RingerOf.ToString() }));
            
        }

        public List<BattingCardLineData> GetPlayerBattingStatsData(int PlayerID)
        {
            List<BattingCardLineData> data = new List<BattingCardLineData>();
            string sql = "select * from batting_scorecards a, matches b where a.match_id = b.match_id and player_id = " + PlayerID;
            
            DataSet ds = scorebook.ExecuteSQLAndReturnAllRows(sql);

            foreach (DataRow row in ds.Tables[0].Rows)
            {
                BattingCardLineData scData = new BattingCardLineData();
                scData.BattingAt = (int)row["batting at"];
                scData.BowlerName = row["bowler_name"].ToString();
                scData.FielderName = row["fielder_name"].ToString();
                scData.Fours = (int)row["4s"];
                scData.Sixes = (int)row["6s"];
                scData.ModeOfDismissal = (int)row["dismissal_id"];
                scData.PlayerID = (int)row["player_id"];
                scData.MatchID = (int)row["a.match_id"];
                scData.Score = (int)row["score"];
                scData.MatchTypeID = (int)row["comp_id"];
                scData.MatchDate = (DateTime)row["match_date"];
                scData.VenueID = (int)row["venue_id"];

                data.Add(scData);

            }

            return data;
        }

       
        public List<BattingCardLineData> GetPlayerFieldingStatsData(int PlayerID)
        {
            List<BattingCardLineData> data = new List<BattingCardLineData>();
            string sql = "select * from bowling_scorecards a, matches b where a.match_id = b.match_id and (fielder_id = " + PlayerID + " or bowler_id = " + PlayerID + ")";

            DataSet ds = scorebook.ExecuteSQLAndReturnAllRows(sql);

            foreach (DataRow row in ds.Tables[0].Rows)
            {
                BattingCardLineData scData = new BattingCardLineData();
                scData.BattingAt = (int)row["batting at"];
                scData.BowlerID = (int)row["bowler_id"];
                scData.FielderID = (int)row["fielder_id"];
                scData.ModeOfDismissal = (int)row["dismissal_id"];
                scData.PlayerName = row["player_name"].ToString();
                scData.MatchID = (int)row["a.match_id"];
                scData.Score = (int)row["score"];
                scData.MatchTypeID = (int)row["comp_id"];
                scData.MatchDate = (DateTime)row["match_date"];
                scData.VenueID = (int)row["venue_id"];
                data.Add(scData);

            }

            return data;
        }

        

        public List<BowlingStatsEntryData> GetPlayerBowlingStatsData(int PlayerID)
        {
            List<BowlingStatsEntryData> data = new List<BowlingStatsEntryData>();
            string sql = "select * from bowling_stats a, matches b where a.match_id = b.match_id and player_id = " + PlayerID;

            DataSet ds = scorebook.ExecuteSQLAndReturnAllRows(sql);

            foreach (DataRow row in ds.Tables[0].Rows)
            {
                BowlingStatsEntryData scData = new BowlingStatsEntryData();
                scData.Overs = decimal.Parse(row["overs"].ToString());
                scData.Maidens = (int)row["maidens"];
                scData.Runs = (int)row["runs"];
                scData.Wickets = (int)row["wickets"];
                scData.PlayerID = (int)row["player_id"];
                scData.MatchID = (int)row["a.match_id"];
                scData.MatchTypeID = (int)row["comp_id"];
                scData.MatchDate = (DateTime)row["match_date"];
                scData.VenueID = (int)row["venue_id"];
                

                data.Add(scData);

            }

            return data;
        }

        #endregion

        #region Teams

        public TeamData GetTeamData(int TeamID)
        {
            string sql = "select * from Teams where team_id = " + TeamID.ToString();

            DataRow dr = scorebook.ExecuteSQLAndReturnFirstRow(sql);
            TeamData data = new TeamData();
            data.ID = (int)dr["team_id"];
            data.Name = dr["team"].ToString();
            return data;
        }
        
        public int CreateNewTeam(string TeamName)
        {
            DataRow dr = scorebook.ExecuteSQLAndReturnFirstRow("select * from teams where team ='"+TeamName+"'");
            if (dr != null)
            {
                return (int)dr["team_id"];
            }
            else
            {
                int NewTeamID = (int)scorebook.ExecuteSQLAndReturnSingleResult("select max(team_id) from teams") + 1;
                int rowsAffected = scorebook.ExecuteInsertOrUpdate("insert into teams(team_id, team) select " + NewTeamID +
                    ", '" + TeamName + "'");
                if (rowsAffected == 1)
                {
                    return NewTeamID;
                }
                else
                {
                    return 0;
                }
            }
        }

        public void UpdateTeam(TeamData Data)
        {
            string sql = "update teams set {0} = {1} where team_id = " + Data.ID;
            int rowsAffected = scorebook.ExecuteInsertOrUpdate(string.Format(sql, new string[] { "team", "'" + Data.Name + "'" }));
        }

        public List<TeamData> GetAllTeamData()
        {
            List<TeamData> teams = new List<TeamData>();
            string sql = "select * from teams";
            DataSet ds = scorebook.ExecuteSQLAndReturnAllRows(sql);
            foreach (DataRow data in ds.Tables[0].Rows)
            {
                TeamData team = new TeamData();
                team.ID = (int)data["team_id"];
                team.Name = data["team"].ToString();
                teams.Add(team);
            }

            return teams;
        }

        #endregion

        #region Venues

        public VenueData GetVenueData(int VenueID)
        {
            string sql = "select * from Venues where venue_id = " + VenueID.ToString();

            VenueData venue = new VenueData();
            DataRow data = scorebook.ExecuteSQLAndReturnFirstRow(sql);
            venue.ID = (int)data["venue_id"];
            venue.Name = data["venue"].ToString();
            //TODO: Add map url
            venue.MapUrl = "";

            return venue;
        }
        
        public int CreateNewVenue(string VenueName)
        {
            DataRow dr = scorebook.ExecuteSQLAndReturnFirstRow("select * from venues where venue ='" + VenueName + "'");
            if (dr != null)
            {
                return (int)dr["venue_id"];
            }
            else
            {
                int NewVenueID = (int)scorebook.ExecuteSQLAndReturnSingleResult("select max(venue_id) from venues") + 1;
                int rowsAffected = scorebook.ExecuteInsertOrUpdate("insert into venues(venue_id, venue) select " + NewVenueID +
                    ", '" + VenueName + "'");
                if (rowsAffected == 1)
                {
                    return NewVenueID;
                }
                else
                {
                    return 0;
                }
            }
        }

        public void UpdateVenue(VenueData Data)
        {
            string sql = "update venues set {0} = {1} where venue_id = " + Data.ID;
            int rowsAffected = scorebook.ExecuteInsertOrUpdate(string.Format(sql, new string[] { "venue", "'" + Data.Name + "'" }));
        }

        public List<VenueData> GetAllVenueData()
        {
            List<VenueData> venues = new List<VenueData>();
            string sql = "select * from Venues";
            DataSet ds = scorebook.ExecuteSQLAndReturnAllRows(sql);
            foreach (DataRow data in ds.Tables[0].Rows)
            {
                VenueData venue = new VenueData();
                venue.ID = (int)data["venue_id"];
                venue.Name = data["venue"].ToString();
                //TODO: Add map url
                venue.MapUrl = "";
                venues.Add(venue);
            }

            return venues;
        }

        #endregion

        #region Matches

        public MatchData GetMatchData(int MatchID)
        {
            string sql = "select * from Matches where match_id = " + MatchID.ToString();

            MatchData match = new MatchData();
            DataRow dr = scorebook.ExecuteSQLAndReturnFirstRow(sql);

            match.ID = (int)dr["match_id"];
            match.MatchType = (int)dr["comp_id"];
            match.HomeOrAway = dr["Home_Away"].ToString();
            match.OppositionID = (int)dr["oppo_id"];
            match.Date = DateTime.Parse(dr["match_date"].ToString());
            match.VenueID = (int)dr["venue_id"];
            try
            {
                match.Overs = (int)dr["match_overs"];
            }
            catch
            {
                //
            }
            try
            {
                match.TheyDeclared = Convert.ToBoolean((int)dr["their_innings_was_declared"]);
            }
            catch
            {
                match.TheyDeclared = false;
            }
            try
            {
                match.WeDeclared = Convert.ToBoolean((int)dr["our_innings_was_declared"]);
            }
            catch
            {
                match.WeDeclared = false;
            }
            try
            {
                match.OurInningsLength = (double.Parse(dr["our_innings_length"].ToString()));
            }
            catch
            {
                match.OurInningsLength = 0.0;
            }
            try
            {
                match.TheirInningsLength = (double.Parse(dr["their_innings_length"].ToString()));
            }
            catch
            {
                match.TheirInningsLength = 0.0;
            } 


            match.Abandoned = Convert.ToBoolean((int)dr["abandoned"]);
            try
            {
                match.Batted = Convert.ToBoolean((int)dr["batted"]);
            }
            catch
            {
                match.Batted = false;
            }
            try
            {
                match.WonToss = Convert.ToBoolean((int)dr["won_toss"]);
            }
            catch
            {
                match.WonToss = false;
            }
            try
            {
                match.WasDeclarationGame = Convert.ToBoolean((int)dr["was_declaration"]);
            }
            catch
            {
                match.WasDeclarationGame = false;
            }
            try
            {
                match.CaptainID = ((int)dr["captain_id"]);
            }
            catch
            {
                match.CaptainID = 0;
            }
            try
            {
                match.WicketKeeperID = ((int)dr["wicketkeeper_id"]);
            }
            catch
            {
                match.WicketKeeperID = 0;
            }

            return match;

        }

        public int CreateNewMatch(int opponentID, DateTime matchDate, int venueID, int matchTypeID, string HomeAway)
        {
            int NewMatchID = (int)scorebook.ExecuteSQLAndReturnSingleResult("select max(match_id) from matches") + 1;
            int rowsAffected = scorebook.ExecuteInsertOrUpdate("insert into matches(match_id, match_date, oppo_id, comp_id, venue_id, home_away) select " 
                + NewMatchID +", '" 
                + matchDate.ToString() + "' , "
                + opponentID + ", "
                + matchTypeID + ", "
                + venueID + ", '"
                + HomeAway + "'"
                );
            if (rowsAffected == 1)
            {
                return NewMatchID;
            }
            else
            {
                return 0;
            }
        }

        public void UpdateMatch(MatchData Data)
        {
            string sql = "update matches set {0} = {1} where match_id = " + Data.ID;
            int rowsAffected = scorebook.ExecuteInsertOrUpdate(string.Format(sql, new string[] { "match_date", "'" + Data.Date + "'" }));
            rowsAffected = scorebook.ExecuteInsertOrUpdate(string.Format(sql, new string[] { "oppo_id", Data.OppositionID.ToString() }));
            rowsAffected = scorebook.ExecuteInsertOrUpdate(string.Format(sql, new string[] { "comp_id", Data.MatchType.ToString() }));
            rowsAffected = scorebook.ExecuteInsertOrUpdate(string.Format(sql, new string[] { "venue_id", Data.VenueID.ToString() }));
            rowsAffected = scorebook.ExecuteInsertOrUpdate(string.Format(sql, new string[] { "home_away", Data.HomeOrAway }));
            rowsAffected = scorebook.ExecuteInsertOrUpdate(string.Format(sql, new string[] { "won_toss", (Convert.ToInt16(Data.WonToss)).ToString() }));
            rowsAffected = scorebook.ExecuteInsertOrUpdate(string.Format(sql, new string[] { "batted", (Convert.ToInt16(Data.Batted)).ToString() }));
            rowsAffected = scorebook.ExecuteInsertOrUpdate(string.Format(sql, new string[] { "was_declaration", (Convert.ToInt16(Data.WasDeclarationGame)).ToString() }));
            rowsAffected = scorebook.ExecuteInsertOrUpdate(string.Format(sql, new string[] { "captain_id", Data.CaptainID.ToString() }));
            rowsAffected = scorebook.ExecuteInsertOrUpdate(string.Format(sql, new string[] { "wicketkeeper_id", Data.WicketKeeperID.ToString() }));
            rowsAffected = scorebook.ExecuteInsertOrUpdate(string.Format(sql, new string[] { "match_overs", Data.Overs.ToString() }));
            rowsAffected = scorebook.ExecuteInsertOrUpdate(string.Format(sql, new string[] { "their_innings_was_declared", (Convert.ToInt16(Data.TheyDeclared)).ToString() }));
            rowsAffected = scorebook.ExecuteInsertOrUpdate(string.Format(sql, new string[] { "our_innings_was_declared", (Convert.ToInt16(Data.WeDeclared)).ToString() }));
            rowsAffected = scorebook.ExecuteInsertOrUpdate(string.Format(sql, new string[] { "their_innings_length", Data.TheirInningsLength.ToString() }));
            rowsAffected = scorebook.ExecuteInsertOrUpdate(string.Format(sql, new string[] { "our_innings_length", Data.OurInningsLength.ToString() }));
            rowsAffected = scorebook.ExecuteInsertOrUpdate(string.Format(sql, new string[] { "abandoned", (Convert.ToInt16(Data.Abandoned)).ToString() }));
            

        }

        public int GetNextMatch(DateTime date)
        {
            string sql = "select * from matches where match_date >= #" + date.ToLongDateString() + "# order by match_date asc";
            DataRow dr = scorebook.ExecuteSQLAndReturnFirstRow(sql);
            try
            {
                return (int)dr["match_id"];
            }
            catch
            {
                return -1;
            }
        }

        public int GetPreviousMatch(DateTime date)
        {
            string sql = "select * from matches where match_date <= #" + date.ToUniversalTime().ToShortDateString() + "# order by match_date desc";
            DataRow dr = scorebook.ExecuteSQLAndReturnFirstRow(sql);
            try
            {
                return (int)dr["match_id"];
            }
            catch
            {
                return -1;
            }
        }

        public List<MatchData> GetAllMatches()
        {
            string sql = "select * from matches";
            DataSet ds = scorebook.ExecuteSQLAndReturnAllRows(sql);
            List<MatchData> matches = new List<MatchData>();
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                MatchData match = new MatchData();
                match.ID = (int)dr["match_id"];
                match.MatchType = (int)dr["comp_id"];
                match.HomeOrAway = dr["Home_Away"].ToString();
                match.OppositionID = (int)dr["oppo_id"];
                match.Date = DateTime.Parse(dr["match_date"].ToString());
                match.VenueID = (int)dr["venue_id"];
                try
                {
                    match.Overs = (int)dr["match_overs"];
                }
                catch
                {
                    //
                }
                try
                {
                    match.TheyDeclared = Convert.ToBoolean((int)dr["their_innings_was_declared"]);
                }
                catch
                {
                    match.TheyDeclared = false;
                }
                try
                {
                    match.WeDeclared = Convert.ToBoolean((int)dr["our_innings_was_declared"]);
                }
                catch
                {
                    match.WeDeclared = false;
                }
                try
                {
                    match.OurInningsLength = (double.Parse(dr["our_innings_length"].ToString()));
                }
                catch
                {
                    match.OurInningsLength = 0.0;
                }
                try
                {
                    match.TheirInningsLength = (double.Parse(dr["their_innings_length"].ToString()));
                }
                catch
                {
                    match.TheirInningsLength = 0.0;
                }


                match.Abandoned = Convert.ToBoolean((int)dr["abandoned"]);
                try
                {
                    match.Batted = Convert.ToBoolean((int)dr["batted"]);
                }
                catch
                {
                    match.Batted = false;
                }
                try
                {
                    match.WonToss = Convert.ToBoolean((int)dr["won_toss"]);
                }
                catch
                {
                    match.WonToss = false;
                }
                try
                {
                    match.WasDeclarationGame = Convert.ToBoolean((int)dr["was_declaration"]);
                }
                catch
                {
                    match.WasDeclarationGame = false;
                }
                try
                {
                    match.CaptainID = ((int)dr["captain_id"]);
                }
                catch
                {
                    match.CaptainID = 0;
                }
                try
                {
                    match.WicketKeeperID = ((int)dr["wicketkeeper_id"]);
                }
                catch
                {
                    match.WicketKeeperID = 0;
                }
                matches.Add(match);
            }
            return matches;
         
        }



        #endregion

        #region Scorecards

        public List<BattingCardLineData> GetBattingCard(int MatchID, ThemOrUs us)
        {
            string tableName = "";
            if (us == ThemOrUs.Us) {
                tableName = "batting_scorecards";
            } else {
                tableName = "bowling_scorecards";
            }
            List<BattingCardLineData> data = new List<BattingCardLineData>();
            string sql = "select * from " + tableName + " where match_id = " + MatchID;
            DataSet ds = scorebook.ExecuteSQLAndReturnAllRows(sql);
            foreach (DataRow row in ds.Tables[0].Rows)
            {
                BattingCardLineData scData = new BattingCardLineData();
                scData.BattingAt = ((int)row["batting at"])+1;
                scData.MatchID = (int)row["match_id"];
                scData.Score = (int)row["score"];
                scData.ModeOfDismissal = (int)row["dismissal_id"];
                if (us == ThemOrUs.Them)
                {
                    scData.BowlerID = (int)row["bowler_id"];
                    scData.FielderID = (int)row["fielder_id"];
                    scData.PlayerName = row["player_name"].ToString();
                }
                if (us == ThemOrUs.Us)
                {
                    scData.BowlerName = row["bowler_name"].ToString();
                    scData.FielderName = row["fielder_name"].ToString();
                    scData.Fours = (int)row["4s"];
                    scData.Sixes = (int)row["6s"];
                    scData.PlayerID = (int)row["player_id"];
                
                }


                data.Add(scData);

            }
            return data;
        }

        public void UpdateScoreCard(List<BattingCardLineData> BattingData, int TotalExtras, BattingOrBowling BattingOrBowling)
        {
            if (BattingData.Count > 0 && TotalExtras != 0)
            {
                string Table = "batting_scorecards";
                if (BattingOrBowling == BattingOrBowling.Bowling)
                {
                    Table = "bowling_scorecards";
                }
                string sql = "delete from " + Table + " where match_id = " + BattingData[0].MatchID;
                int a = scorebook.ExecuteInsertOrUpdate(sql);
                foreach (BattingCardLineData _row in BattingData)
                {
                    if (BattingOrBowling == BattingOrBowling.Bowling)
                    {
                        sql = "insert into bowling_scorecards(player_name, dismissal_id, score, [batting at], match_id, bowler_id, fielder_id) select '" + _row.PlayerName + "', " + (int)_row.ModeOfDismissal + ", " + _row.Score + ", " + (_row.BattingAt-1).ToString() + ", " + _row.MatchID + " , " + _row.BowlerID + ", " + _row.FielderID;
                    }
                    else
                    {
                        sql = "insert into batting_scorecards(player_id, dismissal_id, score, [batting at], match_id, bowler_name, fielder_name, 4s, 6s) select " + _row.PlayerID + ", " + (int)_row.ModeOfDismissal + ", " + _row.Score + ", " + (_row.BattingAt-1).ToString() + ", " + _row.MatchID + " , '" + _row.BowlerName + "', '" + _row.FielderName + "'," + _row.Fours + ", " + _row.Sixes;
                    }

                    int temp = scorebook.ExecuteInsertOrUpdate(sql);
                }

                //Extras
                if (BattingOrBowling == BattingOrBowling.Batting)
                {
                    sql = "insert into batting_scorecards(player_id, dismissal_id, score, [batting at], match_id, bowler_name, 4s, 6s) select -1, -1, " + TotalExtras + ", 11, " + BattingData[0].MatchID + " , '', 0, 0";
                }
                else
                {
                    sql = "insert into bowling_scorecards(player_name, dismissal_id, score, [batting at], match_id, bowler_id) select '(Frank) Extras', -1, " + TotalExtras + ", 11, " + BattingData[0].MatchID + " , 0";
                }
                int extras = scorebook.ExecuteInsertOrUpdate(sql);
            }
            else
            {
                throw new InvalidConstraintException("No Extras or No Batting Data Submited");
            }
            


        }

        public List<BowlingStatsEntryData> GetBowlingStats(int MatchID, ThemOrUs who)
        {
            string tableName = "";
            if (who == ThemOrUs.Us)
            {
                tableName = "bowling_stats";
            }
            else
            {
                tableName = "oppo_bowling_stats";
            }
            List<BowlingStatsEntryData> data = new List<BowlingStatsEntryData>();
            string sql = "select * from " + tableName + " where match_id = " + MatchID;
            DataSet ds = scorebook.ExecuteSQLAndReturnAllRows(sql);
            foreach (DataRow row in ds.Tables[0].Rows)
            {
                BowlingStatsEntryData scData = new BowlingStatsEntryData();
                scData.Overs = decimal.Parse(row["overs"].ToString());
                scData.Maidens = (int)row["maidens"];
                scData.Runs = (int)row["runs"];
                scData.Wickets = (int)row["wickets"];
                if (who == ThemOrUs.Us)
                {
                    scData.PlayerID = (int)row["player_id"];
                }
                else
                {
                    scData.PlayerName = row["player_name"].ToString();
                }
                scData.MatchID = (int)row["match_id"];
               
                data.Add(scData);
            }
            return data;
        }

        public void UpdateBowlingStats(List<BowlingStatsEntryData> Data, ThemOrUs Who)
        {
            if (Data.Count > 0)
            {
                string Table = "bowling_stats";
                if (Who == ThemOrUs.Them)
                {
                    Table = "oppo_bowling_stats";
                }

                string sql = "delete from " + Table + " where match_id = " + Data[0].MatchID;
                int temp = scorebook.ExecuteInsertOrUpdate(sql);

                foreach (BowlingStatsEntryData line in Data)
                {
                    if (Who == ThemOrUs.Us)
                    {
                        sql = "insert into " + Table + "(match_id, player_id, overs, maidens, runs, wickets) select " + line.MatchID + ", " + line.PlayerID + ", " + line.Overs + ", " + line.Maidens + ", " + line.Runs + ", " + line.Wickets;
                    }
                    else
                    {
                        sql = "insert into " + Table + "(match_id, player_name, overs, maidens, runs, wickets) select " + line.MatchID + ", '" + line.PlayerName + "', " + line.Overs + ", " + line.Maidens + ", " + line.Runs + ", " + line.Wickets;
                    }
                    temp = scorebook.ExecuteInsertOrUpdate(sql);
                }

            }
            else
            {
                throw new InvalidOperationException("No data found in Bowling Stats collection");
            }
        }

        
        public List<FoWDataLine> GetFoWData(int MatchID, ThemOrUs who)
        {
            string Table  = "fow";
            if (who == ThemOrUs.Them) {
                Table = "oppo_fow";
            }

            string sql = "select * from " + Table + " where match_id = " + MatchID;
            DataSet ds = scorebook.ExecuteSQLAndReturnAllRows(sql);

            List<FoWDataLine> data = new List<FoWDataLine>();

            foreach (DataRow row in ds.Tables[0].Rows)
            {
                FoWDataLine line = new FoWDataLine();
                line.MatchID = (int)row["match_id"];
                line.NotOutBatsman = (int)row["no_bat"];
                line.NotOutBatsmanScore = (int)row["no_score"];
                line.OutgoingBatsman = (int)row["outgoing_bat"];
                line.OutgoingBatsmanScore = (int)row["outgoing_score"];
                line.OverNumber = (int)row["over_no"];
                line.Partnership = (int)row["partnership"];
                line.Score = (int)row["score"];
                line.Wicket = (int)row["wicket"];
                line.who = who;
                data.Add(line);
            }
            return data;
        }

        public void UpdateFoWData(List<FoWDataLine> Data, ThemOrUs who)
        {
            if (Data.Count > 0)
            {
                string Table = "fow";
                if (who == ThemOrUs.Them)
                {
                    Table = "oppo_fow";
                }

                string sql = "delete from " + Table + " where match_id = " + Data[0].MatchID;
                int temp = scorebook.ExecuteInsertOrUpdate(sql);

                foreach (FoWDataLine line in Data)
                {
                    sql = "insert into " + Table + "(match_id, wicket, score, partnership, over_no, outgoing_score, outgoing_bat, no_score, no_bat) select " +
                        line.MatchID + ", " +
                        line.Wicket + ", " +
                        line.Score + ", " +
                        line.Partnership + ", " +
                        line.OverNumber + ", " +
                        line.OutgoingBatsmanScore + ", " +
                        line.OutgoingBatsman + ", " +
                        line.NotOutBatsmanScore + ", " +
                        line.NotOutBatsman;
                    temp = scorebook.ExecuteInsertOrUpdate(sql);
                }
            }
            else
            {
                throw new InvalidOperationException("No Data found in Fow Collection");
            }

        }

        public ExtrasData GetExtras(int MatchID, ThemOrUs Who)
        {
            string Table  = "extras";
            if (Who == ThemOrUs.Them) {
                Table = "oppo_extras";
            }
            string sql = "select * from " + Table + " where match_id = " + MatchID;
            DataRow data = scorebook.ExecuteSQLAndReturnFirstRow(sql);

            ExtrasData ed = new ExtrasData();
            ed.MatchID = MatchID;
            if (data != null)
            {
                
                ed.Byes = (int)data["byes"];
                ed.LegByes = (int)data["leg_byes"];
                ed.NoBalls = (int)data["no_balls"];
                ed.Penalty = (int)data["penalty"];
                ed.Wides = (int)data["wides"];
            }
            return ed;
        }

        public void UpdateExtras(ExtrasData Data, ThemOrUs Who)
        {
            string Table = "extras";
            if (Who == ThemOrUs.Them)
            {
                Table = "oppo_extras";
            }

            string sql = "delete from " + Table + " where match_id = " + Data.MatchID;
            int temp = scorebook.ExecuteInsertOrUpdate(sql);

            sql = "insert into " + Table + "(match_id, wides, no_balls, penalty, leg_byes, byes) select " + Data.MatchID + ", " + Data.Wides + ", " + Data.NoBalls + ", " + Data.Penalty + ", " + Data.LegByes + ", " + Data.Byes;
            temp = scorebook.ExecuteInsertOrUpdate(sql);
        }
        
        public string GetDismissalText(int dismissalID)
        {
            string sql = "select dismissal from how_out where dismissal_id = " + dismissalID;
            return scorebook.ExecuteSQLAndReturnSingleResult(sql).ToString();
        }



        #endregion

        #region News

        public void SaveNewsStory(NewsData data)
        {
            string story = data.Story;
            List<string> storyChunks = new List<string>();
            //Break into bits of 250 - note, not 255 as the replacement of ' for "''" might add extra chars
            while (story.Length > 250)
            {
                storyChunks.Add(story.Substring(0, 250));
                story = story.Substring(250);
            }
            storyChunks.Add(story);

            string sql = "select max(news_id) as id from news";
            int NewsID = (int)scorebook.ExecuteSQLAndReturnSingleResult(sql)+1;

            sql = "insert into News(news_id, headline, short_headline, teaser, item_date) select " 
                + NewsID + ", '" 
                + SafeForSQL(data.Headline) + "', '" 
                + SafeForSQL(data.ShortHeadline) + "', '" 
                + SafeForSQL(data.Teaser) + "', #" 
                + data.Date.ToString("dd MMMM yyyy HH:mm:ss") + "#";

            int temp = scorebook.ExecuteInsertOrUpdate(sql);

            int counter = 0;
            foreach (string chunk in storyChunks)
            {
                counter ++;
                if (counter <= 20 && counter > 1)
                {
                    sql = "update news set story"+counter.ToString()+ "='"+SafeForSQL(chunk)+"' where news_id = " + NewsID;
                    temp = scorebook.ExecuteInsertOrUpdate(sql);
                }
                if (counter == 1)
                {
                    //special case - first field is just "story", not story1
                    sql = "update news set story='" + SafeForSQL(chunk) + "' where news_id = " + NewsID;
                    temp = scorebook.ExecuteInsertOrUpdate(sql);
                }
            }

            
        }

        public List<NewsData> GetTopXStories(int x)
        {
            List<NewsData> data = new List<NewsData>();
            string sql = "select top " + x + " * from News order by item_date desc";
            DataSet ds = scorebook.ExecuteSQLAndReturnAllRows(sql);
            foreach (DataRow row in ds.Tables[0].Rows)
            {
                NewsData item = new NewsData();
                item.Date = (DateTime)row["item_date"];
                item.Headline = row["headline"].ToString();
                item.ShortHeadline = row["short_headline"].ToString();
                item.Teaser = row["teaser"].ToString();
                item.Story = row["story"].ToString() 
                    + row["story2"].ToString()
                    + row["story3"].ToString()
                    + row["story4"].ToString()
                    + row["story5"].ToString()
                    + row["story6"].ToString()
                    + row["story7"].ToString()
                    + row["story8"].ToString()
                    + row["story9"].ToString()
                    + row["story10"].ToString()
                    + row["story11"].ToString()
                    + row["story12"].ToString()
                    + row["story13"].ToString()
                    + row["story14"].ToString()
                    + row["story15"].ToString()
                    + row["story16"].ToString()
                    + row["story17"].ToString()
                    + row["story18"].ToString()
                    + row["story19"].ToString()
                    + row["story20"].ToString();
                data.Add(item);
            }

            return data;
        }

        #endregion

        #region Chat

        public void SubmitChatComment(ChatData data)
        {
            string comment = data.Comment;
            List<string> commentChunks = new List<string>();
            //Break into bits of 250 - note, not 255 as the replacement of ' for "''" might add extra chars
            while (comment.Length > 250)
            {
                commentChunks.Add(comment.Substring(0, 250));
                comment = comment.Substring(250);
            }
            commentChunks.Add(comment);

            string sql = "insert into Chat(annon_user_name, image_url, post_time) select '"
                + SafeForSQL(data.Name) + "', '"
                + SafeForSQL(data.ImageUrl) + "', #"
                + data.Date.ToString("U") + "#";

            int temp = scorebook.ExecuteInsertOrUpdate(sql);

            sql = "select max(ID) as chat_id from chat where annon_user_name = '"+ SafeForSQL(data.Name) + "' and post_time=#"+data.Date.ToString("U")+"#";
            int ChatID;
            try
            {
                ChatID = (int)scorebook.ExecuteSQLAndReturnSingleResult(sql);
            }
            catch (NullReferenceException e)
            {
                ChatID = 0;
            }
            int counter = 0;
            foreach (string chunk in commentChunks)
            {
                counter++;
                if (counter <= 10 && counter > 0)
                {
                    sql = "update chat set comment" + counter.ToString() + "='" + SafeForSQL(chunk) + "' where ID = " + ChatID;
                    temp = scorebook.ExecuteInsertOrUpdate(sql);
                }
                
            }
        }

        public List<ChatData> GetChatBetween(DateTime startDate, DateTime endDate)
        {
            string sql = "select * from chat where post_time between #" + startDate.ToString(CultureInfo.CreateSpecificCulture("en-US")) + "# and #" + endDate.ToString(CultureInfo.CreateSpecificCulture("en-US")) + "#";
            DataSet ds = scorebook.ExecuteSQLAndReturnAllRows(sql);
            List<ChatData> data = new List<ChatData>();
            foreach (DataRow row in ds.Tables[0].Rows)
            {
                ChatData item = new ChatData();
                item.Date = (DateTime)row["post_time"];
                item.Name = row["annon_user_name"].ToString();
                item.ImageUrl = row["image_url"].ToString();
                item.ID = int.Parse(row["ID"].ToString());
                item.Comment = row["comment1"].ToString() +
                    row["comment2"].ToString() +
                    row["comment3"].ToString() +
                    row["comment4"].ToString() +
                    row["comment5"].ToString() +
                    row["comment6"].ToString() +
                    row["comment7"].ToString() +
                    row["comment8"].ToString() +
                    row["comment9"].ToString() +
                    row["comment10"].ToString();
                data.Add(item);
            
            }
            return data;
        }

        public List<ChatData> GetChatAfter(int CommentID)
        {
            string sql = "select * from chat where ID > " + CommentID;
            DataSet ds = scorebook.ExecuteSQLAndReturnAllRows(sql);
            List<ChatData> data = new List<ChatData>();
            foreach (DataRow row in ds.Tables[0].Rows)
            {
                ChatData item = new ChatData();
                item.Date = (DateTime)row["post_time"];
                item.Name = row["annon_user_name"].ToString();
                item.ImageUrl = row["image_url"].ToString();
                item.ID = int.Parse(row["ID"].ToString());
                item.Comment = row["comment1"].ToString() +
                    row["comment2"].ToString() +
                    row["comment3"].ToString() +
                    row["comment4"].ToString() +
                    row["comment5"].ToString() +
                    row["comment6"].ToString() +
                    row["comment7"].ToString() +
                    row["comment8"].ToString() +
                    row["comment9"].ToString() +
                    row["comment10"].ToString();
                data.Add(item);

            }
            return data;
        }

        public MatchReportData GetMatchReportData(int MatchID)
        {
            string sql = "select * from Match_Reports where match_id = " + MatchID.ToString();

            MatchReportData match = new MatchReportData();
            DataRow dr = scorebook.ExecuteSQLAndReturnFirstRow(sql);

            match.MatchID = MatchID;
            try
            {
                match.ReportFilename = dr["filename"].ToString();
                match.Password = dr["password"].ToString();
            }
            catch
            {
                //
            }
            try
            {
                match.HasPhotos = Convert.ToBoolean((int)dr["photos"]);
            }
            catch
            {
                match.HasPhotos = false;
            }
            
            return match;

        }

        public void SaveMatchReport(MatchReportData data)
        {
            string sql = "delete from match_reports where match_id = "+ data.MatchID;
            int rowsAffected = scorebook.ExecuteInsertOrUpdate(sql);
            sql = "insert into match_reports(match_id, [filename], [password], [photos]) select " + data.MatchID + ", '" + data.ReportFilename + "', '" + data.Password + "', " + Convert.ToInt16(data.HasPhotos).ToString();
            rowsAffected = scorebook.ExecuteInsertOrUpdate(sql);
        
        }

        #endregion

        #region Accounts

        public List<AccountEntryData> GetAllAccountData()
        {
            List<AccountEntryData> accounts = new List<AccountEntryData>();
            string sql = "select * from accounts";
            DataSet ds = scorebook.ExecuteSQLAndReturnAllRows(sql);
            foreach (DataRow data in ds.Tables[0].Rows)
            {
                AccountEntryData entry = new AccountEntryData();
                entry.ID = (int)data["id"];
                entry.Amount = (double)data["amount"];
                entry.CreditOrDebit = (int)data["debit_credit"];
                try
                {
                    entry.Date = (DateTime)data["transaction_time"];
                }
                catch
                {
                    entry.Date = new DateTime(1970, 1, 1);
                }
                entry.Description = data["description"].ToString();
                entry.MatchID = (int)data["match_id"];
                entry.PlayerID = (int)data["player_id"];
                entry.Status = (int)data["status"];
                entry.Type = (int)data["payment_type"];

                accounts.Add(entry);
            }

            return accounts;
        }

        public void UpdateAccountEntry(AccountEntryData Data)
        {
            string sql = "update accounts set {0} = {1} where id = " + Data.ID;
            int rowsAffected = scorebook.ExecuteInsertOrUpdate(string.Format(sql, new string[] { "amount", Data.Amount.ToString() }));
             rowsAffected = scorebook.ExecuteInsertOrUpdate(string.Format(sql, new string[] { "debit_credit", "'" + (int)Data.CreditOrDebit + "'" }));
             rowsAffected = scorebook.ExecuteInsertOrUpdate(string.Format(sql, new string[] { "transaction_time", "'" + Data.Date + "'" }));
             rowsAffected = scorebook.ExecuteInsertOrUpdate(string.Format(sql, new string[] { "description", "'" + Data.Description + "'" }));
             rowsAffected = scorebook.ExecuteInsertOrUpdate(string.Format(sql, new string[] { "match_id",  Data.MatchID.ToString()  }));
             rowsAffected = scorebook.ExecuteInsertOrUpdate(string.Format(sql, new string[] { "player_id",  Data.PlayerID.ToString()  }));
             rowsAffected = scorebook.ExecuteInsertOrUpdate(string.Format(sql, new string[] { "status", (int)Data.Status + "" }));
             rowsAffected = scorebook.ExecuteInsertOrUpdate(string.Format(sql, new string[] { "payment_type", (int)Data.Type + "" }));



        }

        public int CreateNewAccountEntry(int PlayerID, string Description, double Amount, int CreditDebit, int Type, int MatchID, int Status, DateTime TransactionDate)
        {
            int rowsAffected = scorebook.ExecuteInsertOrUpdate("insert into accounts(player_id, description, amount, debit_credit, payment_type, match_id, status, transaction_time) select "
                + PlayerID + ", '"
                + Description + "' , "
                + Amount + ", "
                + CreditDebit + ", "
                + Type + ", "
                + MatchID + ", "
                + Status + ", '"
                + TransactionDate.ToLongDateString() + "'"
                );
            if (rowsAffected == 1)
            {
                int NewAccEntryID = (int)scorebook.ExecuteSQLAndReturnSingleResult("select max([id]) from accounts where player_id = " + PlayerID);
                return NewAccEntryID;
            }
            else
            {
                return 0;
            }
        }
        

        #endregion

        #region Users

        public List<UserData> GetAllUsers()
        {
            string sql = "select * from users";
            DataSet ds = scorebook.ExecuteSQLAndReturnAllRows(sql);
            List<UserData> users = new List<UserData>();
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                UserData newUser = new UserData();
                newUser.ID = (int)dr["user_id"];
                newUser.Name = dr["username"].ToString();
                newUser.EmailAddress = dr["email_address"].ToString();
                newUser.Password = dr["password"].ToString();
                newUser.DisplayName = dr["display_name"].ToString();
                newUser.Permissions = (int)dr["permissions"];

                users.Add(newUser);
            }
            return users;
        }

        public int CreateNewUser(string name, string emailaddress, string password, string displayname)
        {
            int NewUserID = 1;
            try
            {
                NewUserID = (int)scorebook.ExecuteSQLAndReturnSingleResult("select max(user_id) from users") + 1;
            }
            catch { }
            int rowsAffected = scorebook.ExecuteInsertOrUpdate("insert into users([user_id], [username], [password], [email_address], [display_name]) select " + NewUserID + ",'" + name + "', '" + password + "', '" + emailaddress + "', '" + displayname + "'");
            if (rowsAffected == 1)
            {
                return NewUserID;
            }
            else
            {
                return 0;
            }
        }

        public void UpdateUser(UserData userData)
        {
            string sql = "update [users] set [{0}] = {1} where user_id = " + userData.ID;
            int rowsAffected = scorebook.ExecuteInsertOrUpdate(string.Format(sql, new string[] { "username", "'" + userData.Name + "'" }));
            rowsAffected = scorebook.ExecuteInsertOrUpdate(string.Format(sql, new string[] { "password", "'" + userData.Password + "'" }));
            rowsAffected = scorebook.ExecuteInsertOrUpdate(string.Format(sql, new string[] { "email_address", "'" + userData.EmailAddress + "'" }));
            rowsAffected = scorebook.ExecuteInsertOrUpdate(string.Format(sql, new string[] { "display_name", "'" + userData.DisplayName + "'" }));
            rowsAffected = scorebook.ExecuteInsertOrUpdate(string.Format(sql, new string[] { "permissions",  userData.Permissions + "" }));

        }

        #endregion

        #region Photos

        public List<PhotoData> GetAllPhotos()
        {
            string sql = "select * from Match_Photos";
            DataSet ds = scorebook.ExecuteSQLAndReturnAllRows(sql);
            List<PhotoData> photos = new List<PhotoData>();
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                PhotoData newPhoto = new PhotoData();
                newPhoto.ID = (int)dr["ImageID"];
                newPhoto.AuthorID = (int)dr["Author"];
                newPhoto.FileName = dr["ImageName"].ToString();
                newPhoto.Title = dr["ImageTitle"].ToString();
                try
                {
                    newPhoto.UploadDate = (DateTime)dr["dob"];
                }
                catch
                {
                    newPhoto.UploadDate = new DateTime(1, 1, 1);
                }
                newPhoto.MatchID = (int)dr["Match_ID"];
                photos.Add(newPhoto);
            }
            return photos;
        }

        public int AddOrUpdatePhoto(PhotoData photo)
        {
            if (photo.ID != 0)
            {
                string sql = "delete from [Match_Photos] where Image_ID = " + photo.ID.ToString();
                int tmp = scorebook.ExecuteInsertOrUpdate(sql);
            }
            int NewPhotoID = 1;
            try
            {
                NewPhotoID = (int)scorebook.ExecuteSQLAndReturnSingleResult("select max([ImageID]) as [ID] from [Match_Photos]") + 1;
            }
            catch (Exception e)
            {
                //
            }
            int rowsAffected = scorebook.ExecuteInsertOrUpdate("insert into [Match_Photos](imageID, ImageNAme, ImageTitle, Match_ID, [author], uploadDate) select " + NewPhotoID +
                ", '" + photo.FileName + "', '" + photo.Title + "', " + photo.MatchID + "," + photo.AuthorID + ", #" + photo.UploadDate.ToString() + "#");
            if (rowsAffected == 1)
            {
                return NewPhotoID;
            }
            else
            {
                return 0;
            }

        }

        public List<PhotoCommentData> GetAllPhotoComments()
        {
            string sql = "select * from Match_Image_Comments";
            DataSet ds = scorebook.ExecuteSQLAndReturnAllRows(sql);
            List<PhotoCommentData> comments = new List<PhotoCommentData>();
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                PhotoCommentData newComment = new PhotoCommentData();
                newComment.ID = (int)dr["CommentID"];
                newComment.AuthorID = (int)dr["UserID"];
                newComment.PhotoID = (int)dr["ImageID"];
                try
                {
                    newComment.CommentTime = (DateTime)dr["CommentTime"];
                }
                catch
                {
                    newComment.CommentTime = new DateTime(1, 1, 1);
                }

                newComment.Comment = dr["Comment1"].ToString() +
                    dr["Comment2"].ToString() +
                    dr["Comment3"].ToString() +
                    dr["Comment4"].ToString() +
                    dr["Comment5"].ToString();
                comments.Add(newComment);
            }
            return comments;
        }

        public int SubmitPhotoComment(PhotoCommentData data)
        {
            string comment = data.Comment;
            List<string> commentChunks = new List<string>();
            //Break into bits of 250 - note, not 255 as the replacement of ' for "''" might add extra chars
            while (comment.Length > 250)
            {
                commentChunks.Add(comment.Substring(0, 250));
                comment = comment.Substring(250);
            }
            commentChunks.Add(comment);

            string sql = "insert into Match_Image_Comments(ImageID, UserID, CommentTime) select "
                + data.PhotoID + ", "
                + data.AuthorID + ", #"
                + data.CommentTime.ToString("U") + "#";

            int temp = scorebook.ExecuteInsertOrUpdate(sql);

            sql = "select max(CommentID) as comment_id from Match_Image_Comments where UserID = " + data.AuthorID + " and CommentTime=#" + data.CommentTime.ToString("U") + "#";
            int ChatID;
            try
            {
                ChatID = (int)scorebook.ExecuteSQLAndReturnSingleResult(sql);
            }
            catch (NullReferenceException e)
            {
                ChatID = 0;
            }
            int counter = 0;
            foreach (string chunk in commentChunks)
            {
                counter++;
                if (counter <= 5 && counter > 0)
                {
                    sql = "update Match_Image_Comments set comment" + counter.ToString() + "='" + SafeForSQL(chunk) + "' where CommentID = " + ChatID;
                    temp = scorebook.ExecuteInsertOrUpdate(sql);
                }

            }
            return ChatID;
        }

        

        #endregion

        #region Utility

        public string GetSetting(string settingName)
        {
            string sql = "select [value] from Settings where [key] = '" + settingName + "'";
            try
            {
                return scorebook.ExecuteSQLAndReturnSingleResult(sql).ToString();
            }
            catch
            {
                return null;
            }
        }

        public void SetSetting(string settingName, string value, string description)
        {
            string sql = "delete from Settings where [key] = '" + settingName + "'";
            scorebook.ExecuteInsertOrUpdate(sql);
            sql = "insert into Settings([key],[value], description) select '" + settingName + "','" + value + "','"+SafeForSQL(description)+"'";
            scorebook.ExecuteInsertOrUpdate(sql);
        }

        public List<SettingData> GetAllSettings()
        {
            string sql = "select * from Settings";
            DataSet data = scorebook.ExecuteSQLAndReturnAllRows(sql);
            List<SettingData> settings = new List<SettingData>();

            foreach (DataRow row in data.Tables[0].Rows)
            {
                SettingData setting = new SettingData();
                setting.Name = row["key"].ToString();
                setting.Value = row["value"].ToString();
                setting.Description = row["description"].ToString();
                settings.Add(setting);
            }
            return settings;
        }

        #endregion

        #region Logging

        public void LogMessage(string message, string stack, string level, DateTime when)
        {
            string sql = "insert into log(Message, Stack, Severity, MessageTime) select '"+message+"','"+stack+"','"+level+"',#"+when.ToString("U")+"#";
            scorebook.ExecuteInsertOrUpdate(sql);
        }
        
        #endregion

        private static string SafeForSQL(string s)
        {
            if (!string.IsNullOrEmpty(s))
            {
                return s.Replace("'", "''");
            }
            else
            {
                return " ";
            }
        }





        
    }
}
