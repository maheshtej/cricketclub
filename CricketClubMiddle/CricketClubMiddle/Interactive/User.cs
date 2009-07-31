using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using CricketClubDomain;
using CricketClubDAL;

namespace CricketClubMiddle.Interactive
{
    public class User
    {
        private UserData _data;

        private User(UserData data)
        {
            _data = data;
        }

        public string Name
        {
            get
            {
                return _data.Name;
            }
            set
            {
                _data.Name = value;
            }
        }

        public string Password
        {
            get
            {
                return _data.Password;
            }
            set
            {
                _data.Password = value;
            }
        }

        public int ID
        {
            get
            {
                return _data.ID;
            }
        }

        public string DisplayName
        {
            get
            {
                return _data.DisplayName;
            }
            set
            {
                _data.DisplayName = value;
            }
        }

        public string EmailAddress
        {
            get { return _data.EmailAddress; }
            set { _data.EmailAddress = value; }
        }

        public User(int ID)
        {
            User u = (from a in GetAll() where a.ID == ID select a).FirstOrDefault();
            _data = u._data;
        }

        public static User GetByName(string Username)
        {
            User u = (from a in GetAll() where a.Name == Username select a).FirstOrDefault();
            return u;
        }

        public static User CreateNew(string UserName, string Password, string emailaddress, string displayname) 
        {
            DAO myDAO = new DAO();
            int id = myDAO.CreateNewUser(UserName, emailaddress, Password, displayname);
            return new User(id);
        }

        public static List<User> GetAll()
        {
            DAO myDao = new DAO();
            return (from a in myDao.GetAllUsers() select new User(a)).ToList(); 
        }

        public void Save()
        {
            DAO myDao = new DAO();
            myDao.UpdateUser(_data);
        }

        public void AuthenticateUser(string password, bool isEncoded)
        {
            if (!isEncoded)
            {
                if (password == Password)
                {
                    IsLoggedIn = true;
                }
            }
            else
            {
                if (password == Helpers.MD5HashString(Password))
                {
                    IsLoggedIn = true;
                }
            }
        }

        public bool IsLoggedIn
        {
            get;
            private set;
        }

        
    }
}
