using System;

namespace NW55.Integration.RuneScape
{
    public class PlayerDetails
    {
        public PlayerDetails(string name, string clanName, bool recruiting, string title, bool isSuffix)
        {
            Name = name;
            ClanName = String.IsNullOrEmpty(clanName) ? null : clanName;
            ClanIsRecruiting = recruiting;
            Title = String.IsNullOrEmpty(title) ? null : title;
            TitleIsSuffix = isSuffix;
        }

        public string Name { get; }

        public string ClanName { get; }

        public bool ClanIsRecruiting { get; }

        public string Title { get; }

        public bool TitleIsSuffix { get; }

        public bool IsInClan => ClanName != null;

        public bool NameExistsCertain => ClanName != null || Title != null;

        public override string ToString() => $"PlayerDetails({Name},{ClanName},{Title})";
    }
}
