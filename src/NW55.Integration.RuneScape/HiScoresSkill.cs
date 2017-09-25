namespace NW55.Integration.RuneScape
{
    public class HiScoresSkill : HiScoresEntry
    {
        public HiScoresSkill(int hiScoresIndex, string name, int runeMetricsId)
            : base(hiScoresIndex, name)
        {
            RuneMetricsId = runeMetricsId;
        }

        public int RuneMetricsId { get; }
    }
}
