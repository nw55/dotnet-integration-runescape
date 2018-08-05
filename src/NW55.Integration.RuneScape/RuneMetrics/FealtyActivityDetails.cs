using System;
using System.Collections.Generic;
using System.Text;

namespace NW55.Integration.RuneScape.RuneMetrics
{
    public struct FealtyActivityDetails
    {
        public FealtyActivityDetails(int level, bool maintained)
        {
            Level = level;
            Maintained = maintained;
        }

        public int Level { get; }

        public bool Maintained { get; }
    }
}
