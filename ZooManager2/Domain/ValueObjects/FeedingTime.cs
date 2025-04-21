namespace Domain.ValueObjects;

public readonly struct FeedingTime
{
    public TimeOnly Value { get; }
    public FeedingTime(TimeOnly time) => Value = time;
}