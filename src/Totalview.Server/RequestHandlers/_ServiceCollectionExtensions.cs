using Totalview.Server.RequestHandlers;

namespace Microsoft.Extensions.DependencyInjection
{
    internal static partial class ServiceCollectionExtensions
    {
        public static IServiceCollection AddRequestHandlers(this IServiceCollection services)
        {
            services.AddSingleton<IRequestHandlerHub, RequestHandlerHub>();
            services.Scan(scan => scan
                .FromCallingAssembly()
                    .AddClasses(classes => classes.AssignableTo<IRequestHandler>())
                    .AsImplementedInterfaces()
                    .WithTransientLifetime());

            return services;
        }
    }
}
