using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using NW55.Integration.RuneScape.RuneMetrics;

namespace NW55.Integration.RuneScape.Api
{
    public class RuneMetricsProfileApi : RuneScapeApi<RuneMetricsProfileApi.Parameters, RuneMetricsProfileApi.Result>
    {
        public const int DefaultActivityCount = 4;
        public const int MaxActivityCount = 20;

        static RuneMetricsError ParseError(string errorString)
        {
            switch (errorString)
            {
                case null:
                    return RuneMetricsError.NoError;
                case "NO_PROFILE":
                    return RuneMetricsError.NoProfile;
                case "NOT_A_MEMBER":
                    return RuneMetricsError.NoMember;
                case "PROFILE_PRIVATE":
                    return RuneMetricsError.PrivateProfile;
                default:
                    return RuneMetricsError.UnknownError;
            }
        }

        public override string GetUri(Parameters parameters)
        {
            if (parameters.ActivityCount < 0 || parameters.ActivityCount > MaxActivityCount)
                throw new ArgumentOutOfRangeException(nameof(parameters), "ActivityCount out of range");

            return $"{AppsBaseUri}/runemetrics/profile/profile?user={parameters.PlayerName}&activities={parameters.ActivityCount}";
        }

        public Result ParseResult(string rawResponse)
        {
            ApiResult result = JsonConvert.DeserializeObject<ApiResult>(rawResponse);

            RuneMetricsError error = ParseError(result.Error);
            RuneMetricsProfile profile = null;

            if (error == RuneMetricsError.NoError)
            {
                string name = result.Name;

                int? overallRank = result.Rank == null ? (int?)null : Int32.Parse(result.Rank.Replace(",", ""));
                SkillScore overall = new SkillScore(HiScoresEntries.Overall, result.TotalXP, result.TotalSkill, overallRank);

                RuneMetricsProfile.CombatInfo combatInfo = new RuneMetricsProfile.CombatInfo(result.Magic, result.Ranged, result.Melee, result.CombatLevel);

                RuneMetricsProfile.QuestsInfo questsInfo = new RuneMetricsProfile.QuestsInfo(result.QuestsNotStarted, result.QuestsStarted, result.QuestsComplete);

                RuneMetricsSkillScores.Entry[] skillScoreEntries = new RuneMetricsSkillScores.Entry[result.SkillValues.Length + 1];
                skillScoreEntries[0] = new RuneMetricsSkillScores.Entry(HiScoresEntries.Overall.RuneMetricsId, overall.Level, overall.XP, overall.Rank);
                for (int i = 0; i < result.SkillValues.Length; i++)
                {
                    ApiSkillValueEntry entry = result.SkillValues[i];
                    long xp = entry.XP / 10;
                    skillScoreEntries[i + 1] = new RuneMetricsSkillScores.Entry(entry.Id, entry.Level, xp, entry.Rank);
                }
                RuneMetricsSkillScores skillScores = new RuneMetricsSkillScores(skillScoreEntries);

                AdventurersLogActivity[] activities = new AdventurersLogActivity[result.Activities.Length];
                for (int i = 0; i < result.Activities.Length; i++)
                {
                    ApiActivityEntry entry = result.Activities[i];
                    activities[i] = new AdventurersLogActivity(entry.Date, entry.Text, entry.Details);
                }

                profile = new RuneMetricsProfile(name, overall, combatInfo, questsInfo, skillScores, activities);
            }

            return new Result(error, profile);
        }

        public override Result ParseResult(Parameters parameters, string rawResponse)
            => ParseResult(rawResponse);

        public class Parameters
        {
            public Parameters(string playerName)
                : this(playerName, DefaultActivityCount)
            {
            }

            public Parameters(string playerName, int activityCount)
            {
                PlayerName = playerName;
                ActivityCount = activityCount;
            }

            public string PlayerName { get; }

            public int ActivityCount { get; }
        }

        public class Result
        {
            public Result(RuneMetricsError error, RuneMetricsProfile profile)
            {
                Error = error;
                Profile = profile;
            }

            public RuneMetricsError Error { get; }

            public RuneMetricsProfile Profile { get; }

            public bool Success => Error == RuneMetricsError.NoError;
        }

#pragma warning disable CS0649 // no value assigned to field
        // fields are assigned by JsonConvert.DeserializeObject
        class ApiResult
        {
            // error only
            public string Error;

            // success only
            public int Magic;
            public int QuestsStarted;
            public int TotalSkill;
            public int QuestsComplete;
            public int QuestsNotStarted;
            public long TotalXP;
            public int Ranged;
            public ApiActivityEntry[] Activities;
            public ApiSkillValueEntry[] SkillValues;
            public string Name;
            public string Rank; // with comma between thousands
            public int Melee;
            public int CombatLevel;

            // set for error and success
            public string LoggedIn; // bool
        }

        class ApiActivityEntry
        {
            public DateTime Date;
            public string Details;
            public string Text;
        }

        class ApiSkillValueEntry
        {
            public int Level;
            public long XP; // NOTE: api returns one decimal place in integer
            public int? Rank;
            public int Id;
        }
#pragma warning restore CS0649
    }
}
