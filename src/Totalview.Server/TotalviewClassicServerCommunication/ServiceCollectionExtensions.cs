using Totalview.Server.TotalviewClassicServerCommunication;
using Totalview.Server.TotalviewClassicServerCommunication.StreamBaseObjectHandlers;

namespace Microsoft.Extensions.DependencyInjection
{
    internal static partial class ServiceCollectionExtensions
    {
        public static IServiceCollection AddTotalviewClassicServerCommunication(this IServiceCollection services)
        {
            // Totalview Classic Server Communication
            // This fasilitates the PROXY features of the new server
            services.AddSingleton<ITotalviewClassicServerCommunicationService, TotalviewClassicServerCommunicationService>();
            services.Scan(scan => scan
                .FromCallingAssembly()
                    .AddClasses(classes => classes.AssignableTo<IStreamBaseObjectHandler>())
                    .AsImplementedInterfaces()
                    .AsSelf()
                    .WithTransientLifetime()
            );

            return services;
        }
    }
}
