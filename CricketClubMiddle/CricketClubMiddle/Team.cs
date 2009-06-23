using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using CricketClubDAL;
using CricketClubDomain;

namespace CricketClubMiddle
{
    public class Team
    {
        private InternalCache teamCache = InternalCache.GetInstance();
        private DAO myDAO = new DAO();
        private TeamData _teamData;

        public Team(int TeamID)
        {
            if (teamCache.Get("team" + TeamID) == null)
            {
                _teamData = myDAO.GetTeamData(TeamID);
                teamCache.Insert("team" + TeamID, _teamData, new TimeSpan(24, 0, 0));
            }
            else
            {
                _teamData = (TeamData)teamCache.Get("team" + TeamID);
            }
        }

        public static Team CreateNewTeam(string TeamName)
        {
            DAO myDAO = new DAO();
            int newTeamid = myDAO.CreateNewTeam(TeamName);
            return new Team(newTeamid);
        }

        public string Name
        {
            get
            {
                return _teamData.Name;
            }
            set
            {
                _teamData.Name = value;
            }
        }

        public int ID
        {
            get
            {
                return _teamData.ID;
            }
        }

        public void Save()
        {
            myDAO.UpdateTeam(_teamData);
        }


        public static List<Team> GetAll()
        {
            List<TeamData> data = new DAO().GetAllTeamData();
            List<Team> teams = new List<Team>();
            foreach (TeamData item in data)
            {
                teams.Add(new Team(item));

            }
            return teams;
        }

        public static Team GetByName(string Name)
        {
            Team team = (from a in Team.GetAll() where a.Name == Name select a).FirstOrDefault();
            return team;
        }

        private Team(TeamData data)
        {
            _teamData = data;
        }

    }
}
