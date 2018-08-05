using System;
using System.Collections.Generic;
using System.Text;

namespace NW55.Integration.RuneScape.RuneMetrics
{
    public static class KnownActivityTypes
    {
        public static KnownActivityType ClanCitadelCap { get; } = new SimpleContainsActivityType("Capped at my Clan Citadel");
        public static KnownActivityType ClanCitadelVisit { get; } = new SimpleContainsActivityType("Visited my Clan Citadel");
        public static KnownDetailsActivityType<FealtyActivityDetails> ClanFealty { get; } = new FealtyActivityType();

        class SimpleContainsActivityType : KnownActivityType
        {
            readonly string text;

            public SimpleContainsActivityType(string text)
            {
                this.text = text;
            }

            public override bool IsMatch(AdventurersLogActivity activity)
                => activity.Text.IndexOf(text, StringComparison.OrdinalIgnoreCase) >= 0;
        }

        class FealtyActivityType : KnownDetailsActivityType<FealtyActivityDetails>
        {
            static readonly char[] fealtyLevelChars = new[] { '1', '2', '3' };

            public override bool IsMatch(AdventurersLogActivity activity)
                => activity.Text.IndexOf("Clan Fealty", StringComparison.OrdinalIgnoreCase) >= 0;

            protected override FealtyActivityDetails ParseDetails(AdventurersLogActivity activity)
            {
                if (activity.Text.IndexOf("Maintained", StringComparison.OrdinalIgnoreCase) >= 0)
                    return new FealtyActivityDetails(3, true);

                int levelIndex = activity.Text.IndexOfAny(fealtyLevelChars);
                char levelChar = activity.Text[levelIndex];
                int level = levelChar - '0';
                return new FealtyActivityDetails(level, false);
            }
        }
    }
}
