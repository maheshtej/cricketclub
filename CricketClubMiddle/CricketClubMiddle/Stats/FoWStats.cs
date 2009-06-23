using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CricketClubDomain;
using CricketClubDAL;

namespace CricketClubMiddle.Stats
{
    public class FoWStats
    {
        
        public FoWStats(int MatchID, ThemOrUs who)
        {
            Who = who;
            DAO myDAO = new DAO();
            Data = (from a in myDAO.GetFoWData(MatchID, who) 
                   select new FoWStatsLine(a)).ToList();
        }


        public ThemOrUs Who
        {
            get;
            set;
        }
        public List<FoWStatsLine> Data
        {
            get;
            set;
        }

        public void Save()
        {
            DAO myDAO = new DAO();

            List<FoWDataLine> _data = (from a in Data select a._data).ToList();
            myDAO.UpdateFoWData(_data, Who);
        }


    }
}
