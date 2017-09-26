using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace NW55.Integration.RuneScape.Api
{
    public class RuneScapeApiClient : IDisposable
    {
        HttpClient client;

        public RuneScapeApiClient()
        {
            HttpClientHandler handler = new HttpClientHandler();
            handler.MaxConnectionsPerServer = 20;
            client = new HttpClient(handler, true);
        }

        public void Dispose()
        {
            client.Dispose();
        }

        public async Task<TResult> Get<TParameter, TResult>(RuneScapeApi<TParameter, TResult> api, TParameter parameter)
        {
            string uri = api.GetUri(parameter);

            string responseText;

            using (HttpResponseMessage response = await client.GetAsync(uri))
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
                        using (Stream stream = await response.Content.ReadAsStreamAsync())
                        using (StreamReader reader = new StreamReader(stream, api.OverrideResponseEncoding))
                            responseText = await reader.ReadToEndAsync();
                    }
                    else
                    {
                        responseText = await response.Content.ReadAsStringAsync();
                    }
                }
            }

            return api.ParseResult(parameter, responseText);
        }
    }
}
