using Totalview.Server.Services;

namespace Microsoft.Extensions.DependencyInjection
{
    internal static partial class ServiceCollectionExtensions
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddSingleton<ITotalviewDataService, TotalviewDataService>();
            return services;
        }
    }
}
