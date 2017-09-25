namespace NW55.Integration.RuneScape
{
    public interface ISkillScores : IScores
    {
        SkillScore GetSkillScore(HiScoresSkill skill);
    }
}
