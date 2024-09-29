using Domain.Entities;

namespace Domain.Events;

public class ShortLinkDeletedEvent : BaseEvent
{
    public ShortLinkDeletedEvent(ShortLink item)
    {
        Item = item;
    }

    public ShortLink Item { get; }
}
