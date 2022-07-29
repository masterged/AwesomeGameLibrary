using System.Reflection;
using AwesomeGameLibrary.Application.Features.Games;
using AwesomeGameLibrary.Application.Validation;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace AwesomeGameLibrary.Application;

public static class ConfigurationExtension
{
    public static IServiceCollection AddApplication(this IServiceCollection serviceCollection)
    {
        var domainAssembly = typeof(ConfigurationExtension).GetTypeInfo().Assembly;

        // Add MediatR
        serviceCollection.AddMediatR(configuration => configuration.RegisterServicesFromAssembly(domainAssembly));

        serviceCollection.Add(new ServiceDescriptor(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>),
            ServiceLifetime.Transient));
        
        serviceCollection.AddValidatorsFromAssemblies(new[] { domainAssembly }, ServiceLifetime.Transient, null);
        return serviceCollection;
    }
}