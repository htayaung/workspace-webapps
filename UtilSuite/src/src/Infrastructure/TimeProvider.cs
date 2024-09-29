namespace Infrastructure;

public class TimeProvider
{
    public DateTimeOffset GetUtcNow()
    {
        return DateTime.UtcNow;
    }
}
