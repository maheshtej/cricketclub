using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CricketClubDomain
{
    public class PhotoCommentData
    {
        public PhotoCommentData()
        {

        }

        public string Comment
        {
            get;
            set;
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

        public int PhotoID
        {
            get;
            set;
        }

        public DateTime CommentTime
        {
            get;
            set;
        }

    }
}
