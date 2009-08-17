using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CricketClubDomain
{
    public class AccountEntryData
    {
        public double Amount
        {
            get;
            set;
        }

        public string Description
        {
            get;
            set;
        }


        public DateTime Date
        {
            get;
            set;
        }

        public int CreditOrDebit
        {
            get;
            set;
        }

        public int MatchID
        {
            get;
            set;
        }

        public int PlayerID
        {
            get;
            set;
        }

        public int ID
        {
            get;
            set;
        }

        public int Status
        {
            get;
            set;
        }

        public int Type
        {
            get;
            set;
        }
    }
}
