using ECommerce.Behaviors;
using ECommerce.EventBus;
using ECommerce.EventBusRabbitMQ;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Polly;
using Polly.Extensions.Http;
using System;
using System.Linq;
using System.Net.Http;
using System.Reflection;

namespace ECommerce.ApiInfrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddCustomSwagger(this IServiceCollection services, string apiName)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = apiName, Version = "v1" });
            });

            return services;
        }

        public static IServiceCollection AddCustomMediatr(this IServiceCollection services, params Assembly[] assemblies)
        {
            var validators = AssemblyScanner.FindValidatorsInAssemblies(assemblies);
            if (validators.Any())
            {
                validators.ForEach(result => services.AddScoped(result.InterfaceType, result.ValidatorType));
                services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidatorBehavior<,>));
            }

            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));

            services.AddMediatR(assemblies);
            return services;
        }

        public static IServiceCollection AddCustomHttpClient(this IServiceCollection services, string clientName, string baseUrl)
        {
            services
                .AddHttpClient(clientName, client =>
                {
                    client.BaseAddress = new Uri(baseUrl);
                })
                .AddPolicyHandler(GetRetryPolicy())
                .AddPolicyHandler(GetCircuitBreakerPolicy());

            return services;
        }

        static IAsyncPolicy<HttpResponseMessage> GetRetryPolicy()
        {
            return HttpPolicyExtensions
                .HandleTransientHttpError()
                .WaitAndRetryAsync(
                retryCount: 5,
                retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)),
                onRetry: (outcome, timespan, retryAttempt, context) =>
                {
                    //Log.Information($"Delaying for { timespan.Seconds } seconds, then making retry { retryAttempt }. Error: {outcome?.Result?.ToString()}");
                });
        }

        static IAsyncPolicy<HttpResponseMessage> GetCircuitBreakerPolicy()
        {
            return HttpPolicyExtensions
                .HandleTransientHttpError()
                .CircuitBreakerAsync(
                handledEventsAllowedBeforeBreaking: 4,
                durationOfBreak: TimeSpan.FromSeconds(30),
                onBreak: (exception, timespan) =>
                {
                    //Log.Error($"Circuit Breaker open");
                },
                onReset: () => {
                    //Log.Information($"Circuit Breaker reseted");
                });
        }

        public static IServiceCollection AddEventBus(this IServiceCollection services)
        {
            services.AddSingleton<IRabbitMQConnection, RabbitMQConnection>();
            services.AddSingleton<IEventBus, DirectEventBusRabbitMQ>();

            return services;
        }
    }
}
