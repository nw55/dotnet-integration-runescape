namespace NW55.Integration.RuneScape
{
    public interface IScores
    {
        IScoreValue GetScoreValue(HiScoresEntry entry);
    }
}
