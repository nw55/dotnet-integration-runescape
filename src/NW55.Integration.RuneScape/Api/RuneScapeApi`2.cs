using System;
using System.Collections.Generic;
using System.Text;

namespace NW55.Integration.RuneScape.Api
{
    public abstract class RuneScapeApi<TParameter, TResult> : RuneScapeApi
    {
        public abstract string GetUri(TParameter parameter);
        
        public abstract TResult ParseResult(TParameter parameter, string rawResponse);
    }
}
