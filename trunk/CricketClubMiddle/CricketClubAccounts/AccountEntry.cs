using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CricketClubDomain;

namespace CricketClubAccounts
{
    public class AccountEntry
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


        /// <summary>
        /// Credit - Cash paid to the club
        /// Debit - monies owed to the club - match fees etc
        /// </summary>
        public CreditDebit CreditOrDebit
        {
            get;
            set;
        }

        public int MatchID
        {
            get;
            set;
        }

        
        
        public AccountEntry() {

        }

        public static void Create(PlayerAccount account, double amount, string description)
        {

        }
        
    }
}
