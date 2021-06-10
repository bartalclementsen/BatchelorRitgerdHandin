using AutoMapper;
using IdentityModel;
using IdentityServer4.AccessTokenValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Logging;
using System;
using Totalview.Communication;
using Totalview.Server.GrpcServices;
using Totalview.Server.TotalviewClassicServerCommunication;

namespace Totalview.Server
{
    internal class Startup
    {
        /* ----------------------------------------------------------------------------  */
        /*                                  PROPERTIES                                   */
        /* ----------------------------------------------------------------------------  */
        private readonly IConfiguration Configuration;

        /* ----------------------------------------------------------------------------  */
        /*                                 CONSTRUCTORS                                  */
        /* ----------------------------------------------------------------------------  */
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        /* ----------------------------------------------------------------------------  */
        /*                                PUBLIC METHODS                                 */
        /* ----------------------------------------------------------------------------  */
        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddOptions(Configuration)
                .AddCustomAutoMappe()
                .AddTotalviewMediator()
                .AddTotalviewCommunication()
                .AddSubscriptionsHandlers()
                .AddServices()
                .AddTotalviewClassicServerCommunication()
                .AddRequestHandlers()
                .AddCustomControllers()
                .AddCustomGrpc()
                .AddCustomAuthentication(Configuration)
                .AddCustomCors();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ITotalviewClassicServerCommunicationService totalviewClassicServerCommunicationService, ILogger<Startup> logger)
        {
            try
            {
                logger.LogInformation("Starting totalview classic server communication service");

                totalviewClassicServerCommunicationService.Start();
            }
            catch (Exception ex)
            {
                logger.LogCritical(ex, $"Could not start {nameof(totalviewClassicServerCommunicationService)}");
                throw; // Let application crash if connection to classic server could not be started
            }

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();
            app.UseCors();

            app.UseGrpcWeb(new GrpcWebOptions()
            {
                DefaultEnabled = true
            });

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints
                .MapGrpcService<TotalviewServiceImplementation>()
                    .RequireCors("AllowAll");

                // Add gRPC reflection for testing
                if (env.IsDevelopment())
                {
                    endpoints.MapGrpcReflectionService();
                }

                endpoints.MapControllers();

                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");
                });
            });
        }
    }

    internal static class StartupExtensions
    {
        public static IServiceCollection AddOptions(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<TotalviewOptions>(configuration.GetSection("Totalview"));
            return services;
        }

        public static IServiceCollection AddCustomAutoMappe(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(Startup).Assembly);
            return services;
        }



        public static IServiceCollection AddCustomControllers(this IServiceCollection services)
        {
            services.AddControllers();
            return services;
        }

        public static IServiceCollection AddCustomGrpc(this IServiceCollection services)
        {
            services.AddGrpc();
            services.AddGrpcReflection();
            // services.AddGrpcHttpApi(); See: https://docs.microsoft.com/en-us/aspnet/core/grpc/httpapi?view=aspnetcore-5.0
            return services;
        }



        public static IServiceCollection AddCustomAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            IdentityModelEventSource.ShowPII = true; // TODO: Remove this. Only for testing

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = IdentityServerAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = IdentityServerAuthenticationDefaults.AuthenticationScheme;
            })
                .AddIdentityServerAuthentication(options =>
                {
                    IdentityServerAuthenticationOptions option = configuration
                        .GetSection("IdentityServerAuthentication")
                        .Get<IdentityServerAuthenticationOptions>();

                    new MapperConfiguration(cfg => cfg.CreateMap<IdentityServerAuthenticationOptions, IdentityServerAuthenticationOptions>())
                        .CreateMapper()
                        .Map(option, options);
                });

            services.AddAuthorization(options =>
            {
                options.AddPolicy(IdentityServerAuthenticationDefaults.AuthenticationScheme, policy =>
                {
                    policy.AddAuthenticationSchemes(IdentityServerAuthenticationDefaults.AuthenticationScheme);
                    policy.RequireClaim(JwtClaimTypes.Name);
                });
            });

            return services;
        }

        public static IServiceCollection AddCustomCors(this IServiceCollection services)
        {
            services.AddCors(o => o.AddPolicy("AllowAll", builder =>
            {
                builder
                    .AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .WithExposedHeaders("Grpc-Status", "Grpc-Message", "Grpc-Encoding", "Grpc-Accept-Encoding");
            }));
            return services;
        }
    }
}
