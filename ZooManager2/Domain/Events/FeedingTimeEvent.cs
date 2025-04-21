using Domain.Entities;
using Domain.ValueObjects;

namespace Domain.Events;

public class FeedingTimeEvent
{
    public Animal EventAnimal { get; }
    public FeedingTime Time { get; }

    public FeedingTimeEvent(Animal eventAnimal, FeedingTime time)
    {
        EventAnimal = eventAnimal;
        Time = time;
    }
}