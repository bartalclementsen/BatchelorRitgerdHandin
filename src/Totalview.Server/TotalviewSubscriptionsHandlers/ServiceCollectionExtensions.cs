using Totalview.Server.TotalviewSubscriptionsHandlers;

namespace Microsoft.Extensions.DependencyInjection
{
    internal static partial class ServiceCollectionExtensions
    {
        public static IServiceCollection AddSubscriptionsHandlers(this IServiceCollection services)
        {
            services.AddSingleton<ITotalviewSubscriptionsHandler, TotalviewSubscriptionsHandler>();
            services.AddTransient<ITotalviewSubscriptionFactory, TotalviewSubscriptionFactory>();
            return services;
        }
    }
}
