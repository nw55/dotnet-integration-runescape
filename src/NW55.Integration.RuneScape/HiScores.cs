using System;
using System.Collections.Generic;

namespace NW55.Integration.RuneScape
{
    public class HiScores : IScores, ISkillScores
    {
        IList<Entry> entries;

        public HiScores(IList<Entry> entries)
        {
            this.entries = entries;
        }

        public IScoreValue GetScoreValue(HiScoresEntry entry)
        {
            if (entry.HiScoresIndex < 0 || entry.HiScoresIndex >= entries.Count)
                throw new ArgumentOutOfRangeException(nameof(entry), "HiScoresIndex out of range");

            if (entry is HiScoresSkill skill)
                return GetSkillScore(skill);

            Entry value = entries[entry.HiScoresIndex];
            if (value.Level != null)
                throw new ArgumentException("Score is a skill");
            return new ScoreValue(entry, value.Score, value.Rank);
        }

        public SkillScore GetSkillScore(HiScoresSkill skill)
        {
            Entry value = entries[skill.HiScoresIndex];
            if (value.Level == null)
                throw new ArgumentException("Score is not a skill");
            return new SkillScore(skill, value.Score, value.Level.Value, value.Rank);
        }

        public class Entry
        {
            public int Rank { get; set; }

            public int? Level { get; set; }

            public long Score { get; set; }
        }
    }
}
