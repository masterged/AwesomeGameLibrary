using System.Reflection;
using AwesomeGameLibrary.Application.Validation;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

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
}