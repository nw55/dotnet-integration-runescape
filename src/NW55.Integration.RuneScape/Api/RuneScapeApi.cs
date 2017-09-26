using System;
using System.Collections.Generic;
using System.Text;

namespace NW55.Integration.RuneScape.Api
{
    public abstract class RuneScapeApi<TParameter, TResult>
    {
        public const string ServicesBaseUri = "http://services.runescape.com";

        public const string AppsBaseUri = "https://apps.runescape.com";

        public static readonly Encoding IsoEncoding = Encoding.GetEncoding("ISO-8859-1");

        public virtual Encoding OverrideResponseEncoding => null;

        public abstract string GetUri(TParameter parameter);
        
        public abstract TResult ParseResult(TParameter parameter, string responseText);
    }
}
