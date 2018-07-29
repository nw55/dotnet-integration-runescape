using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using NW55.Integration.RuneScape.Api;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class NW55RuneScapeIntegrationServiceCollectionExtensions
    {
        public static IServiceCollection AddRuneScapeApiClient(this IServiceCollection services)
        {
            services.AddHttpClient<RuneScapeApiClient>()
                .ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler()
                {
                    MaxConnectionsPerServer = 20
                });
            return services;
        }
    }
}
