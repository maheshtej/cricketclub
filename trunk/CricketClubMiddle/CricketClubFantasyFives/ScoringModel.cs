using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CricketClubFantasyFives
{
    public class ScoringModel
    {
        public static ScoringModel GetCurrentScoringModel() 
        {
            ScoringModel _model = new ScoringModel();
            //DAO fetch data
            return _model;
        }

        public int PointsPerRun
        {
            get;
            private set;
        }

        public int PointsPerFour
        {
            get;
            private set;
        }

        public int PointsPerSix
        {
            get;
            private set;
        }

        public int PointsPer50
        {
            get;
            private set;
        }

        public int PointsPer100
        {
            get;
            private set;
        }

        public int PointsPerCatch
        {
            get;
            private set;
        }

        public int PointsPerWicket
        {
            get;
            private set;
        }

        public int PointsPerFivefer
        {
            get;
            private set;
        }

        public int PointsPerThreefer
        {
            get;
            private set;
        }

        public int PointsLostPerRunConceeded
        {
            get;
            private set;
        }



    }
}
