
using Totalview.BlazorClient.Client.TotalviewCommunication;
using Totalview.Server;

namespace Microsoft.Extensions.DependencyInjection
{
    internal static partial class ServiceCollectionExtensions
    {
        public static void AddTotalviewCommunication(this IServiceCollection services)
        {
            services.AddScoped<ITotalviewCommunicationService, TotalviewCommunicationService>();
            
            services.AddTransient<ITotalviewEventHandler, ResourceCollectionTotalviewEventHandler>();
            services.AddTransient<ITotalviewEventHandler, ReservationCollectionTotalviewEventHandler>();
            services.AddTransient<ITotalviewEventHandler, StateCollectionTotalviewEventHandler>();

            
            services.AddScoped<ITotalviewDataCache, TotalviewDataCache>();
            services.AddTransient((o) => (ITotalviewDataReaderCache)o.GetRequiredService<ITotalviewDataCache>());

            services.AddScoped<IDataCache<Resource>, ResourceDataCache>();
            services.AddScoped<IDataCache<Reservation>, ReservationDataCache>();
            services.AddScoped<IDataCache<State>, StateDataCache>();
        }
    }
}
