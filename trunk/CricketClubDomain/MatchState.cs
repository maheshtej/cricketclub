namespace CricketClubDomain
{
    public class MatchState
    {
        public PlayerState[] Players;
        public int LastCompletedOver;
        public Over Over;
        public int Score;
        public decimal RunRate;
    }

    public class PlayerState
    {
        public string PlayerName;
        public int PlayerId;
        public string State;
        public int Position;
    }
}