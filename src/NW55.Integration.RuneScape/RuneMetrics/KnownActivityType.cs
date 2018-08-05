using System;
using System.Collections.Generic;
using System.Text;

namespace NW55.Integration.RuneScape.RuneMetrics
{
    public abstract class KnownActivityType
    {
        public abstract bool IsMatch(AdventurersLogActivity activity);
    }
}
