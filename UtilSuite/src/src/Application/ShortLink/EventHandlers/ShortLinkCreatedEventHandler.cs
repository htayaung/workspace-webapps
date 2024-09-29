using Microsoft.Extensions.Logging;
using Domain.Events;

namespace Application.ShortLink.EventHandlers;

public class ShortLinkCreatedEventHandler : INotificationHandler<AnnouncementCreatedEvent>
{
    private readonly ILogger<ShortLinkCreatedEventHandler> _logger;

    public ShortLinkCreatedEventHandler(ILogger<ShortLinkCreatedEventHandler> logger)
    {
        _logger = logger;
    }

    public Task Handle(AnnouncementCreatedEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Domain Event: {DomainEvent}", notification.GetType().Name);
        return Task.CompletedTask;
    }
}
