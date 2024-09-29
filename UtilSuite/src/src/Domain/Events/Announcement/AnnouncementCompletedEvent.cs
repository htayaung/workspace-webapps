using Domain.Entities;

namespace Domain.Events;

public class AnnouncementCompletedEvent : BaseEvent
{
    public AnnouncementCompletedEvent(Announcement item)
    {
        Item = item;
    }

    public Announcement Item { get; }
}
