namespace NW55.Integration.RuneScape
{
    public class ScoreValue : IScoreValue
    {
        public ScoreValue(HiScoresEntry entry, long score, int? rank)
        {
            Entry = entry;
            Score = score;
            Rank = rank;
        }

        public HiScoresEntry Entry { get; }

        public long Score { get; }

        public int? Rank { get; }
    }
}
