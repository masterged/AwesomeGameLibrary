using AwesomeGameLibrary.Domain.Audit;
using MediatR;

namespace AwesomeGameLibrary.Application.Features.Audit.Command;

public record FancyAuditCommand<TEvent>(TEvent Event) : INotification where TEvent : FancyAuditEvent;