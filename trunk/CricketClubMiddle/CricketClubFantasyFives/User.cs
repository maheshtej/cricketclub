using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CricketClubFantasyFives
{
    public class User
    {
        public string Name
        {
            get;
            set;
        }

        public string Password
        {
            get;
            set;
        }

        public int ID
        {
            get;
            private set;
        }

        public User(int ID)
        {

        }

        public static User GetByName(string Username)
        {
            return new User(0);
        }

        public static User CreateNew(string UserName, string Password) 
        {
            return new User(0);
        }

        public void Save()
        {
            //save the user.
        }

        public int CurrentScore
        {
            get;
            protected set;
        }

        public int ScoreForLastMatch
        {
            get;
            protected set;
        }


        internal static IEnumerable<User> GetAll()
        {
            throw new NotImplementedException();
        }
    }
}
