using System;
using System.Collections.Generic;
using System.Text;

namespace NW55.Integration.RuneScape.RuneMetrics
{
    public class RuneMetricsProfile
    {
        public RuneMetricsProfile(string name, SkillScore overall, CombatInfo combat, QuestsInfo quests, RuneMetricsSkillScores skills, IList<AdventurersLogActivity> activities)
        {
            Name = name;
            Overall = overall;
            Combat = combat;
            Quests = quests;
            Skills = skills;
            Activities = activities;
        }

        public string Name { get; }

        public SkillScore Overall { get; }
        
        public CombatInfo Combat { get; }

        public QuestsInfo Quests { get; }

        public RuneMetricsSkillScores Skills { get; }

        public IList<AdventurersLogActivity> Activities { get; }

        public class CombatInfo
        {
            public CombatInfo(int magic, int ranged, int melee, int combatLevel)
            {
                Magic = magic;
                Ranged = ranged;
                Melee = melee;
                CombatLevel = combatLevel;
            }

            public int Magic { get; }
            public int Ranged { get; }
            public int Melee { get; }
            public int CombatLevel { get; }
        }

        public class QuestsInfo
        {
            public QuestsInfo(int notStarted, int started, int complete)
            {
                NotStarted = notStarted;
                Started = started;
                Complete = complete;
            }

            public int NotStarted { get; }
            public int Started { get; }
            public int Complete { get; }
        }
    }
}
