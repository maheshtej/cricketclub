using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CricketClubDomain
{
    public class UserData
    {
        public UserData() { }

        public int ID
        {
            get;
            set;
        }

        public string Name
        {
            get;
            set;
        }

        public string EmailAddress
        {
            get;
            set;
        }

        public string Password
        {
            get;
            set;
        }

        public string DisplayName
        {
            get;
            set;
        }

        public int Permissions
        {
            get;
            set;
        }
    }
}
