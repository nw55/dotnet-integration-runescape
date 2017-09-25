using System;
using System.Collections.Generic;
using System.Text;

namespace NW55.Integration.RuneScape
{
    public class RuneMetricsProfile
    {
        public string Name { get; }

        public int TotalXP { get; }
        public int TotalSkill { get; }
        public int? Rank { get; }

        public int Magic { get; }
        public int Ranged { get; }
        public int Melee { get; }
        public int CombatLevel { get; }

        public int QuestsNotStarted { get; }
        public int QuestsStarted { get; }
        public int QuestsComplete { get; }

        public RuneMetricsSkillScores Skills { get; }

        public IList<Activity> Activities { get; }
    }
}
