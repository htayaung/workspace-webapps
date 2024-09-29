using Domain.Entities;

namespace Domain.Events;

public class ShortLinkCreatedEvent : BaseEvent
{
    public ShortLinkCreatedEvent(ShortLink item)
    {
        Item = item;
    }

    public ShortLink Item { get; }
}
