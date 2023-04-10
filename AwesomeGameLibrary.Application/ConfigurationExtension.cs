using System.Reflection;
using Audit.Core;
using AwesomeGameLibrary.Application.Validation;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using NLog;
using NLog.Web;
using LogLevel = Audit.NLog.LogLevel;

namespace AwesomeGameLibrary.Application;

public static class ConfigurationExtension
{
    public static IServiceCollection AddApplication(this IServiceCollection serviceCollection)
    {
        var domainAssembly = typeof(ConfigurationExtension).GetTypeInfo().Assembly;

        // Add MediatR
        serviceCollection.AddMediatR(configuration =>
            configuration.RegisterServicesFromAssembly(domainAssembly)
                .AddOpenBehavior(typeof(ValidationBehavior<,>)));

        serviceCollection.AddValidatorsFromAssemblies(new[] { domainAssembly }, ServiceLifetime.Transient, null);
        return serviceCollection;
    }
    
    public static void AuditSetupOutput(this WebApplication app)
    {
        // TODO: Configure the audit output.
        // For more info, see https://github.com/thepirat000/Audit.NET#data-providers.
        Audit.Core.Configuration.Setup()
            .UseNLog(config =>config
                .Logger(LogManager.Setup()
                    .LoadConfigurationFromAppSettings(nlogConfigSection:"NLogAudit")
                    .GetLogger("Audit"))        
                .LogLevel(LogLevel.Debug)
                .Message(auditEvent => auditEvent.ToJson()));

        Audit.Core.Configuration.JsonSettings.WriteIndented = true;

        // Include the trace identifier in the audit events
        var httpContextAccessor = app.Services.GetRequiredService<IHttpContextAccessor>();
        Audit.Core.Configuration.AddCustomAction(ActionType.OnScopeCreated, scope =>
        {
            scope.SetCustomField("TraceId", httpContextAccessor.HttpContext?.TraceIdentifier);
        });
    }
}