using Audit.Core;
using AwesomeGameLibrary.Application.Features.Audit.Command;
using AwesomeGameLibrary.Domain.Audit;
using MediatR;

namespace AwesomeGameLibrary.Application.Features.Audit;

public class FancyAuditHandler<TEvent> : INotificationHandler<FancyAuditCommand<TEvent>> where TEvent : FancyAuditEvent
{
    public async Task Handle(FancyAuditCommand<TEvent> notification, CancellationToken cancellationToken)
    {
        await AuditScope.LogAsync("1111",new {field = "1111"});
    }
}