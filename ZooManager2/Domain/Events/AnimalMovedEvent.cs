using Domain.Entities;
using Domain.ValueObjects;

namespace Domain.Events;

public class AnimalMovedEvent
{
    public Animal EventAnimal { get; }
    public Enclosure NewEnclosure { get; }

    public AnimalMovedEvent(Animal eventAnimal, Enclosure newEnclosure)
    {
        EventAnimal = eventAnimal;
        NewEnclosure = newEnclosure;
    }
}