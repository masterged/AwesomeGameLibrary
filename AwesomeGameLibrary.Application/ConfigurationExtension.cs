using System.Reflection;
using Audit.Core;
using AwesomeGameLibrary.Application.Validation;
using FluentValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using NLog;
using NLog.Web;
using LogLevel = Audit.NLog.LogLevel;

namespace AwesomeGameLibrary.Application;

public static class ConfigurationExtension
{
    public static void AddMediatr(this IServiceCollection serviceCollection)
    {
        var domainAssembly = typeof(ConfigurationExtension).GetTypeInfo().Assembly;

        // Add MediatR
        serviceCollection.AddMediatR(configuration =>
            configuration.RegisterServicesFromAssembly(domainAssembly)
                .AddOpenBehavior(typeof(ValidationBehavior<,>)));

        serviceCollection.AddValidatorsFromAssemblies(new[] { domainAssembly }, ServiceLifetime.Transient, null);
    }

    public static void AuditSetupOutput(this WebApplication app)
    {
        // TODO: Configure the audit output.
        // For more info, see https://github.com/thepirat000/Audit.NET#data-providers.
        Configuration.Setup()
            .UseNLog(config => config
                .Logger(LogManager.Setup()
                    .LoadConfigurationFromAppSettings()
                    .GetLogger("Audit"))        
                .LogLevel(LogLevel.Debug)
                .Message(auditEvent => auditEvent.ToJson()));

        Configuration.JsonSettings.WriteIndented = true;
    }

    public static void AddHealthCheck(this IServiceCollection serviceCollection)
    {
        // Add services to the container.
        serviceCollection
            .AddHealthChecks()
            .AddProcessAllocatedMemoryHealthCheck(maximumMegabytesAllocated: 100, tags: new[] { "process", "memory" })
            .AddDiskStorageHealthCheck(x => x.AddDrive("C:\\"), tags: new[] { "storage" });

        serviceCollection.AddHealthChecksUI(x =>
        {
            x.SetHeaderText("Branding Demo - Health Checks Status");
            x.AddHealthCheckEndpoint("endpoint1", "/health-process");
            x.AddHealthCheckEndpoint("endpoint2", "/health-disc");
        }).AddInMemoryStorage();
    }
}