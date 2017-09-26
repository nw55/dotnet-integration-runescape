using System;

namespace NW55.Integration.RuneScape
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
    }
}
