using System;
using System.Collections.Generic;
using System.Text;

namespace NW55.Integration.RuneScape.Api
{
    public abstract class RuneScapeApi
    {
        public const string ServicesBaseUri = "http://services.runescape.com";
        public const string AppsBaseUri = "https://apps.runescape.com";

        public static readonly Encoding IsoEncoding = Encoding.GetEncoding("ISO-8859-1");

        public static ClanApi Clans { get; } = new ClanApi();
        public static HiScoresApi HiScores { get; } = new HiScoresApi();
        public static PlayerDetailsApi PlayerDetails { get; } = new PlayerDetailsApi();
        public static RuneMetricsProfileApi RuneMetricsProfile { get; } = new RuneMetricsProfileApi();

        public virtual Encoding OverrideResponseEncoding => null;
    }
}
