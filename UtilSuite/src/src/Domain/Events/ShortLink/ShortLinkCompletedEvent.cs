using Domain.Entities;

namespace Domain.Events;

public class ShortLinkCompletedEvent : BaseEvent
{
    public ShortLinkCompletedEvent(ShortLink item)
    {
        Item = item;
    }

    public ShortLink Item { get; }
}
