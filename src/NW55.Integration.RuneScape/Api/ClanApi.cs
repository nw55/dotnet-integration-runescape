using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace NW55.Integration.RuneScape.Api
{
    public class ClanApi : RuneScapeApi<string, IList<ClanMember>>
    {
        static string ParsePlayerNameFromApi(string nameString)
        {
            // replace non breaking space with regular space
            return nameString.Replace('\xa0', '\x20');
        }

        static ClanRank ParseClanRankFromApi(string rankString)
        {
            switch (rankString.ToLowerInvariant().Trim())
            {
                case "recruit":
                    return ClanRank.Recruit;
                case "corporal":
                    return ClanRank.Corporal;
                case "sergeant":
                    return ClanRank.Sergeant;
                case "lieutenant":
                    return ClanRank.Lieutenant;
                case "captain":
                    return ClanRank.Captain;
                case "general":
                    return ClanRank.General;
                case "admin":
                    return ClanRank.Admin;
                case "organiser":
                    return ClanRank.Organiser;
                case "coordinator":
                    return ClanRank.Coordinator;
                case "overseer":
                    return ClanRank.Overseer;
                case "deputy owner": // api returns this
                case "deputyowner": // just be more robust
                    return ClanRank.DeputyOwner;
                case "owner":
                    return ClanRank.Owner;
                default:
                    throw new FormatException("Invalid clan rank: " + rankString);
            }
        }

        public override Encoding OverrideResponseEncoding => IsoEncoding;

        public override string GetUri(string clanName)
        {
            return $"{ServicesBaseUri}/m=clan-hiscores/members_lite.ws?clanName={clanName}";
        }

        public IList<ClanMember> ParseResult(string rawResponse)
        {
            if (rawResponse == null)
                return null;

            string[] lines = rawResponse.Split(new char[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);
            ClanMember[] members = new ClanMember[lines.Length - 1]; // skip first line (header)
            try
            {
                for (int i = 0; i < members.Length; i++)
                {
                    string line = lines[i + 1]; // skip first line (header)
                    string[] segments = line.Split(',');
                    string name = ParsePlayerNameFromApi(segments[0]);
                    ClanRank rank = ParseClanRankFromApi(segments[1]);
                    long clanXP = Int64.Parse(segments[2]);
                    int kills = Int32.Parse(segments[3]);
                    members[i] = new ClanMember(name, rank, clanXP, kills);
                }
            }
            catch (Exception e)
            {
                throw new InvalidDataException("Error parsing response.", e);
            }
            return members;
        }

        public override IList<ClanMember> ParseResult(string clanName, string rawResponse)
            => ParseResult(rawResponse);
    }
}