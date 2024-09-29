using Microsoft.Extensions.Logging;
using Domain.Events;

namespace Application.Announcement.EventHandlers;

public class AnnouncementCreatedEventHandler : INotificationHandler<AnnouncementCreatedEvent>
{
    private readonly ILogger<AnnouncementCreatedEventHandler> _logger;

    public AnnouncementCreatedEventHandler(ILogger<AnnouncementCreatedEventHandler> logger)
    {
        _logger = logger;
    }

    public Task Handle(AnnouncementCreatedEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Domain Event: {DomainEvent}", notification.GetType().Name);
        return Task.CompletedTask;
    }
}
