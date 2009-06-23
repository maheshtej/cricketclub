using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CricketClubMiddle;
using CricketClubDAL;

namespace CricketClubAccounts
{
    public class PlayerAccount
    {
        private Player _player;

        public PlayerAccount(Player player)
        {
            _player = player;
        }

        public List<AccountEntry> GetStatement()
        {
            DAO myDAO = new DAO();
            
            return new List<AccountEntry>();
        }

        public double GetBalance()
        {
            double balance = 0.0;

            foreach (AccountEntry entry in this.GetStatement())
            {
                balance = balance + entry.Amount;
            }

            return balance;
        }

        public void AddEntry(double amount, string description)
        {

        }
    }
}
