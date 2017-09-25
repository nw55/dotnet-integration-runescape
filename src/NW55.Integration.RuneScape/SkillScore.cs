namespace NW55.Integration.RuneScape
{
    public class SkillScore : IScoreValue
    {
        public SkillScore(HiScoresSkill skill, long xp, int level, int? rank)
        {
            Skill = skill;
            XP = xp;
            Level = level;
            Rank = rank;
        }

        public HiScoresSkill Skill { get; }

        public long XP { get; }

        public int Level { get; }

        public int? Rank { get; }

        HiScoresEntry IScoreValue.Entry => Skill;

        long IScoreValue.Score => XP;
    }
}
