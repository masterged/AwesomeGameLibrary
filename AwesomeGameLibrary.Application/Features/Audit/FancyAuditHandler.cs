using Audit.Core;
using AwesomeGameLibrary.Application.Features.Audit.Command;
using AwesomeGameLibrary.Domain.Audit;
using MediatR;

namespace AwesomeGameLibrary.Application.Features.Audit;

public class FancyAuditHandler : INotificationHandler<FancyAuditCommand<GenreEvent>>
{
    public async Task Handle(FancyAuditCommand<GenreEvent> notification, CancellationToken cancellationToken)
    {
        await using (var scope =
                     await AuditScope.CreateAsync(new AuditScopeOptions() { AuditEvent = notification.Event }))
        {
        }
    }
}