using Domain.Entities;

namespace Domain.Events;

public class AnnouncementDeletedEvent : BaseEvent
{
    public AnnouncementDeletedEvent(Announcement item)
    {
        Item = item;
    }

    public Announcement Item { get; }
}
