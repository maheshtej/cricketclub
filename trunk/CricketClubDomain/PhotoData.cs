using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CricketClubDomain
{
    public class PhotoData
    {
        public PhotoData()
        {

        }

        public int ID
        {
            get;
            set;
        }

        public int AuthorID
        {
            get;
            set;
        }

        public string FileName
        {
            get;
            set;
        }

        public string Title
        {
            get;
            set;
        }

        public int MatchID
        {
            get;
            set;
        }

        public DateTime UploadDate
        {
            get;
            set;
        }
    }
}
