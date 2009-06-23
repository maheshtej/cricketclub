using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CricketClubDomain
{
    public class NewsData
    {
        public NewsData() { }

        public string Headline
        {
            get;
            set;
        }
        public string Story
        {
            get;
            set;
        }
        public DateTime Date
        {
            get;
            set;
        }

        public string ShortHeadline
        {
            get;
            set;
        }

        public string Teaser
        {
            get;
            set;
        }
    }
}
