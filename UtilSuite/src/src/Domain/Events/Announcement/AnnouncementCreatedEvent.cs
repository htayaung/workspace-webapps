using Domain.Entities;

namespace Domain.Events;

public class AnnouncementCreatedEvent : BaseEvent
{
    public AnnouncementCreatedEvent(Announcement item)
    {
        Item = item;
    }

    public Announcement Item { get; }
}
