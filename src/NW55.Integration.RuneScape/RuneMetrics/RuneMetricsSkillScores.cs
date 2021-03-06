﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace NW55.Integration.RuneScape.RuneMetrics
{
    public class RuneMetricsSkillScores : ISkillScores
    {
        Dictionary<int, Entry> skillScores;

        public RuneMetricsSkillScores(IEnumerable<Entry> skillScores)
        {
            this.skillScores = skillScores.ToDictionary(score => score.Id);
        }

        public SkillScore GetSkillScore(HiScoresSkill skill)
        {
            if (skillScores.TryGetValue(skill.RuneMetricsId, out Entry entry))
                return new SkillScore(skill, entry.XP, entry.Level, entry.Rank);
            else
                throw new ArgumentException("Skill not found", nameof(skill));
        }

        IScoreValue IScores.GetScoreValue(HiScoresEntry entry)
        {
            if (entry is HiScoresSkill skill)
                return GetSkillScore(skill);
            else
                throw new ArgumentException("Skill entry required.", nameof(entry));
        }

        public class Entry
        {
            public Entry(int id, int level, long xp, int? rank)
            {
                Id = id;
                Level = level;
                XP = xp;
                Rank = rank;
            }

            public int Id { get; }

            public int Level { get; }

            public long XP { get; }

            public int? Rank { get; }
        }
    }
}
