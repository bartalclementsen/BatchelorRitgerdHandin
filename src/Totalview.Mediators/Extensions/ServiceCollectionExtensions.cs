using Totalview.Mediators;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddTotalviewMediator(this IServiceCollection services)
        {
            services.AddSingleton<IMediator, Mediator>();
            return services;
        }
    }
}
