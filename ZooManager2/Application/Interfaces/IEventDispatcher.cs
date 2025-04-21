namespace Application.Interfaces;

public interface IEventDispatcher
{
    void Dispatch<TEvent>(TEvent domainEvent);
}