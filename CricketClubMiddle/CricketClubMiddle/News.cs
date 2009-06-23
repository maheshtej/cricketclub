using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CricketClubDAL;

namespace CricketClubMiddle
{
    public class News
    {
        public static IEnumerable<NewsItem> GetLastXStories(int number)
        {
            DAO myDao = new DAO();
            return from a in myDao.GetTopXStories(number)
                       select new NewsItem(a);
        }

        public static void SubmitNewStory(NewsItem story)
        {
            DAO myDao = new DAO();
            myDao.SaveNewsStory(story._data);
        }
    }
}
