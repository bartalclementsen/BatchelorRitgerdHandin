using Totalview.BlazorMvvm;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddBlazorMvvm(this IServiceCollection services)
        {
            services.AddTransient<INavigationService, NavigationService>();
            services.AddScoped<IErrorReportingService, ErrorReportingService>();
            services.AddTotalviewMediator();
            return services;
        }
    }
}
