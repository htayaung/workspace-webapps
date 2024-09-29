using Microsoft.Extensions.Logging;
using Domain.Events;

namespace Application.Announcement.EventHandlers;

public class AnnouncementCompletedEventHandler : INotificationHandler<AnnouncementCompletedEvent>
{
    private readonly ILogger<AnnouncementCompletedEventHandler> _logger;

    public AnnouncementCompletedEventHandler(ILogger<AnnouncementCompletedEventHandler> logger)
    {
        _logger = logger;
    }

    public Task Handle(AnnouncementCompletedEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Domain Event: {DomainEvent}", notification.GetType().Name);
        return Task.CompletedTask;
    }
}
