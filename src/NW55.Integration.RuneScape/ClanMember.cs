namespace NW55.Integration.RuneScape
{
    public class ClanMember
    {
        public ClanMember(string name, ClanRank rank, long clanXP, int kills)
        {
            Name = name;
            Rank = rank;
            ClanXP = clanXP;
            Kills = kills;
        }

        public string Name { get; }

        public ClanRank Rank { get; }

        public long ClanXP { get; }

        public int Kills { get; }
    }
}
