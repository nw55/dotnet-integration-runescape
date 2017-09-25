namespace NW55.Integration.RuneScape
{
    public interface IScoreValue
    {
        HiScoresEntry Entry { get; }

        long Score { get; }

        int? Rank { get; }
    }
}
