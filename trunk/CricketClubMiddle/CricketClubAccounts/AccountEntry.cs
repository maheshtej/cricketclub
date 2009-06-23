using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
        
        public AccountEntry() {

        }

        public static void Create(PlayerAccount account, double amount, string description)
        {

        }
        
    }
}
