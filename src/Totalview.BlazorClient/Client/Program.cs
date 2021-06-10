using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Configuration;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Totalview.BlazorClient.Client.Services;
using Totalview.BlazorClient.Client.ViewModels;
using Totalview.BlazorMvvm;

namespace Totalview.BlazorClient.Client
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            // Must be set to allow usgin Unencrypted http/2 messages
            AppContext.SetSwitch("System.Net.Http.SocketsHttpHandler.Http2UnencryptedSupport", true);

            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");

            builder.Logging.AddConfiguration(builder.Configuration.GetSection("Logging"));

            builder.Services.AddOidcAuthentication(options =>
            {
                builder.Configuration.Bind("Authentication", options.ProviderOptions);

                options.ProviderOptions.DefaultScopes.Add("openid");
                options.ProviderOptions.DefaultScopes.Add("profile");
                options.ProviderOptions.DefaultScopes.Add("totalview-server");
            });

            builder.Services.AddTotalviewCommunication();

            builder.Services.AddScoped<IDialogService, DialogService>();

            builder.Services.AddBlazorMvvm();

            //View Models
            builder.Services.Scan(scan => scan
                .FromCallingAssembly()
                    .AddClasses(classes => classes.AssignableTo<ViewModelBase>())
                    .AsSelf()
                    .WithTransientLifetime()
            );

            builder.Services.AddTransient<Func<ResourceViewModel>>(c => () => c.GetRequiredService<ResourceViewModel>());

            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

            await builder.Build().RunAsync();
        }
    }
}
