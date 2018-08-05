using System;

namespace NW55.Integration.RuneScape.RuneMetrics
{
    public class AdventurersLogActivity
    {
        public AdventurersLogActivity(DateTime date, string text, string details)
        {
            Date = date;
            Text = text;
            Details = details;
        }

        public DateTime Date { get; }

        public string Text { get; }

        public string Details { get; }

        public bool HasType(KnownActivityType type) => type.IsMatch(this);

        public bool TryGetDetails<TDetails>(KnownDetailsActivityType<TDetails> type, out TDetails details)
            => type.TryGetDetails(this, out details);
    }
}
