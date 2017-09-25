namespace NW55.Integration.RuneScape
{
    public class HiScoresEntry
    {
        public HiScoresEntry(int hiScoresIndex, string name)
        {
            HiScoresIndex = hiScoresIndex;
            Name = name;
        }

        public int HiScoresIndex { get; }

        public string Name { get; }
    }
}
