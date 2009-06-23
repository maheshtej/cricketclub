using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CricketClubMiddle
{
    public class Helpers
    {
        public static string ReadableOversString(decimal Overs)
        {
            decimal wholepart = Math.Round(Overs, 0);
            decimal fraction = Overs - wholepart;
            string overFraction = "";
            try
            {
                overFraction = Math.Round((fraction * 6), 1).ToString().Substring(1, 2);
            }
            catch
            {
                //Exact number of overs
            }
            string wholePartString = wholepart.ToString();

            if (overFraction == ".0")
            {
                overFraction = "";
            }
            return wholePartString + overFraction;
        }
    }
}
