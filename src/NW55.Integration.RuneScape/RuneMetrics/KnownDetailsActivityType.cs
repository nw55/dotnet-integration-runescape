using System;
using System.Collections.Generic;
using System.Text;

namespace NW55.Integration.RuneScape.RuneMetrics
{
    public abstract class KnownDetailsActivityType<TDetails> : KnownActivityType
    {
        public bool TryGetDetails(AdventurersLogActivity activity, out TDetails details)
        {
            if (IsMatch(activity))
            {
                details = ParseDetails(activity);
                return true;
            }
            else
            {
                details = default;
                return false;
            }
        }

        protected abstract TDetails ParseDetails(AdventurersLogActivity activity);
    }
}
