using System;
using System.ComponentModel.Design;
using Totalview.Communication;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddTotalviewCommunication(this IServiceCollection services)
        {
            services.AddTransient<ITcpConnection, TcpConnection>();
            services.AddSingleton<ITotalviewState, TotalviewState>();
            services.AddSingleton<ITotalviewBasicData, TotalviewBasicData>();
            services.AddTransient<ITotalviewClient, TotalviewClient>();

            return services;
        }

        public static IServiceCollection AddTotalviewCommunication(this IServiceCollection services, Action<TotalviewOptions> configure)
        {
            services.Configure<TotalviewOptions>(o => configure.Invoke(o));

            services.AddTransient<ITcpConnection, TcpConnection>();
            services.AddSingleton<ITotalviewState, TotalviewState>();
            services.AddSingleton<ITotalviewBasicData, TotalviewBasicData>();
            services.AddTransient<ITotalviewClient, TotalviewClient>();

            return services;
        }

        //public static IServiceCollection AddTotalviewCommunication(this IServiceCollection services, Action<TotalviewOptions> configure)
        //{
        //    TotalviewOptions totalviewOptions = new();
        //    configure.Invoke(totalviewOptions);

        //    services.AddSingleton(s => totalviewOptions);

        //    services.AddTransient<ITcpConnection, TcpConnection>();
        //    services.AddSingleton<ITotalviewState, TotalviewState>();
        //    services.AddSingleton<ITotalviewBasicData, TotalviewBasicData>();
        //    services.AddTransient<ITotalviewClient, TotalviewClient>();

        //    return services;
        //}
    }
}
