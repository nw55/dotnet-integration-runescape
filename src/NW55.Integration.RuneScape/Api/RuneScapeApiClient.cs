using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace NW55.Integration.RuneScape.Api
{
    public class RuneScapeApiClient
    {
        HttpClient httpClient;

        public RuneScapeApiClient(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<string> GetRaw<TParameter, TResult>(RuneScapeApi<TParameter, TResult> api, TParameter parameter)
        {
            string uri = api.GetUri(parameter);

            string responseText;

            using (var response = await httpClient.GetAsync(uri))
            {
                if (response.StatusCode == HttpStatusCode.NotFound)
                {
                    responseText = null;
                }
                else
                {
                    response.EnsureSuccessStatusCode();
                    if (api.OverrideResponseEncoding != null)
                    {
                        using (var stream = await response.Content.ReadAsStreamAsync())
                        using (var reader = new StreamReader(stream, api.OverrideResponseEncoding))
                            responseText = await reader.ReadToEndAsync();
                    }
                    else
                    {
                        responseText = await response.Content.ReadAsStringAsync();
                    }
                }
            }

            return responseText;
        }

        public async Task<TResult> Get<TParameter, TResult>(RuneScapeApi<TParameter, TResult> api, TParameter parameter)
        {
            string responseText = await GetRaw(api, parameter);

            return api.ParseResult(parameter, responseText);
        }
    }
}
