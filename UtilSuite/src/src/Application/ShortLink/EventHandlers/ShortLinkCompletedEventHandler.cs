using Microsoft.Extensions.Logging;
using Domain.Events;

namespace Application.ShortLink.EventHandlers;

public class ShortLinkCompletedEventHandler : INotificationHandler<AnnouncementCompletedEvent>
{
    private readonly ILogger<ShortLinkCompletedEventHandler> _logger;

    public ShortLinkCompletedEventHandler(ILogger<ShortLinkCompletedEventHandler> logger)
    {
        _logger = logger;
    }

    public Task Handle(AnnouncementCompletedEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Domain Event: {DomainEvent}", notification.GetType().Name);
        return Task.CompletedTask;
    }
}
