using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Sunday.ElasticSearch.Repository.Api.ServiceExtensions
{
    public static class ElasticSearchSetup
    {
        public static void AddElasticSearchSetup(this IServiceCollection services, IConfiguration configuration)
        {
            if (services == null)
                throw new ArgumentNullException(nameof(services));

            services.AddEsRepository(configuration);
        }
    }
}