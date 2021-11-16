using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Sunday.ElasticSearch.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Sunday.ElasticSearch
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddEsRepository(this IServiceCollection services, IConfiguration configuration)
        {
            if (services == null)
                throw new ArgumentNullException(nameof(services));

            services.Configure<EsConfig>(options =>
            {
                options.Urls = configuration.GetSection("EsConfig:ConnectionStrings").GetChildren().Select(p => p.Value).ToList();
            });

            services.AddSingleton<IEsClientProvider, EsClientProvider>();
            services.AddEsRepository();

            return services;
        }

        public static IServiceCollection AddEsRepository(this IServiceCollection services, Action<EsConfig> optionAction)
        {
            if (services == null)
                throw new ArgumentNullException(nameof(services));

            EsConfig esConfig = new EsConfig();
            optionAction?.Invoke(esConfig);

            services.AddSingleton<IEsClientProvider, EsClientProvider>();
            services.AddEsRepository();

            return services;
        }

        private static IServiceCollection AddEsRepository(this IServiceCollection services)
        {
            IEnumerable<Assembly> assemblies = AppDomain.CurrentDomain.GetCurrentPathAssembly().Where(x => !x.GetName().Name.Equals("Sunday.ElasticSearch"));
            services.AddEsRepository(assemblies, typeof(IEsRepository<>));
            return services;
        }

        private static void AddEsRepository(this IServiceCollection services, IEnumerable<Assembly> assemblies, Type baseType)
        {
            foreach (var assembly in assemblies)
            {
                var types = assembly.GetTypes()
                                    .Where(x => x.IsClass 
                                            && !x.IsAbstract 
                                            && x.BaseType != null
                                            && x.HasImplementedRawGeneric(baseType));
                foreach (var type in types)
                {
                    Type[] interfaces = type.GetInterfaces();
                    Type interfaceType = interfaces.FirstOrDefault(x => x.Name == $"I{type.Name}");
                    if (interfaceType == null) interfaceType = type;
                    ServiceDescriptor serviceDescriptor = new ServiceDescriptor(interfaceType, type, ServiceLifetime.Transient);
                    if (!services.Contains(serviceDescriptor)) services.Add(serviceDescriptor);
                }
            }
        }
    }
}
